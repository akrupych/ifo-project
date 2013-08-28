using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IFOProject.DataStructures
{
    public class MyRectangle
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public int Width { get { return Right - Left + 1; } }

        public int Height { get { return Bottom - Top + 1; } }

        public Location[] Corners
        {
            get
            {
                return new Location[] {
                    new Location(Left, Top),
                    new Location(Right, Top),
                    new Location(Right, Bottom),
                    new Location(Left, Bottom)
                };
            }
        }

        public void MoveCorner(Corner edge, Location newLocation)
        {
            switch (edge)
            {
                case Corner.TopLeft:
                    Left = newLocation.X;
                    Top = newLocation.Y;
                    break;
                case Corner.TopRight:
                    Right = newLocation.X;
                    Top = newLocation.Y;
                    break;
                case Corner.BottomRight:
                    Right = newLocation.X;
                    Bottom = newLocation.Y;
                    break;
                case Corner.BottomLeft:
                    Left = newLocation.X;
                    Bottom = newLocation.Y;
                    break;
            }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Left, Top, Width, Height);
        }

        public void Crop(MyRectangle that)
        {
            this.Left = Math.Max(this.Left, that.Left);
            this.Right = Math.Min(this.Right, that.Right);
            this.Top = Math.Max(this.Top, that.Top);
            this.Bottom = Math.Min(this.Bottom, that.Bottom);
        }
    }
}
