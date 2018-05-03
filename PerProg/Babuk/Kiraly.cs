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
            return true;
        }
    }
}
