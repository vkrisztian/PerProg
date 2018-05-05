using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Proba()
        {
            foreach (var item in fekete.Babuk)
            {
                if (item.Xpozicio == 1 && item.Ypozicio == 1)
                {
                    item.Lep(this.Table.Table, 2, 1);
                }
            }
            OPC("tabla");
        }
    }
}
