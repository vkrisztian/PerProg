using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PerProg
{
    class ParhuzamosJatekos : Jatekos
    {

        public ParhuzamosJatekos(string name, Szin szin) : base(name, szin)
        {

        }

        public Point[] LepesKiszamit(int[,]tabla,Jatekos feher)
        {
            List<List<Point>> osszesLehetsegesLepes = new List<List<Point>>();
            Point[] fromTo = new Point[2];
            foreach (var item in Babuk)
            {
                osszesLehetsegesLepes.Add(item.LehetsegesLepesek(tabla));
            }

            return fromTo;
        }
        bool SakkbaLep(int x, int y, Babu babu, Jatekos feher, int[,] tabla)
        {
            bool megmindigSakk = true;
            int tempx = babu.Xpozicio;
            int tempy = babu.Ypozicio;
            babu.Xpozicio = x;
            babu.Ypozicio = y;
            int[,] temp = Util.CreateTemp(tabla);
            temp[tempx, tempy] = 0;
            temp[x, y] = (int)babu.tipus * (int)babu.Szin;
            Babu ideiglenesenLeutott = IdeiglenesLeut(x, y,feher);
            if (feher.SakkTesz(this.GetKiraly(), temp))
            {
                megmindigSakk = true;
            }
            else
            {
                megmindigSakk = false;
            }
            if (ideiglenesenLeutott != null)
            {
                feher.Babuk.Add(ideiglenesenLeutott);
            }
            babu.Xpozicio = tempx;
            babu.Ypozicio = tempy;

            return megmindigSakk;
        }

        Babu IdeiglenesLeut(int x, int y, Jatekos feher)
        {
            Babu temp = null;
            foreach (var item in feher.Babuk)
            {
                if (item.Xpozicio == x && item.Ypozicio == y)
                {
                    temp = item;
                }
            }
            if (temp != null)
                feher.Babuk.Remove(temp);

            return temp;
        }
    }
}
