using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace IFOProject.CustomControls
{
    public class Plot : ZedGraphControl
    {
        private const double margin = 0.1;

        public Plot() : base()
        {
            GraphPane.XAxis.MajorGrid.IsVisible = true;
            GraphPane.YAxis.MajorGrid.IsVisible = true;
            IsAntiAlias = true;
        }

        public override void AxisChange()
        {
            // general bounds
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = 0;
            double maxY = 0;
            // find general bounds
            foreach (var curve in GraphPane.CurveList)
            {
                double _minX, _minY, _maxX, _maxY;
                curve.GetRange(out _minX, out _maxX, out _minY, out _maxY, true, false, GraphPane);
                if (_minX < minX) minX = _minX;
                if (_minY < minY) minY = _minY;
                if (_maxX > maxX) maxX = _maxX;
                if (_maxY > maxY) maxY = _maxY;
            }
            // graph with and height
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;
            // set margins
            GraphPane.XAxis.Scale.Min = minX - margin * rangeX;
            GraphPane.XAxis.Scale.Max = maxX + margin * rangeX;
            GraphPane.YAxis.Scale.Min = minY - margin * rangeY;
            GraphPane.YAxis.Scale.Max = maxY + margin * rangeY;
            // update plot
            base.AxisChange();
        }
    }
}
