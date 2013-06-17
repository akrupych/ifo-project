using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IFOProject.DataStructures;
using IFOProject.Forms;
using IFOProject.Experimental;

namespace IFOProject.CustomControls
{
    /// <summary>
    /// DoubleBuffered PictureBox for pattern image
    /// </summary>
    class ImageCanvas : PictureBox
    {
        /// <summary>
        /// Radius from selection edge for changing cursor
        /// </summary>
        private const int CURSOR_RADIUS = 10;
        /// <summary>
        /// Parent form
        /// </summary>
        private MainForm ParentForm { get; set; }

        public bool MousePressed { get; set; }

        public bool Empty { get { return Program.Package.Patterns.Count == 0; } }

        /// <summary>
        /// Default contructor
        /// </summary>
        public ImageCanvas()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        /// Sets parent form for accessing members
        /// </summary>
        /// <param name="parent">Parent form</param>
        public ImageCanvas(MainForm parent, Size size) : this()
        {
            DoubleBuffered = true;
            ParentForm = parent;
            Size = size;
            GotFocus += new EventHandler(ImageCanvas_GotFocus);
            Paint += new PaintEventHandler(ImageCanvas_Paint);
            MouseDown += new MouseEventHandler(ImageCanvas_MouseDown);
            MouseMove += new MouseEventHandler(ImageCanvas_MouseMove);
            MouseUp += new MouseEventHandler(ImageCanvas_MouseUp);
        }

        /// <summary>
        /// Process arrows keys (with CTRL)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Empty) return;
            if (ParentForm.ProfilesSelected)
            {
                Location profiles = ParentForm.ProfilesPoint;
                int step = e.Control ? 15 : 1;
                if (e.KeyCode == Keys.Up && profiles.Y >= step) profiles.Y -= step;
                else if (e.KeyCode == Keys.Down && profiles.Y < Height - step) profiles.Y += step;
                else if (e.KeyCode == Keys.Left && profiles.X >= step) profiles.X -= step;
                else if (e.KeyCode == Keys.Right && profiles.X < Width - step) profiles.X += step;
                MoveProfilesPoint(profiles);
            }
        }

        /// <summary>
        /// Converts given location relatively to the upper-left control corner
        /// </summary>
        /// <param name="location">Event location</param>
        /// <returns>Converted location</returns>
        private Location ToRelativeLocation(Point location)
        {
            return new Location(location) - new Location(this.Location);
        }

        /// <summary>
        /// Moves profiles point to the mouse position
        /// </summary>
        /// <param name="newPoint">Location from the upper-left window corner</param>
        private void MoveProfilesPoint(Location location)
        {
            ParentForm.ProfilesPoint = location;
            ParentForm.RefreshProfiles();
            Invalidate();
        }

        /// <summary>
        /// Mouse clicked
        /// </summary>
        private void ImageCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (Empty) return;
            MousePressed = true;
            HandleCursor(ToRelativeLocation(e.Location));
        }

        /// <summary>
        /// Mouse moved
        /// </summary>
        private void ImageCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Empty) return;
            Location location = ToRelativeLocation(e.Location);
            HandleCursor(location);
            if (MousePressed && ParentForm.SelectionSelected && Cursor.Current != Cursors.Cross)
            {
                MyRectangle selection = Program.Package.CurrentPattern.Selection;
                if (Cursor.Current == Cursors.PanWest) selection.Left = location.X;
                else if (Cursor.Current == Cursors.PanEast) selection.Right = location.X;
                else if (Cursor.Current == Cursors.PanNorth) selection.Top = location.Y;
                else if (Cursor.Current == Cursors.PanSouth) selection.Bottom = location.Y;
                else if (Cursor.Current == Cursors.PanNW)
                    selection.MoveCorner(Corner.TopLeft, location);
                else if (Cursor.Current == Cursors.PanNE)
                    selection.MoveCorner(Corner.TopRight, location);
                else if (Cursor.Current == Cursors.PanSE)
                    selection.MoveCorner(Corner.BottomRight, location);
                else if (Cursor.Current == Cursors.PanSW)
                    selection.MoveCorner(Corner.BottomLeft, location);
                Refresh();
                ParentForm.RefreshCalculating();
            }
        }

        /// <summary>
        /// Mouse released
        /// </summary>
        private void ImageCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (Empty) return;
            MousePressed = false;
            if (ParentForm.ProfilesSelected)
                MoveProfilesPoint(ToRelativeLocation(e.Location));
            if (ParentForm.SelectionSelected)
                Program.Package.CurrentPattern.Selection.Crop(
                    Program.Package.CurrentPattern.Bounds);
        }

        private void HandleCursor(Location location)
        {
            if (Empty) return;
            if (ParentForm.ProfilesSelected && Cursor.Current != Cursors.Cross)
                Cursor.Current = Cursors.Cross;
            if (ParentForm.SelectionSelected)
            {
                MyRectangle selection = Program.Package.CurrentPattern.Selection;
                bool left = Math.Abs(location.X - selection.Left) < CURSOR_RADIUS;
                bool top = Math.Abs(location.Y - selection.Top) < CURSOR_RADIUS;
                bool right = Math.Abs(location.X - selection.Right) < CURSOR_RADIUS;
                bool bottom = Math.Abs(location.Y - selection.Bottom) < CURSOR_RADIUS;
                if (left)
                    if (top) Cursor.Current = Cursors.PanNW;
                    else if (bottom) Cursor.Current = Cursors.PanSW;
                    else Cursor.Current = Cursors.PanWest;
                else if (right)
                    if (top) Cursor.Current = Cursors.PanNE;
                    else if (bottom) Cursor.Current = Cursors.PanSE;
                    else Cursor.Current = Cursors.PanEast;
                else if (top) Cursor.Current = Cursors.PanNorth;
                else if (bottom) Cursor.Current = Cursors.PanSouth;
            }
        }

        /// <summary>
        /// Drawing lines
        /// </summary>
        private void ImageCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (Empty) return;
            Graphics g = e.Graphics;
            if (ParentForm.SelectionSelected)
            {
                g.DrawRectangle(new Pen(Color.Red),
                    Program.Package.CurrentPattern.Selection.ToRectangle());
            }
            if (ParentForm.ProfilesSelected)
            {
                Location profiles = ParentForm.ProfilesPoint;
                g.DrawLine(new Pen(Color.Yellow), new Point(profiles.X, 0),
                    new Point(profiles.X, Height - 1));
                g.DrawLine(new Pen(Color.Yellow), new Point(0, profiles.Y),
                    new Point(Width - 1, profiles.Y));
            }
        }

        /// <summary>
        /// Redraw when focused
        /// </summary>
        private void ImageCanvas_GotFocus(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
