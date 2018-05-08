using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    class Kiraly : Babu
    {
        public Kiraly(int x, int y, Szin szin) : base(x, y, szin)
        {
            this.tipus = BabuTipus.kiraly;
        }
        public override bool Lep(int[,] palya, int x, int y)
        {
            bool lepett = false;
            if (LehetsegesLepes(x,y,palya))
            {
                if (palya[x,y]== 0 || palya[x, y] * (int)this.Szin < 0)
                {
                    lepett = true;
                }
            }
            return lepett;
        }

        public override bool LehetsegesLepes(int x, int y,int[,] palya)
        {
            bool lephet = false;
            if (Math.Abs(this.Xpozicio -x) <=1 && Math.Abs(this.Ypozicio-y) <=1)
            {
                lephet = true;
            }
            return lephet;
        }
    }
}
