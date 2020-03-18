using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{
    class ListaR
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Fecha_Vencimiento { get; set; }
        public string Tipo_Pago { get; set; }

        public string Fecha_y_Hora { get; set; }

        public ListaR() { }

        public ListaR(string Pdocumento, string Pnombre, string pApellido, string pFecha_Ven, string pTipo_pago, string pFyH)
        {
            this.Documento = Pdocumento;
            this.Nombre = Pnombre;
            this.Apellido = pApellido;
            this.Fecha_Vencimiento = pFecha_Ven;
            this.Tipo_Pago = pTipo_pago;
            this.Fecha_y_Hora = pFyH;
        }
    }
}
