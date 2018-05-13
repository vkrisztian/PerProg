using System;
using System.Collections.Generic;
using System.IO;
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
        ParhuzamosJatekos fekete;
        bool sakkmatt;
        public ViewModel()
        {

            feher = new Jatekos("Jatekos", Szin.feher);
            fekete = new ParhuzamosJatekos("AI", Szin.fekete,feher);
            table = new Tabla();
            sakkmatt = false;
        }
        public bool Lepes(Point from, Point to, bool aktualisjatekos)
        {
            bool lepett = false;
            if (aktualisjatekos)
            {
                foreach (var item in feher.Babuk)
                {
                    if (item.Xpozicio == from.X && item.Ypozicio == from.Y)
                    {
                        lepett = item.Lep(this.Table.Table, (int)to.X, (int)to.Y);
                        if (lepett)
                        {
                            if (SakkbaLep((int)to.X, (int)to.Y, item, aktualisjatekos))
                            {
                                lepett = false;
                            }
                            else
                            {
                                Table.Table[item.Xpozicio, item.Ypozicio] = 0;
                                Table.Table[(int)to.X, (int)to.Y] = (int)item.tipus * (int)item.Szin;
                                item.Xpozicio = (int)to.X;
                                item.Ypozicio = (int)to.Y;
                                Leut((int)to.X, (int)to.Y, aktualisjatekos);
                                feher.sakk = false;
                                fekete.sakk = feher.SakkTesz(fekete.GetKiraly(), table.Table);
                                OPC("Table");
                                StreamWriter sw = new StreamWriter("log.txt");
                                for (int i = 0; i < table.Table.GetLength(0); i++)
                                {
                                    for (int j = 0; j < table.Table.GetLength(1); j++)
                                    {
                                        sw.Write(table.Table[i, j] + "\t");
                                    }
                                    sw.WriteLine();
                                }
                                sw.Close();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in fekete.Babuk)
                {
                    if (item.Xpozicio == from.X && item.Ypozicio == from.Y)
                    {
                        Table.Table[item.Xpozicio, item.Ypozicio] = 0;
                        Table.Table[(int)to.X, (int)to.Y] = (int)item.tipus * (int)item.Szin;
                        item.Xpozicio = (int)to.X;
                        item.Ypozicio = (int)to.Y;
                        Leut((int)to.X, (int)to.Y, aktualisjatekos);
                        fekete.sakk = false;
                        feher.sakk = fekete.SakkTesz(feher.GetKiraly(), table.Table);
                        OPC("Table");
                        if (feher.sakk)
                        {
                            MessageBox.Show("Fehér Sakkban van!");
                        }
                    }
                }
            }

            OPC("table");
            sakkmatt = SakkMattCheck(!aktualisjatekos);

            if (sakkmatt)
            {
                string nev = aktualisjatekos == true ? feher.Name : fekete.Name;
                MessageBox.Show("Sakk Matt!\nJáték vége nyert: " + nev);
            }
            return lepett;
        }

        private bool SakkMattCheck(bool aktualisjatekos)
        {
            Babu temp = null;
            bool sakkmatt = false;
            bool sakk = false;
            if (aktualisjatekos)
            {
                temp = feher.GetKiraly();
                sakk = feher.sakk;
            }
            else
            {
                temp = fekete.GetKiraly();
                sakk = fekete.sakk;
            }
            if (sakk)
            {
                List<Point> kiralyLehetsegesLepesek = temp.LehetsegesLepesek(table.Table);
                foreach (var item in kiralyLehetsegesLepesek)
                {
                    if (!SakkbaLep((int)item.X, (int)item.Y, temp, aktualisjatekos))
                    {
                        return false;
                    }
                }
                if (aktualisjatekos)
                {
                    foreach (var item in feher.Babuk)
                    {
                        List<Point> babuLepesei = item.LehetsegesLepesek(table.Table);
                        foreach (var lepes in babuLepesei)
                        {
                            if (!SakkbaLep((int)lepes.X, (int)lepes.Y, item, aktualisjatekos))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in fekete.Babuk)
                    {
                        List<Point> babuLepesei = item.LehetsegesLepesek(table.Table);
                        foreach (var lepes in babuLepesei)
                        {
                            if (!SakkbaLep((int)lepes.X, (int)lepes.Y, item, aktualisjatekos))
                            {
                                return false;
                            }
                        }
                    }
                }
                sakkmatt = true;
            }
            return sakkmatt;
        }


        void Leut(int x, int y, bool aktualisjatekos)
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
                if (i >= 0)
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
                if (i >= 0)
                {
                    feher.Babuk.RemoveAt(i);
                }

            }

        }

        Babu IdeiglenesLeut(int x, int y, bool aktualisjatekos)
        {
            Babu temp = null;
            if (aktualisjatekos)
            {
                foreach (var item in fekete.Babuk)
                {
                    if (item.Xpozicio == x && item.Ypozicio == y)
                    {
                        temp = item;
                    }
                }
                if (temp != null)
                    fekete.Babuk.Remove(temp);
            }
            else
            {
                foreach (var item in feher.Babuk)
                {
                    if (item.Xpozicio == x && item.Ypozicio == y)
                    {
                        temp = item;
                    }
                }
                if (temp != null)
                    feher.Babuk.Remove(temp);
            }
            return temp;
        }
        bool SakkbaLep(int x, int y, Babu babu, bool aktualisjatekos)
        {
            bool megmindigSakk = true;
            int tempx = babu.Xpozicio;
            int tempy = babu.Ypozicio;
            babu.Xpozicio = x;
            babu.Ypozicio = y;
            int[,] temp = Util.CreateTemp(Table.Table);
            temp[tempx, tempy] = 0;
            temp[x, y] = (int)babu.tipus * (int)babu.Szin;
            Babu ideiglenesenLeutott = IdeiglenesLeut(x, y, aktualisjatekos);
            if (aktualisjatekos)
            {
                if (fekete.SakkTesz(feher.GetKiraly(), temp))
                {
                    megmindigSakk = true;
                }
                else
                {
                    megmindigSakk = false;
                }
                if (ideiglenesenLeutott != null)
                {
                    fekete.Babuk.Add(ideiglenesenLeutott);
                }
            }
            else
            {
                if (feher.SakkTesz(fekete.GetKiraly(), temp))
                {
                    megmindigSakk = true;
                }
                else
                {
                    megmindigSakk = false;
                }
                if (ideiglenesenLeutott != null)
                {
                    feher.Babuk.Add(ideiglenesenLeutott);
                }
            }
            babu.Xpozicio = tempx;
            babu.Ypozicio = tempy;

            return megmindigSakk;
        }

        public bool ParhuzamosJatekosLepett()
        {
            bool aktualisjatekos = false;
            bool lepett = false;
            Point[] fromto = new Point[2];
            fromto = fekete.LepesKiszamit(1, Table.Table, true);
            Lepes(fromto[0], fromto[1], aktualisjatekos);
            return lepett;
        }
    }

}
