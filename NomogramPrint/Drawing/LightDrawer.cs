using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NomogramPrint.Models;
using System.Drawing;
using NomogramPrint.Settings;

namespace NomogramPrint.Drawing
{
    public class LightDrawer
    {
        public LightDrawer()
        {

        }

        private PointF GetTopDiagonalPoint(PointF bottomPoint) =>
                    new PointF(bottomPoint.X + bottomPoint.Y, 0);

        public void DrawLine(float left, Graphics graphics, DrawSettings drawSettings, Light light) 
        {
            var settings = drawSettings.LightSettings;
            var currentY = settings.Top;

            using (var pen = new Pen(settings.Color, 1))
                graphics.DrawLine(pen, left, settings.Top, left, settings.Top + settings.Height);

        }

        public void DrawDiagonal(float left, Graphics graphics, DrawSettings drawSettings)
        {
            var settings = drawSettings.LightSettings;

            var bottomPoint = new PointF(left, settings.Top);
            var topPoint = GetTopDiagonalPoint(bottomPoint);
            using (var pen = new Pen(settings.Color, 1))
                graphics.DrawLine(pen, bottomPoint, topPoint);
        }

        public void DrawCoordinates(float left, Graphics graphics, DrawSettings drawSettings, Light light)
        {
            var kmSettings = drawSettings.KilometerSettings;
            //var cell = kmSettings.Cells[0];
            var y = kmSettings.Top + kmSettings.Cells[0].WrapHeight * kmSettings.Height;
            var point = new PointF(left, y);
            var size = new SizeF(kmSettings.Width, kmSettings.Cells[1].WrapHeight * kmSettings.Height);

            var state = graphics.Save();

            graphics.TranslateTransform(point.X - size.Width /4, point.Y + size.Height / 2);
            graphics.RotateTransform(270);

            var font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular);
            SizeF textSize = graphics.MeasureString(light.Coordinate.ToString(), font);
            graphics.DrawString(light.Coordinate.ToString(), font, Brushes.Blue, -(textSize.Width / 2), -(textSize.Height / 2));
            graphics.Restore(state);
        }

    }
}
