using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomogramPrint.Models
{
    public class Light
    {

        public string Coordinate { get; }

        public Light(int kilometers, int meters)
        {
            Kilometers = kilometers;
            Meters = meters;
            Coordinate = GetCoordinate();
        }

        public int Kilometers { get; set; }
        public int Meters { get; set; }


        private string GetCoordinate()
        {
            var major = (int)Math.Ceiling((double)Meters / 100d);
            var minor = Meters % 100;
            return major + "+" + minor;
        }
    }
}
