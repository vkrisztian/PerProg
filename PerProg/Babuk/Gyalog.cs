using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    sw.Write(palya[i, j]+"\t");
                }
                sw.WriteLine();
            }
            sw.Close();

            
            if(LehetsegesLepes(x,y,palya))
            {
                return true;
            }
            
            return false;
        }

        public override bool LehetsegesLepes(int x, int y, int[,] palya)
        {
            bool lephet = false;
            if (this.Szin == Szin.fekete)
            {
                if (this.Xpozicio + 1 == x && this.Ypozicio == y && palya[x, y] == 0)
                {
                    lephet = true;
                }
                else if (Math.Abs(this.Xpozicio - x) == 1 && Math.Abs(this.Ypozicio - y) == 1)
                {
                    int dy = y - this.Ypozicio;
                    if (palya[this.Xpozicio + 1, this.Ypozicio + dy] != 0 && palya[x, y] * (int)this.Szin < 0)
                    {
                        lephet = true;
                    }
                }
            }
            else
            {
                if (this.Xpozicio - 1 == x && this.Ypozicio == y && palya[x, y] == 0 && x >= 0 && y >= 0)
                {
                    lephet = true;
                }
                else if (Math.Abs(this.Xpozicio - x) == 1 && Math.Abs(this.Ypozicio - y) == 1)
                {
                    int dy = y - this.Ypozicio;
                    if (palya[this.Xpozicio - 1, this.Ypozicio + dy] != 0 && palya[x, y] * (int)this.Szin < 0)
                    {
                        lephet = true;
                    }
                }
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
