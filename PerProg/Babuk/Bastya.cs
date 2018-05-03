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
        public override bool Lep(int[,] palya, int x, int y)
        {
            return true; 
        }
    }
}
