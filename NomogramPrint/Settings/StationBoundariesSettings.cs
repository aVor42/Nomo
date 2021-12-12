using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NomogramPrint.Settings
{
    public class StationBoundariesSettings
    {
        public StationBoundariesSettings() :
            this(0f, 0f, 0f, Color.Black, new float[] {10, 4})
        { }

        public StationBoundariesSettings(float width, float height, float top, Color color, float[] dashPattern)
        {
            Color = color;
            Width = width;
            Height = height;
            Top = top;
            DashPattern = dashPattern;
        }

        public Color Color { get; set; }
        public float Top { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float[] DashPattern { get; set; }
    }
}
