﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public static class Util
    {
        public static BindingList<Babu> InitBabuk(Szin szin)
        {
            int sor = szin == Szin.feher ? 7 : 0;
            BindingList<Babu> babuk = new BindingList<Babu>()
            {
                new Bastya(sor,0,szin),
                new Bastya(sor,7,szin),
                new Lo(sor,1,szin),
                new Lo(sor,6,szin),
                new Futo(sor,2,szin),
                new Futo(sor,5,szin),
                new Kiraly(sor,3,szin),
                new Kiralyno(sor,4,szin)
            };
            if (szin == Szin.feher)
            {
                sor--;
            }
            else
            {
                sor++;
            }
            for (int i = 0; i < 8; i++)
            {
                babuk.Add(new Gyalog(sor,i,szin));
            }
            return babuk;
        }
        public static int [,] InitTabla()
        {
          
           int[,] table = new int[8, 8];
           /* for (int j = 2; j < table.GetLength(0)-2; j++)
            {
                for (int i = 0; i < table.GetLength(1); i++)
                {
                    table[j, i] = 0;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                table[1, i] = (int)new Gyalog(1,i,Szin.fekete).tipus;
                table[6, i] = (int)new Gyalog(6,i,Szin.feher).tipus;
            }
            table[0, 0] = (int)new Bastya(0,0,Szin.fekete).tipus;
            table[0, 7] = (int)new Bastya(0,7, Szin.feher).tipus;
            table[7, 0] = (int)new Bastya(7).tipus;
            table[7, 7] = (int)new Bastya().tipus;
            table[0, 1] = (int)new Lo().tipus;
            table[0, 6] = (int)new Lo().tipus;
            table[7, 1] = (int)new Lo().tipus;
            table[7, 6] = (int)new Lo().tipus;
            table[0, 2] = (int)new Futo().tipus;
            table[0, 5] = (int)new Futo().tipus;
            table[7, 2] = (int)new Futo().tipus;
            table[7, 5] = (int)new Futo().tipus;
            table[0, 3] = (int)new Kiraly().tipus;
            table[7, 3] = (int)new Kiraly().tipus;
            table[0, 4] = (int)new Kiralyno().tipus;
            table[7, 4] = (int)new Kiraly().tipus;*/
            return table;
        }
    }

    public abstract class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OPC([CallerMemberName]string n = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(n));
        }
    }
}