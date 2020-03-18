using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Atlantis_Gym
{
    public class UsserOn
    {
        public Int64 ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_USUARIO { get; set; }
        
        public UsserOn() { }

        public UsserOn (Int64 pID_USUARIO,String pNOMBRE,String pTIPO_USUARIO)
        {
            this.ID_USUARIO = pID_USUARIO;
            this.NOMBRE = pNOMBRE;
            this.TIPO_USUARIO = pTIPO_USUARIO;
        }
    }
}
