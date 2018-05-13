using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PerProg
{
    public enum BabuTipus { gyalog = 10,bastya = 50,futo = 30, lo = 31,kiraly = 900,kiralyno = 90 }
    public enum Szin { fekete=-1,feher=1}
    public abstract class Babu : Bindable
    {
        int xpozicio;
        int ypozicio;
        public bool aktiv;
        public BabuTipus tipus { get; set; }
        public int Xpozicio
        {
            get { return xpozicio; }
            set { xpozicio = value; OPC("Xpozicio"); }
        }
        public int Ypozicio
        {
            get { return ypozicio; }
            set { ypozicio = value; OPC("Ypozicio"); }
        }
        public Szin Szin {get;set;}

        public Babu(int x,int y,Szin color)
        {
            xpozicio = x;
            ypozicio = y;
            aktiv = true;
            this.Szin = color;
        }
        public abstract bool Lep(int[,] palya, int x, int y);
        public abstract bool LehetsegesLepes(int x, int y,int [,] palya);
        public abstract List<Point> LehetsegesLepesek(int[,] palya);
    }
}
