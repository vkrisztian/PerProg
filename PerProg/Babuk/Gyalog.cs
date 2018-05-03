using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    class Gyalog : Babu
    {
        public Gyalog(int x, int y, Szin szin) : base(x, y, szin)
        {
            this.tipus = BabuTipus.gyalog;
        }

        public override bool Lep(int[,] palya, int x, int y)
        {
            return true;
        }
    }
}
