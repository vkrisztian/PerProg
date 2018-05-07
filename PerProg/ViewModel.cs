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
        bool sakkmatt;
        public ViewModel()
        {
           
            feher = new Jatekos("Jatekos", Szin.feher);
            fekete = new Jatekos("AI", Szin.fekete);
            table = new Tabla();
            sakkmatt = false;
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
                        lepett = item.Lep(this.Table.Table, (int)to.X, (int)to.Y,feher.sakk);
                        if (lepett)
                        {
                            Leut((int)to.X, (int)to.Y, aktualisjatekos);
                            fekete.sakk = SakkTesz(fekete.GetKiraly(), !aktualisjatekos);
                            OPC("table");
                          
                        }
                        break;
                    }
                }
            }
            else
            {
                foreach (var item in fekete.Babuk)
                {
                    if (item.Xpozicio == from.X && item.Ypozicio == from.Y)
                    {
                        lepett = item.Lep(this.Table.Table, (int)to.X, (int)to.Y,fekete.sakk);
                        if (lepett)
                        {
                            Leut((int)to.X, (int)to.Y, aktualisjatekos);
                            feher.sakk = SakkTesz(feher.GetKiraly(), !aktualisjatekos);
                            OPC("table");
                            if (feher.sakk)
                            {
                                MessageBox.Show("Sakk!");
                            }
                        }
                        break;
                    }
                }
            }
           
            OPC("table");


            if (sakkmatt)
            {
                string nev = aktualisjatekos == true ? feher.Name : fekete.Name;
                MessageBox.Show("Játék vége nyert: " + nev);
            }
            return lepett;
        }
        void Leut(int x , int y,bool aktualisjatekos)
        {
            int i = -1;
            if (aktualisjatekos)
            { 
                foreach (var item in fekete.Babuk)
                {
                    if (item.Xpozicio == x && item.Ypozicio == y)
                    {
                        i = fekete.Babuk.IndexOf(item);
                    }
                }
                if (i>=0)
                {
                    fekete.Babuk.RemoveAt(i);
                }
              
            }
            else
            {
                foreach (var item in feher.Babuk)
                {
                    if (item.Xpozicio == x && item.Ypozicio == y)
                    {
                        i = feher.Babuk.IndexOf(item);
                    }
                }
                if (i>=0)
                {
                    feher.Babuk.RemoveAt(i);
                }
                
            }

        }

        bool SakkTesz(Babu kiraly, bool aktualisJatekos)
        {
            bool sakk = false;
            if (aktualisJatekos)
            {
                foreach (var item in fekete.Babuk)
                {
                    if (item.LehetsegesLepes(kiraly.Xpozicio,kiraly.Ypozicio,Table.Table))
                    {
                        sakk = true;
                    }
                }
            }
            else
            {
                foreach (var item in feher.Babuk)
                {
                    if (item.LehetsegesLepes(kiraly.Xpozicio, kiraly.Ypozicio, Table.Table))
                    {
                        sakk = true;
                    }
                }
            }
            return sakk;
        }
    }

}
