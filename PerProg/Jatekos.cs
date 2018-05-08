using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    class Jatekos
    {
        public string Name { get; set; }
        public ObservableCollection<Babu> Babuk { get; set; }
        public bool sakk = false;
        Szin Szin { get; set; }
        public Jatekos(string name,Szin szin)
        {
            this.Name = name;
            this.Szin = szin;
            this.Babuk = Util.InitBabuk(szin);
            
        }
        public Babu GetKiraly()
        {
            int i = -1;
            foreach (var item in Babuk)
            {
                if (item.tipus == BabuTipus.kiraly)
                {
                    i = Babuk.IndexOf(item);
                }
            }
            return Babuk.ElementAt(i);
        }

        public bool SakkTesz(Babu kiraly,int[,] palya)
        {
            bool sakk = false;
                foreach (var item in Babuk)
                {
                    if (item.LehetsegesLepes(kiraly.Xpozicio, kiraly.Ypozicio, palya))
                    {
                        sakk = true;
                    }
                }

            return sakk;
        }
    }
}
