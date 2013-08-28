using System;
using System.Drawing;

namespace IFOProject.DataStructures
{
    public class Location
    {
        /// <summary>
        /// X - coordinate
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y - coordinate
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Value indicating whether the point was initialized
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Constructs (0, 0) non-existing location
        /// </summary>
        public Location() : this(0, 0)
        {
            Exists = false;
        }

        /// <summary>
        /// Coordinates constructor
        /// </summary>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Exists = true;
        }

        /// <summary>
        /// Point constructor
        /// </summary>
        /// <param name="point">Point to construct from</param>
        public Location(Point point) : this(point.X, point.Y) { }

        /// <summary>
        /// Location constructor
        /// </summary>
        /// <param name="location">Location to construct from</param>
        public Location(Location location) : this(location.X, location.Y)
        {
            Exists = location.Exists;
        }

        /// <summary>
        /// Subtracts two locations
        /// </summary>
        /// <param name="first">Left operand</param>
        /// <param name="second">Right operand</param>
        /// <returns>Subtracted location</returns>
        public static Location operator - (Location first, Location second)
        {
            return new Location(first.X - second.X, first.Y - second.Y);
        }

        /// <summary>
        /// Returns distance from this point to another
        /// </summary>
        /// <param name="that">Another point</param>
        /// <returns>Distance</returns>
        public double DistanceTo(Location that)
        {
            return Math.Sqrt(Math.Pow(this.X - that.X, 2) + Math.Pow(this.Y - that.Y, 2));
        }

        /// <summary>
        /// Projects a point to restangle
        /// </summary>
        /// <param name="myRectangle">Rectangle to project</param>
        public void project(MyRectangle rect)
        {
            if (X < rect.Left) X = rect.Left;
            else if (X >= rect.Right) X = rect.Right - 1;
            if (Y < rect.Top) Y = rect.Top;
            else if (Y >= rect.Bottom) Y = rect.Bottom - 1;
        }
    }
}
