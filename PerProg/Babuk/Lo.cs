using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (LehetsegesLepes(x,y,palya) )
            {
                if (palya[x,y] == 0 || palya[x, y] * (int)this.Szin < 0)
                {
                   
                    lepett = true;
                }
            }
            return lepett;
        }

       public override bool LehetsegesLepes(int x,int y,int [,] palya)
        {
            bool lephet = false;
            if ((Math.Abs(this.Xpozicio - x) == 1 && Math.Abs(this.Ypozicio-y)==2) || (Math.Abs(this.Xpozicio - x) == 2 && Math.Abs(this.Ypozicio - y) == 1))
            {
                lephet = true;
            }
            return lephet;
        }

        public override List<Point> LehetsegesLepesek(int[,] palya)
        {
            List<Point> lepesek = new List<Point>();
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (Lep(palya, i, j))
                    {
                        lepesek.Add(new Point(i, j));
                    }
                }
            }
            return lepesek;
        }
    }
}
