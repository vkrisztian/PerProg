﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public class Futo :Babu
    {

        public Futo(int x, int y,Szin szin) : base(x, y, szin)
        {
            this.tipus = BabuTipus.futo;
        }
        public override bool Lep(int[,] palya, int x, int y)
        {
            return true;
        }
    }
}
