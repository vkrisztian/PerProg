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
            set { table = value; OPC("Table");  }
        }
        public Tabla()
        {
            this.table = Util.InitTabla();
        }
    }
}
