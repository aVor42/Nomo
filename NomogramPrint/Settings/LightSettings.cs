using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NomogramPrint.Settings
{
    public class LightSettings
    {
        public LightSettings() :
            this(0f, 0f, 0f, Color.Black)
        { }

        public LightSettings(float width, float height, float top, Color color)
        {
            Color = color;
            Width = width;
            Height = height;
            Top = top;
        }

        public Color Color { get; set; }
        public float Top { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }
}
