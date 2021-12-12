using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NomogramPrint.Models;
using NomogramPrint.Settings;

namespace NomogramPrint.Drawing
{
    public class Drawer
    {
        public Drawer():
            this(new DrawSettings())
        { }

        public Drawer(DrawSettings drawSettings)
        {
            DrawSettings = drawSettings;
        }

        public DrawSettings DrawSettings { get; set; }

        public void DrawKilometers(Graphics graphics, List<Kilometer> kilometers)
        {
            for(int i = 0; i < kilometers.Count; i++)
            {
                var left = DrawSettings.KilometerSettings.Width * i;
                var kilometerDrawer = new KilometerDrawer();
                
                kilometerDrawer.DrawCell(left, graphics, DrawSettings, kilometers[i]);
                kilometerDrawer.DrawDiagonal(left, graphics, DrawSettings);
            }
        }

        public void DrawLights(Graphics graphics, List<Kilometer> kilometers, List<Light> lights)
        {
            // Да, максимальный находится в начале
            var maxKilometer = kilometers[0].Number;

            for(int i = 0; i < lights.Count; i++)
            {
                var numberLight = maxKilometer - lights[i].Kilometers;
                var left = numberLight * DrawSettings.KilometerSettings.Width - 
                    (DrawSettings.KilometerSettings.Width * lights[i].Meters / 1000f);
                var lightDrawer = new LightDrawer();

                lightDrawer.DrawLine(left, graphics, DrawSettings, lights[i]);
                lightDrawer.DrawDiagonal(left, graphics, DrawSettings);
                lightDrawer.DrawCoordinates(left, graphics, DrawSettings, lights[i]);
            }


        }


        public void DrawBoundaries(Graphics graphics, List<Kilometer> kilometers, List<StationBoundaries> stationsBoundaries)
        {
            // Да, максимальный находится в начале
            var maxKilometer = kilometers[0].Number;

            for (int i = 0; i < stationsBoundaries.Count; i++)
            {
                var inNumberBound = maxKilometer - stationsBoundaries[i].InKilometers;
                var outNumberBound = maxKilometer - stationsBoundaries[i].OutKilometers;
                var leftBoundraryY = inNumberBound * DrawSettings.KilometerSettings.Width -
                    (DrawSettings.KilometerSettings.Width * stationsBoundaries[i].InMeters / 1000f);
                var rightBoundaryY = outNumberBound * DrawSettings.KilometerSettings.Width -
                    (DrawSettings.KilometerSettings.Width * stationsBoundaries[i].OutMeters / 1000f);
                var boundariesDrawer = new BoundariesDrawer();

                boundariesDrawer.DrawLine(leftBoundraryY, graphics, DrawSettings, stationsBoundaries[i]);
                boundariesDrawer.DrawDiagonal(leftBoundraryY, graphics, DrawSettings);

                boundariesDrawer.DrawLine(rightBoundaryY, graphics, DrawSettings, stationsBoundaries[i]);
                boundariesDrawer.DrawDiagonal(rightBoundaryY, graphics, DrawSettings);
            }


        }

        public void DrawBrakes(Graphics graphics, List<Kilometer> kilometers, List<BrakeTest> brakes)
        {
            // Да, максимальный находится в начале
            var maxKilometer = kilometers[0].Number;

            for (int i = 0; i < brakes.Count; i++)
            {
                var numberbrake = maxKilometer - brakes[i].Kilometers - (int)Math.Ceiling(brakes[i].Meters / 1000d);
                var left = numberbrake * DrawSettings.KilometerSettings.Width;
                var brakeDrawer = new BrakeDrawer();

                brakeDrawer.DrawTreangle(left, graphics, DrawSettings, brakes[i]);
                brakeDrawer.DrawText(left, graphics, DrawSettings, brakes[i]);
            }
        }



    }
}
