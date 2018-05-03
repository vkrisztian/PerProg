using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public class ViewModel
    {
        Tabla tabla;
        Jatekos feher;
        Jatekos fekete;
        public ViewModel()
        {
            tabla = new Tabla();
            feher = new Jatekos("TODO", Szin.feher);
            fekete = new Jatekos("AI", Szin.fekete);
        }
    }
}
