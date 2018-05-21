using System;
using System.Collections.Concurrent;
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
        bool keresesVege = false;
        object lockobject;
        int szint;
        ConcurrentBag<int[,]> tablestates = new ConcurrentBag<int[,]>();
        public ParhuzamosJatekos(string name, Szin szin,Jatekos ellenfel) : base(name, szin)
        {
            this.ellenfel = ellenfel;
        }

        public Point[] LepesKiszamit(int szint,int[,]tabla,bool aktualisjatekos)
        {
            Point[] fromTo = new Point[2];
            this.szint = szint;
            int[,] temp = Util.CreateTemp(tabla);
            //Kereses(szint, temp,aktualisjatekos);
            ParhuzamosKereses(szint, tabla, aktualisjatekos);
            fromTo[0] = new Point(vegsoLepo.Xpozicio, vegsoLepo.Ypozicio);
            fromTo[1] = vegsolepes;
           // fromTo[0] = defaultlepes[0];
            //fromTo[1] = defaultlepes[1];
            return fromTo;
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

        int ParhuzamosKereses(int szint, int [,] tabla, bool ai)
        {
            int ertek = 0;
            List<Task> workers = new List<Task>();
            for (int i = 0; i < 4; i++)
            {
                Task t = new Task(() => SlaveKeres());
                workers.Add(t);
            }

            Task.WaitAll(workers.ToArray());
            return ertek;
        }
        int  Kereses(int szint, int[,] tabla, bool ai)
        {
            if (szint == 0)
            {
                return tablaKiertekel(tabla);
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
                        int ertek =  Kereses(szint - 1, tabla, !ai);
                        ertek += szint;
                        if (legjobbErtek> ertek)
                        {
                            lock (lockobject)
                            {
                                legjobbErtek = ertek;
                                vegsolepes = lepes;
                                vegsoLepo = item;
                            }
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
                        int ertek = Kereses(szint - 1, tabla,!ai);
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

        void SlaveKeres()
        {
            int [,] ertek;
            while (!keresesVege)
            {
                if (tablestates.TryTake(out ertek))
                {
                    Kereses(szint, ertek, false);
                }
            }
        }

        private int tablaKiertekel(int[,] tabla)
        {
            int ertek = 0;
            int aibabuk = 0;
            int ellenfelbabuk = 0;
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (tabla[i,j] > 0)
                    {
                        ellenfelbabuk += tabla[i, j];
                    }
                    else
                    {
                        aibabuk -= tabla[i, j];
                    }
                }
            }
            ertek += Math.Abs(aibabuk) - ellenfelbabuk;
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                if (tabla[1,i] != -10)
                {
                    ertek -= 10;
                }
            }
            return ertek;
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
