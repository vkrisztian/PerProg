using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PerProg
{
    public class ViewModel : Bindable
    {
        public const int csempeMeret = 20;
        Tabla table;
        public Tabla Table
        {
            get { return table; }
            set { table = value; OPC("Table"); }
        }
        Jatekos feher;
        Jatekos fekete;
        public ViewModel()
        {
           
            feher = new Jatekos("TODO", Szin.feher);
            fekete = new Jatekos("AI", Szin.fekete);
            table = new Tabla();
        }
        public bool Lepes(Point from, Point to,bool aktualisjatekos)
        {
            bool lepett = false;
            if (aktualisjatekos)
            {
                foreach (var item in feher.Babuk)
                {
                    if (item.Xpozicio == from.X && item.Ypozicio == from.Y)
                    {
                        lepett = item.Lep(this.Table.Table, (int)to.X, (int)to.Y);
                    }
                }
            }
            else
            {
                foreach (var item in fekete.Babuk)
                {
                    if (item.Xpozicio == from.X && item.Ypozicio == from.Y)
                    {
                        lepett = item.Lep(this.Table.Table, (int)to.X, (int)to.Y);
                    }
                }
            }
           
            OPC("table");
            return lepett;
        }
    }
}
