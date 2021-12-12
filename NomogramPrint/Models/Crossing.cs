using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomogramPrint.Models
{
    // Переезды без дежурного
    public class Crossing : IObject
    {
        public int Kilometers { get; set; }
        public int Meters { get; set; }
    }
}
