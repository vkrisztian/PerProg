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
        Tabla tabla;
        public Tabla Tabla
        {
            get { return tabla; }
            set { tabla = value; OPC("tabla"); }
        }
        Jatekos feher;
        Jatekos fekete;
        public ViewModel()
        {
           
            feher = new Jatekos("TODO", Szin.feher);
            fekete = new Jatekos("AI", Szin.fekete);
            tabla = new Tabla();
        }
        public void Proba()
        {
            for (int i = 0; i < 8; i++)
            {
                Tabla.Table[1, i]=2;
            }
            OPC("tabla");
        }
    }
}
