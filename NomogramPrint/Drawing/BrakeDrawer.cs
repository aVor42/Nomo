using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NomogramPrint.Settings;
using NomogramPrint.Models;

namespace NomogramPrint.Drawing
{
    public class BrakeDrawer
    {
        public BrakeDrawer()
        {
        }

        public void DrawTreangle(float left, Graphics graphics, DrawSettings drawSettings, BrakeTest brake)
        {

            var kmSettings = drawSettings.KilometerSettings;
            //var cell = kmSettings.Cells[0];
            var y = kmSettings.Top;
            var point = new PointF(left, y);

            graphics.DrawImage(Resource1.treangle, point);
        }

        public void DrawText(float left, Graphics graphics, DrawSettings drawSettings, BrakeTest brake)
        {

            var major = (int)Math.Ceiling((double)brake.Meters / 100d) + 1;

            var text = $"Груз {brake.Kilometers + (int)Math.Ceiling((double)brake.Meters / 1000d)}пк{major}-{brake.MaxVelocity}-{brake.MaxDistance}";
            var kmSettings = drawSettings.KilometerSettings;
            var settings = drawSettings.BrakeSettings;
            //var cell = kmSettings.Cells[0];
            var y = kmSettings.Top;
            var point = new PointF(left, y);
            var font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);
            
            var size = graphics.MeasureString(text.ToString(), font);

            var state = graphics.Save();

            graphics.TranslateTransform(point.X, point.Y + size.Height / 2);
            graphics.RotateTransform(315);

            SizeF textSize = graphics.MeasureString(text.ToString(), font);
            graphics.DrawString(text.ToString(), font, Brushes.Brown, 3 * textSize.Height, 0);
            graphics.Restore(state);
        }

    }
}
