using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{
    class Usuarios
    {
        public string Nombre { get; set; }

        public string Id { get; set; }

        public Usuarios() { }

        public Usuarios(string pNombre, string pId)
        {
            this.Nombre = pNombre;
            this.Id = pId;


        }
    }
}
