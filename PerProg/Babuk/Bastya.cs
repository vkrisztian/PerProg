using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public class Bastya : Babu
    {
        public Bastya(int x, int y,Szin szin) : base(x,y,szin)
        {
            this.tipus = BabuTipus.bastya;
        }
        public override bool Lep(int[,] palya, int x, int y,bool sakk)
        {
            bool lepett = false;
            if (LehetsegesLepes(x, y, palya))
            {
                if (palya[x, y] == 0 || palya[x,y]*(int)this.Szin < 0)
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

        public override bool   LehetsegesLepes(int x, int y, int[,] palya)
        {
            bool lephet = false;
            if ((this.Xpozicio == x || this.Ypozicio==y) && (this.Xpozicio != x || this.Ypozicio != y))
            {
                int yi = y > this.Ypozicio ? 1 : -1;
                int xi = x > this.Xpozicio ? 1 : -1;
                if ((this.Xpozicio == x))
                {
                    xi = 0;
                }
                else if ((this.Ypozicio == y))
                {
                    yi = 0;
                }
                 
                

                int tx = xi;
                int ty = yi;
                while (this.Xpozicio + tx != x)
                {
                    if (palya[this.Xpozicio + tx, this.Ypozicio + ty] != 0)
                    {
                        return false;
                    }
                    tx += xi;
                    ty += yi;
                }

                lephet = true;
            }
            return lephet;
        }
    }
}
