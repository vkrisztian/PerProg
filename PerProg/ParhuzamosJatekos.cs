using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        int maxTask = 4;
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
            List<ObservableCollection<Babu>> babuk = new List<ObservableCollection<Babu>>();
            babuk.Add(this.Babuk);
            babuk.Add(ellenfel.Babuk);
            vegso = Kereses(szint, tabla, ai,babuk);
            return vegso;
        }
        Lepes Kereses(int szint, int[,] tabla, bool ai,List<ObservableCollection<Babu>>babuk)
        {
            if (szint == 0)
            {
                return new Lepes(tablaKiertekel(tabla));
            }
            Lepes legjobbErtek = new Lepes(ai ? 9999 : -9999);
            List<Point[]> lehetsegesLepesek = new List<Point[]>();
            if (ai)
            {
                foreach (var item in babuk[0])
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
                foreach (var item in babuk[1])
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
                        jelenlegi.ertek = Kereses(szint - 1, UjAllapot(jelenlegi, tabla), !ai,ujBabuk(babuk,jelenlegi,ai)).ertek;
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
                    jelenlegi.ertek = Kereses(szint - 1, UjAllapot(jelenlegi, tabla), !ai, ujBabuk(babuk, jelenlegi,ai)).ertek;
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
            if (legjobbErtek.x == -1)
            {
                legjobbErtek = new Lepes((int)lehetsegesLepesek.First()[1].X, (int)lehetsegesLepesek.First()[1].Y, (int)lehetsegesLepesek.First()[0].X, (int)lehetsegesLepesek.First()[0].Y);
            }
            return legjobbErtek;


        }

        private List<ObservableCollection<Babu>> ujBabuk(List<ObservableCollection<Babu>> babuk, Lepes jelenlegi,bool ai)
        {
            ObservableCollection<Babu> temp = new ObservableCollection<Babu>(babuk[0]);
            ObservableCollection<Babu> temp2 = new ObservableCollection<Babu>(babuk[1]);
           
            if (ai)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].Xpozicio == jelenlegi.fromx && temp[i].Ypozicio == jelenlegi.fromy)
                    {
                        temp[i].Xpozicio = jelenlegi.fromx;
                        temp[i].Ypozicio = jelenlegi.fromy;
                    }
                }
                for (int i = 0; i < temp2.Count; i++)
                {
                    if (temp2[i].Xpozicio == jelenlegi.x && temp2[i].Ypozicio == jelenlegi.y)
                    {
                        temp2[i].aktiv = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < temp2.Count; i++)
                {
                    if (temp2[i].Xpozicio == jelenlegi.fromx && temp2[i].Ypozicio == jelenlegi.fromy)
                    {
                        temp2[i].Xpozicio = jelenlegi.fromx;
                        temp2[i].Ypozicio = jelenlegi.fromy;
                    }
                }
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].Xpozicio == jelenlegi.x && temp[i].Ypozicio == jelenlegi.y)
                    {
                        temp2[i].aktiv = false;
                    }
                }
            }
            List<ObservableCollection<Babu>> ujbabuk = new List<ObservableCollection<Babu>>();
            ujbabuk.Add(temp);
            ujbabuk.Add(temp2);
            return ujbabuk;
        }

        private int[,] UjAllapot(Lepes jelenlegi, int[,] tabla)
        {
            int[,] temp = Util.CreateTemp(tabla);
            temp[jelenlegi.x, jelenlegi.y] = temp[jelenlegi.fromx, jelenlegi.fromy];
            temp[jelenlegi.fromx, jelenlegi.fromy] = 0;
            return temp;
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