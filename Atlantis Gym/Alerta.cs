using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{
    public class Alerta
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Fecha_Vencimiento { get; set; }
        public string Tipo_Pago { get; set; }

        public Alerta() { }

        public Alerta(string Pnombre,string pApellido,string pFecha_Ven,string pTipo_pago)
        {
            this.Nombre = Pnombre;
            this.Apellido = pApellido;
            this.Fecha_Vencimiento = pFecha_Ven;
            this.Tipo_Pago = pTipo_pago;
        }
    }
}
