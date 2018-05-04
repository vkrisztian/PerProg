using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerProg
{
    public class Tabla : Bindable
    {
        int[,] table;
        public int[,] Table
        {
            get { return table; }
            set { value = table; OPC("table");  }
        }
        public Tabla()
        {
            this.table = new int[8, 8];
        }
    }
}
