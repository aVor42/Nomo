using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomogramPrint.Models
{
    public class DbContext
    {

        public DbContext()
        {
            Kilometers = new List<Kilometer>();
            Objects = new List<IObject>();
            Lights = new List<Light>();
            StationsBoundaries = new List<StationBoundaries>();
        }

        public List<Kilometer> Kilometers { get; set;}
        public List<IObject> Objects { get; set; }
        public List<Light> Lights { get; set; }
        public List<StationBoundaries> StationsBoundaries { get; set; }
        public List<BrakeTest> BrakeTests { get; set; }
    }
}
