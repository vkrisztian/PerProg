using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    class Jatekos
    {
        string Name { get; set; }
        BindingList<Babu> Babuk { get; set; }
        Szin Szin { get; set; }
        public Jatekos(string name,Szin szin)
        {
            this.Name = name;
            this.Szin = szin;
            this.Babuk = Util.InitBabuk(szin);
        }
    }
}
