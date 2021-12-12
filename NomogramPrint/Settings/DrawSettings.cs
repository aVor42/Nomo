using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NomogramPrint.Settings
{
    public class DrawSettings
    {

        public DrawSettings():
            this(new KilometerSettings(), new LightSettings(), new StationBoundariesSettings(), new BrakeSettings())
        { }

        public DrawSettings(KilometerSettings kilometerSettings, 
            LightSettings lightSettings, 
            StationBoundariesSettings stationBoundariesSettings,
            BrakeSettings brakeSettings)
        {
            KilometerSettings = kilometerSettings;
            LightSettings = lightSettings;
            StationBoundariesSettings = stationBoundariesSettings;
            BrakeSettings = brakeSettings;
        }



        public  KilometerSettings KilometerSettings { get; set; }

        public  LightSettings LightSettings { get; set; }

        public  StationBoundariesSettings StationBoundariesSettings { get; set; }

        public BrakeSettings BrakeSettings { get; set; }

        public void SetAllSameColor(Color color)
        {
            KilometerSettings.Color = color;
            LightSettings.Color = color;
        }


    }
}
