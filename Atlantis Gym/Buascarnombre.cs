using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{
    
    
    class Buascarnombre
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Int64 Documento { get; set; }

        public Buascarnombre() { }

        public Buascarnombre(String pNombre,String pApellido,Int64 pDocumento)
        {
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Documento = pDocumento;

        }
    }
}
