﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomogramPrint.Models
{

    // Переезд с дежурным
    public class CrossingWithControll: IObject
    {
        public int Kilometers { get; set; }
        public int Meters { get; set; }
    }
}
