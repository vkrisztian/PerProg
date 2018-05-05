using System;
using System.Collections.Generic;
using System.IO;
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
            StreamWriter sw = new StreamWriter("log.txt");
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    sw.Write(palya[i, j]);
                }
                sw.WriteLine();
            }
            sw.Close();

            if (this.Szin == Szin.fekete)
            {
                if (this.Xpozicio+1 == x && this.Ypozicio==y && palya[x, y] == 0 && x <= 7)
                {
                    palya[this.Xpozicio, this.Ypozicio] = 0;
                    palya[x, y] = (int)this.tipus * (int)this.Szin;
                    this.Xpozicio = x;
                    this.Xpozicio = y;
                 
                    return true;
                }
                else if (palya[this.Xpozicio + 1, this.Ypozicio - 1] != 0 || palya[this.Xpozicio+1,this.Ypozicio+1] !=0 && Math.Abs(this.Xpozicio-x)==1 && Math.Abs(this.Ypozicio - y) == 1 && x <= 7 && y <= 7 && y >= 0)
                {
                    //TODO Leut;
                    return true;
                }
            }
            else
            {
                if (this.Xpozicio-1 == x && this.Ypozicio == y && palya[x, y] == 0 && x >= 0 && y >= 0)
                {
                    this.Xpozicio = x;
                    this.Xpozicio = y;
                    return true;
                }   
                else if (palya[this.Xpozicio - 1, this.Ypozicio - 1] != 0 && palya[this.Xpozicio - 1, this.Ypozicio + 1] != 0 && Math.Abs(this.Xpozicio - x) == 1 && Math.Abs(this.Ypozicio - y) == 1 && y <= 7 && y >= 0 && x >= 0)
                {
                    //TODO Leut;
                    return true;
                }
            }
            
            return false;
        }
    }
}
