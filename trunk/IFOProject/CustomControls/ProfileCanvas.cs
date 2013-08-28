using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IFOProject.CustomControls
{
    /// <summary>
    /// DoubleBuffered PictureBox for profiles
    /// </summary>
    class ProfileCanvas : PictureBox
    {
        /// <summary>
        /// Profiles plot height
        /// </summary>
        private const int profilesHeight = 128;
        /// <summary>
        /// Distance between vertical lines
        /// </summary>
        private const int gridDistanceX = 50;
        /// <summary>
        /// Distance between horizontal lines
        /// </summary>
        private const int gridDistanceY = 32;

        /// <summary>
        /// Default contructor
        /// </summary>
        public ProfileCanvas()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        /// Draws horizontal or vertical profile
        /// </summary>
        /// <param name="profile">Intensity values</param>
        /// <param name="row">True if row, false if column</param>
        public void DrawProfile(byte[] profile, bool row)
        {
            double scale = profilesHeight / (byte.MaxValue + 1.0);
            int length = profile.Length;
            Bitmap img = new Bitmap(length, profilesHeight);
            Graphics g = Graphics.FromImage(img);
            Point prev = new Point(0, profilesHeight - (int)(profile[0] * scale));
            for (int i = gridDistanceY; i < profilesHeight; i += gridDistanceY)
                g.DrawLine(new Pen(Color.Black), new Point(0, i), new Point(length - 1, i));
            for (int i = 1; i < length; i++)
            {
                if (i % 50 == 0) g.DrawLine(new Pen(Color.Black), new Point(i, 0), new Point(i, profilesHeight));
                Point next = new Point(i, profilesHeight - (int)(profile[i] * scale));
                g.DrawLine(new Pen(Color.Blue), prev, next);
                prev = next;
            }
            if (!row) img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Image = img;
            Size = img.Size;
        }

        public void Clear()
        {
            Image = null;
            Invalidate();
        }
    }
}
