using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        object lockobject = new object();
        int szint;
        int maxTask = 3;
        int taskSzam = 0;
        public ParhuzamosJatekos(string name, Szin szin, Jatekos ellenfel) : base(name, szin)
        {
            this.ellenfel = ellenfel;
        }

        public Point[] LepesKiszamit(int szint, int[,] tabla, bool aktualisjatekos)
        {
            Point[] fromTo = new Point[2];
            this.szint = szint;
            int[,] temp = Util.CreateTemp(tabla);
            //Kereses(szint, temp,aktualisjatekos);
            Lepes vegso = ParhuzamosKereses(szint, tabla, aktualisjatekos);
            fromTo[0] = new Point(vegso.fromx, vegso.fromy);
            fromTo[1] = new Point(vegso.x,vegso.y);
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

        Lepes ParhuzamosKereses(int szint, int[,] tabla, bool ai)
        {

            Lepes vegso = new Lepes(-1, -1, null);
            vegso = Kereses(szint, tabla, ai);
            return vegso;
        }
        Lepes Kereses(int szint, int[,] tabla, bool ai)
        {
            if (szint == 0)
            {
                return new Lepes(tablaKiertekel(tabla));
            }
            Lepes legjobbErtek = new Lepes(ai ? 9999 : -9999);
            List<Point[]> lehetsegesLepesek = new List<Point[]>();
            if (ai)
            {
                foreach (var item in Babuk)
                {
                    foreach (var lepes in item.LehetsegesLepesek(tabla))
                    {
                        Point[] temp = new Point[2] { new Point(item.Xpozicio, item.Ypozicio), lepes };
                        lehetsegesLepesek.Add(temp);
                    }
                }
            }
            else
            {
                foreach (var item in ellenfel.Babuk)
                {
                    foreach (var lepes in item.LehetsegesLepesek(tabla))
                    {
                        Point[] temp = new Point[2] { new Point(item.Xpozicio, item.Ypozicio), lepes };
                        lehetsegesLepesek.Add(temp);
                    }
                }
            }
            Queue<Task> workers = new Queue<Task>();

            foreach (var item in lehetsegesLepesek)
            {
                Lepes jelenlegi = new Lepes((int)item[1].X, (int)item[1].Y, (int)item[0].X, (int)item[0].Y);
                if (taskSzam < maxTask && szint > 0)
                {
                    Interlocked.Increment(ref taskSzam);
                    workers.Enqueue(Task.Factory.StartNew(() =>
                    {
                        jelenlegi = Kereses(szint - 1, UjAllapot(jelenlegi, tabla, ai), !ai);
                        lock (lockobject)
                        {
                            if (ai)
                            {
                                if (jelenlegi.ertek < legjobbErtek.ertek)
                                {
                                    legjobbErtek = jelenlegi;
                                }
                            }
                            else
                            {
                                if (jelenlegi.ertek > legjobbErtek.ertek)
                                {
                                    legjobbErtek = jelenlegi;
                                }
                            }
                        }
                    }
                    ));
                }
                else
                {
                    jelenlegi = Kereses(szint - 1, UjAllapot(jelenlegi, tabla, ai), !ai);
                    lock (lockobject)
                    {
                        if (ai)
                        {
                            if (jelenlegi.ertek < legjobbErtek.ertek)
                            {
                                legjobbErtek = jelenlegi;
                            }
                        }
                        else
                        {
                            if (jelenlegi.ertek > legjobbErtek.ertek)
                            {
                                legjobbErtek = jelenlegi;
                            }
                        }
                    }
                }
            }

            Task.WaitAll(workers.ToArray());
            return legjobbErtek;


        }

        private int[,] UjAllapot(Lepes jelenlegi, int[,] tabla, bool ai)
        {
            throw new NotImplementedException();
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
                    if (tabla[i, j] > 0)
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
                if (tabla[1, i] != -10)
                {
                    ertek -= 10;
                }
            }
            return ertek;
        }

        Point IdeigLenesenMozgat(Babu babu, Point to, int[,] tabla)
        {
            Point honnan = new Point(babu.Xpozicio, babu.Ypozicio);
            tabla[babu.Xpozicio, babu.Ypozicio] = 0;
            babu.Xpozicio = (int)to.X;
            babu.Ypozicio = (int)to.Y;
            tabla[babu.Xpozicio, babu.Ypozicio] = (int)babu.tipus * (int)babu.Szin;
            return honnan;
        }
        void visszaAllit(Babu visszamozgat, Babu leutott, Point hova, int[,] tabla, Jatekos jatekos)
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


    public class Lepes
    {
        public int x, y;
        public int fromx, fromy;
        public int? ertek;

        public Lepes(int x, int y)
        {
            this.x = x;
            this.y = y;
            ertek = null;
        }
        public Lepes(int x, int y,int fromx,int fromy)
        {
            this.x = x;
            this.y = y;
            this.fromx = fromx;
            this.fromy = fromy;
            ertek = null;
        }
        public Lepes(int? ertek)
        {
            x = y = -1;
            this.ertek = ertek;
        }
        public Lepes(int x, int y, int? ertek)
        {
            this.x = x;
            this.y = y;
            this.ertek = ertek;
        }
    }
}