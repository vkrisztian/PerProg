﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public class Lo : Babu
    {
        public Lo(int x, int y, Szin szin) : base(x, y, szin)
        {
            this.tipus = BabuTipus.lo;
        }

        public override bool Lep(int[,] palya, int x, int y)
        {
            bool lepett = false;
            if (LehetsegesLepes(x,y))
            {
                if (palya[x,y] == 0)
                {
                    palya[this.Xpozicio, this.Ypozicio] = 0;
                    palya[x, y] = (int)this.tipus * (int)this.Szin;
                    this.Xpozicio = x;
                    this.Ypozicio = y;
                    lepett = true;
                }
            }
            return lepett;
        }

        bool LehetsegesLepes(int x,int y)
        {
            bool lephet = false;
            if ((Math.Abs(this.Xpozicio - x) == 1 && Math.Abs(this.Ypozicio-y)==2) || (Math.Abs(this.Xpozicio - x) == 2 && Math.Abs(this.Ypozicio - y) == 1))
            {
                lephet = true;
            }
            return lephet;
        }
    }
}
