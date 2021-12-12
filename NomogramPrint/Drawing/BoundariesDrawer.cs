using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NomogramPrint.Models;
using NomogramPrint.Settings;

namespace NomogramPrint.Drawing
{
    public class BoundariesDrawer
    {

        private PointF GetTopDiagonalPoint(PointF bottomPoint) =>
         new PointF(bottomPoint.X + bottomPoint.Y, 0);

        public void DrawLine(float left, Graphics graphics, DrawSettings drawSettings, StationBoundaries boundaries)
        {
            var settings = drawSettings.StationBoundariesSettings;

            using (var pen = new Pen(settings.Color, 2))
            {
                pen.DashPattern = settings.DashPattern;
                graphics.DrawLine(pen, left, settings.Top, left, settings.Top + settings.Height);
            }
                

        }

        public void DrawDiagonal(float left, Graphics graphics, DrawSettings drawSettings)
        {
            var settings = drawSettings.StationBoundariesSettings;

            var bottomPoint = new PointF(left, settings.Top);
            var topPoint = GetTopDiagonalPoint(bottomPoint);
            using (var pen = new Pen(settings.Color, 2))
                graphics.DrawLine(pen, bottomPoint, topPoint);
        }

    }

 

}
