using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IFOProject.DataStructures;
using System.Linq;
using System.Drawing.Imaging;

namespace IFOProject.Experimental
{
    public class Pattern
    {
        /// <summary>
        /// Image file name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Intensity matrix
        /// </summary>
        private ImageMatrix OriginalMatrix { get; set; }
        /// <summary>
        /// Smoothed intensity matrix
        /// </summary>
        private ImageMatrix SmoothedMatrix { get; set; }
        /// <summary>
        /// Smoothing radius for smoothed image
        /// </summary>
        public int SmoothingRadius { get; set; }
        /// <summary>
        /// True for smoothed, false for normal mode
        /// </summary>
        public bool UseSmoothing { get; set; }
        /// <summary>
        /// Gets or sets selection rectangle for pattern calculations
        /// </summary>
        public MyRectangle Selection { get; set; }
        /// <summary>
        /// Row step to perform calculations
        /// </summary>
        public int SelectionRowStep { get; set; }
        /// <summary>
        /// Shows/hides selection
        /// </summary>
        public bool ShowSelection { get; set; }
        /// <summary>
        /// Current calculation results
        /// </summary>
        public RowCalculations[] Calculations { get; set; }

        /// <summary>
        /// Reads pattern from image file
        /// </summary>
        /// <param name="fileName"></param>
        public Pattern(string fileName)
        {
            SelectionRowStep = 1;
            SmoothingRadius = 1;
            Name = Path.GetFileName(fileName);
            OriginalMatrix = new ImageMatrix(fileName);
            Selection = new MyRectangle { Left = Width / 4, Top = Height / 4, Right = (3 * Width) / 4, Bottom = (3 * Height) / 4 };
        }

        private ImageMatrix CurrentMatrix { get { return UseSmoothing ? SmoothedMatrix : OriginalMatrix; } }

        /// <summary>
        /// Pattern image
        /// </summary>
        public Bitmap Bitmap { get { return CurrentMatrix.ToBitmap(); } }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width { get { return OriginalMatrix.Columns; } }

        /// <summary>
        /// Image height
        /// </summary>
        public int Height { get { return OriginalMatrix.Rows; } }

        /// <summary>
        /// Gets column intensity vector
        /// </summary>
        /// <param name="column">Column index</param>
        public byte[] ColumnProfile(int column)
        {
            return CurrentMatrix.Column(column);
        }

        /// <summary>
        /// Gets row intensity vector
        /// </summary>
        /// <param name="row">Row index</param>
        public byte[] RowProfile(int row)
        {
            return CurrentMatrix.Row(row);
        }

        /// <summary>
        /// Gets average intensity value for square with 2*radius+1 edge and center in point
        /// </summary>
        /// <param name="point">Center point of square</param>
        /// <param name="radius">Distance from center to edge</param>
        public byte AverageIntensity(int x, int y, int radius)
        {
            int sum = 0, count = 0;
            Location exactRadius = new Location(radius, radius);
            for (int i = x - exactRadius.X; i <= x + exactRadius.X; i++)
            {
                if (i < 0 || i >= Width)
                {
                    exactRadius.X--;
                    continue;
                }
                for (int j = y - exactRadius.Y; j <= y + exactRadius.Y; j++)
                {
                    if (j < 0 || j >= Height)
                    {
                        exactRadius.Y--;
                        continue;
                    }
                    sum += CurrentMatrix[j, i];
                    count++;
                }
            }
            return (byte)(sum / count);
        }

        public byte AverageIntensity(Location point, int radius)
        {
            return AverageIntensity(point.X, point.Y, radius);
        }

        /// <summary>
        /// Calculates average intencity without bounds check
        /// </summary>
        /// <param name="x">Center X</param>
        /// <param name="y">Center Y</param>
        /// <param name="radius">Averaging radius</param>
        /// <returns>Average intencity in center point</returns>
        private byte SimpleAverageIntensity(int x, int y, int radius)
        {
            int sum = 0;
            for (int i = x - radius; i <= x + radius; i++)
                for (int j = y - radius; j <= y + radius; j++)
                    sum += CurrentMatrix[j, i];
            int count = (2 * radius + 1) * (2 * radius + 1);
            return (byte)(sum / count);
        }

        /// <summary>
        /// Smoothes all image
        /// </summary>
        /// <param name="radius">Smooth radius</param>
        public void Smooth(int radius)
        {
            // reuse old matrix if exists
            if (SmoothedMatrix == null)
                SmoothedMatrix = new ImageMatrix(Height, Width);
            SmoothingRadius = radius;

            // fill heavy part with bounds checking
            for (int x = 0; x < Width; x++)
            {
                // top
                for (int y = 0; y < radius; y++)
                    SmoothedMatrix[y, x] = AverageIntensity(x, y, radius);
                // bottom
                for (int y = Height - radius; y < Height; y++)
                    SmoothedMatrix[y, x] = AverageIntensity(x, y, radius);
            }
            for (int y = radius; y < Height - radius; y++)
            {
                // left
                for (int x = 0; x < radius; x++)
                    SmoothedMatrix[y, x] = AverageIntensity(x, y, radius);
                // right
                for (int x = Width - radius; x < Width; x++)
                    SmoothedMatrix[y, x] = AverageIntensity(x, y, radius);
            }

            // quick smooth without checking
            for (int x = radius; x < Width - radius; x++)
                for (int y = radius; y < Height - radius; y++)
                    SmoothedMatrix[y, x] = SimpleAverageIntensity(x, y, radius);
        }

        public void SmoothSelection(int radius)
        {
            ImageMatrix smoothed = new ImageMatrix(CurrentMatrix);
            // left overlap
            for (int x = Selection.Left; x < radius; x++)
                for (int y = Selection.Top; y <= Selection.Bottom; y++)
                    smoothed[y, x] = AverageIntensity(x, y, radius);
            // right overlap
            for (int x = Width - radius; x <= Selection.Right; x++)
                for (int y = Selection.Top; y <= Selection.Bottom; y++)
                    smoothed[y, x] = AverageIntensity(x, y, radius);
            // top overlap
            for (int y = Selection.Top; y < radius; y++)
                for (int x = Selection.Left; x <= Selection.Right; x++)
                    smoothed[y, x] = AverageIntensity(x, y, radius);
            // bottom overlap
            for (int y = Height - radius; y <= Selection.Bottom; y++)
                for (int x = Selection.Left; x <= Selection.Right; x++)
                    smoothed[y, x] = AverageIntensity(x, y, radius);
            // quick smooth
            int fromX = Math.Max(Selection.Left, radius);
            int toX = Math.Min(Selection.Right, Width - radius);
            int fromY = Math.Max(Selection.Top, radius);
            int toY = Math.Min(Selection.Bottom, Height - radius);
            for (int x = fromX; x < toX; x++)
                for (int y = fromY; y < toY; y++)
                    smoothed[y, x] = SimpleAverageIntensity(x, y, radius);
            SmoothedMatrix = smoothed;
        }

        public RowCalculations[] Calculate()
        {
            return Calculate(null);
        }

        public RowCalculations[] Calculate(Coefficients approximation)
        {
            List<RowCalculations> result = new List<RowCalculations>();
            if (approximation == null) 
                result.Add(new RowCalculations(Selection.Top, CurrentMatrix.Row(Selection.Top), 
                    Selection.Left, Selection.Right));
            else result.Add(new RowCalculations(Selection.Top, CurrentMatrix.Row(Selection.Top),
                    Selection.Left, Selection.Right, approximation));
            for (int row = Selection.Top + SelectionRowStep;
                row <= Selection.Bottom; row += SelectionRowStep)
            {
                result.Add(new RowCalculations(row, CurrentMatrix.Row(row),
                    Selection.Left, Selection.Right, result.Last().Final));
            }
            Calculations = result.ToArray();
            return Calculations;
        }

        public RowCalculations[] Calculate(int fromIndex, Coefficients newValues)
        {
            Calculations[fromIndex].Recalculate(newValues);
            for (int i = fromIndex + 1; i < Calculations.Length; i++)
                Calculations[i].Recalculate(Calculations[i - 1].Final);
            return Calculations;
        }

        public RowCalculations CalculateFirstRow()
        {
            return CalculateFirstRow(null);
        }

        public RowCalculations CalculateFirstRow(Coefficients approximation)
        {
            if (approximation == null)
                return new RowCalculations(Selection.Top, CurrentMatrix.Row(Selection.Top),
                    Selection.Left, Selection.Right);
            else return new RowCalculations(Selection.Top, CurrentMatrix.Row(Selection.Top),
                    Selection.Left, Selection.Right, approximation);
        }

        public MyRectangle Bounds
        {
            get
            {
                return new MyRectangle
                {
                    Top = 0,
                    Left = 0,
                    Right = Width - 1,
                    Bottom = Height - 1
                };
            }
        }
    }
}
