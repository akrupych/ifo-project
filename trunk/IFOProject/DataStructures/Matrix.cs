using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using BitMiracle.LibTiff.Classic;
using System.Runtime.InteropServices;

namespace IFOProject.DataStructures
{
    class ImageMatrix
    {
        /// <summary>
        /// Matrix itsels
        /// </summary>
        private byte[] Data { get; set; }
        /// <summary>
        /// Rows number
        /// </summary>
        public int Rows { get; private set; }
        /// <summary>
        /// Columns number
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Creates empty matrix
        /// </summary>
        /// <param name="rows">Rows number</param>
        /// <param name="columns">Columns number</param>
        public ImageMatrix(int rows, int columns)
        {
            Data = new byte[rows * columns];
            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Creates matrix from 600x500 .TIF image
        /// </summary>
        /// <param name="fileName">Image path</param>
        public ImageMatrix(String fileName)
        {
            // prepare for reading
            Tiff input = Tiff.Open(fileName, "r");
            Columns = input.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            Rows = input.GetField(TiffTag.IMAGELENGTH)[0].ToInt();

            // read data
            Data = new byte[Rows * Columns];
            for (int row = 0; row < Rows; row++)
                input.ReadScanline(Data, row * Columns, row, 0);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="that">Matrix to copy</param>
        public ImageMatrix(ImageMatrix that) : this(that.Rows, that.Columns)
        {
            this.Data = (byte[]) that.Data.Clone();
        }

        /// <summary>
        /// Gets or sets matrix element
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Element value</returns>
        public byte this [int row, int column]
        {
            get { return Data[row * Columns + column]; }
            set { Data[row * Columns + column] = value; }
        }

        /// <summary>
        /// Converts matrix to grayscale bitmap
        /// </summary>
        /// <returns>Created bitmap</returns>
        public Bitmap ToBitmap()
        {
            // prepare bitmap
            Bitmap bmp = new Bitmap(Columns, Rows, PixelFormat.Format8bppIndexed);
            ColorPalette ncp = bmp.Palette;
            for (int i = 0; i < 256; i++)
                ncp.Entries[i] = Color.FromArgb(255, i, i, i);
            bmp.Palette = ncp;

            // copy data
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, Columns, Rows),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            IntPtr ptr = data.Scan0;
            byte[] aligned = GetBitmapArray(data.Stride);
            Marshal.Copy(aligned, 0, ptr, aligned.Length);
            bmp.UnlockBits(data);
            return bmp;
        }

        private byte[] GetBitmapArray(int stride)
        {
            byte[] result = new byte[stride * Rows];
            for (int row = 0; row < Rows; row++)
                Array.Copy(Data, row * Columns, result, row * stride, Columns);
            return result;
        }

        public byte[] Column(int column)
        {
            if (column < 0) column = 0;
            else if (column >= Columns) column = Columns - 1;
            byte[] result = new byte[Rows];
            for (int i = 0; i < Rows; i++)
            {
                result[i] = this[i, column];
            }
            return result;
        }

        public byte[] Row(int row)
        {
            if (row < 0) row = 0;
            else if (row >= Rows) row = Rows - 1;
            byte[] result = new byte[Columns];
            for (int i = 0; i < Columns; i++)
            {
                result[i] = this[row, i];
            }
            return result;
        }
    }
}
