using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{

    class ProductosI
    {
        public string Nombre { get; set; }
        public Int32 Stock { get; set; }

        public ProductosI() { }

        public ProductosI(string pNombre, Int32 pStock)
        {
            this.Nombre = pNombre;
            this.Stock = pStock;

        }
    }
}
