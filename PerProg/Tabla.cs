using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    class Tabla
    {
        int[,] Table { get; set; }
        public Tabla()
        {
            Table = new int[8, 8];
        }
    }
}
