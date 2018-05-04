using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public enum BabuTipus { gyalog = 1,bastya = 2,futo = 3, lo = 4,kiraly = 5,kiralyno = 6 }
    public enum Szin { fekete,feher}
    public abstract class Babu : Bindable
    {
        int xpozicio;
        int ypozicio;
        public BabuTipus tipus { get; set; }
        public int Xpozicio
        {
            get { return xpozicio; }
            set { value = xpozicio; OPC("xpozicio"); }
        }
        public int Ypozicio
        {
            get { return ypozicio; }
            set { value = ypozicio; OPC("ypozicio"); }
        }
        public Szin Szin {get;set;}

        public Babu(int x,int y,Szin color)
        {
            xpozicio = x;
            ypozicio = y;
            this.Szin = color;
        }
        public abstract bool Lep(int[,] palya, int x, int y);
    }
}
