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
        Point vegsolepes;
        Babu vegsoLepo;
        Jatekos ellenfel;
        public ParhuzamosJatekos(string name, Szin szin,Jatekos ellenfel) : base(name, szin)
        {
            this.ellenfel = ellenfel;
        }

        public Point[] LepesKiszamit(int szint,int[,]tabla,bool aktualisjatekos)
        {
            Point[] fromTo = new Point[2];
            Point[] defaultlepes = new Point[2];
            foreach (var item in Babuk)
            {
                foreach (var lepes in item.LehetsegesLepesek(tabla))
                {
                    defaultlepes[0] = new Point((int)item.Xpozicio, (int)item.Ypozicio);
                    defaultlepes[1] = lepes;
                    break;
                }   
            }
            Kereses(szint, tabla,defaultlepes[0],defaultlepes[1],aktualisjatekos);
            fromTo[0] = new Point(vegsoLepo.Xpozicio, vegsoLepo.Ypozicio);
            fromTo[1] = vegsolepes;
            //fromTo[0] = defaultlepes[0];
            //fromTo[1] = defaultlepes[1];
            return fromTo;
        }
        int LepesErtekel(int x, int y, Point from, Jatekos feher, int[,] tabla)
        {
            Babu babu = null;
            foreach (var item in feher.Babuk)
            {
                if (item.Xpozicio == (int)from.X && item.Ypozicio == (int)from.Y)
                {
                    babu = item;
                }
            }
            int ertek = 0;
            int tempx = babu.Xpozicio;
            int tempy = babu.Ypozicio;
            babu.Xpozicio = x;
            babu.Ypozicio = y;
            ertek = tabla[x, y];
            tabla[tempx, tempy] = 0;
            tabla[x, y] = (int)babu.tipus * (int)babu.Szin;
            Babu ideiglenesenLeutott = IdeiglenesLeut(x, y,feher);

            //sakkba lépne invalid lépés negativ érték
            if (feher.SakkTesz(this.GetKiraly(), tabla))
            {
                ertek = -1;
            }
            if (ideiglenesenLeutott != null)
            {
                ideiglenesenLeutott.aktiv = true;
            }
            babu.Xpozicio = tempx;
            babu.Ypozicio = tempy;

            return ertek;
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
                temp.aktiv = false;

            return temp;
        }

        int  Kereses(int szint, int[,] tabla, Point from, Point to,bool ai)
        {
            if (szint == 0)
            {
               return LepesErtekel((int)to.X, (int)to.Y, from, this, tabla);
            }
            if (ai)
            {
                int legjobbErtek = 9999;
                foreach (var item in this.Babuk)
                {
                    foreach (var lepes in item.LehetsegesLepesek(tabla))
                    {
                        Babu temp = IdeiglenesLeut((int)lepes.X, (int)lepes.Y,ellenfel);
                        Point honnan = IdeigLenesenMozgat(item, lepes, tabla);
                        Point ideiglenesTo = new Point(item.Xpozicio, item.Ypozicio);
                        int ertek =  Kereses(szint - 1, tabla,new Point(item.Xpozicio,item.Ypozicio),lepes, !ai);
                        if (legjobbErtek> ertek)
                        {
                            legjobbErtek = ertek;
                            vegsolepes = lepes;
                            vegsoLepo = item;
                        }
                        visszaAllit(item, temp, honnan,tabla,ellenfel);
                    }
                }
                return legjobbErtek;
            }
            else
            {
                int legjobbErtek = -9999;
                foreach (var item in ellenfel.Babuk)
                {
                    foreach (var lepes in item.LehetsegesLepesek(tabla))
                    {
                        Babu temp = IdeiglenesLeut((int)lepes.X, (int)lepes.Y, this);
                        Point honnan = IdeigLenesenMozgat(item, lepes, tabla);
                        Point ideiglenesTo = new Point(item.Xpozicio, item.Ypozicio);
                        int ertek = Kereses(szint - 1, tabla, honnan, ideiglenesTo, !ai);
                        if (legjobbErtek< ertek)
                        {
                            legjobbErtek = ertek;
                        }
                        visszaAllit(item, temp, honnan, tabla, this);

                    }
                }
                return legjobbErtek;
            }

        }

        Point IdeigLenesenMozgat(Babu babu,Point to,int [,] tabla)
        {
            Point honnan = new Point(babu.Xpozicio, babu.Ypozicio);
            tabla[babu.Xpozicio, babu.Ypozicio] = 0;
            babu.Xpozicio = (int)to.X;
            babu.Ypozicio = (int)to.Y;
            tabla[babu.Xpozicio, babu.Ypozicio] = (int)babu.tipus * (int)babu.Szin;
            return honnan;
        }
        void visszaAllit(Babu visszamozgat, Babu leutott,Point hova,int[,] tabla,Jatekos jatekos)
        {
            int tempx = visszamozgat.Xpozicio;
            int tempy = visszamozgat.Ypozicio;
            visszamozgat.Xpozicio = (int)hova.X;
            visszamozgat.Ypozicio = (int)hova.Y;
            tabla[(int)hova.X, (int)hova.Y] = (int)visszamozgat.tipus * (int)visszamozgat.Szin;
            if (leutott != null)
            {
                leutott.aktiv = true;
                tabla[tempx, tempy] = (int)leutott.tipus * (int)leutott.Szin;
            }
            else
            {
                tabla[tempx, tempy] = 0;
            }
           
        }

    }
}
