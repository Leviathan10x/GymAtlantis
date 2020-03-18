using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis_Gym
{
    public class Ingresos
    {
        
        public Int32 DOC_CLIENTE { get; set; }
        public string TIPO_INGRE {get; set;}
        public DateTime FECHA_INGRE { get; set; }
        public Int32 TOTAL { get; set; }

       // public Int32 ID_USUARIO { get; set; }

       // public DateTime FECHA_INI { get; set; }

       // public Int32 ID_INGRESO { get; set; }

        public Ingresos() { }

        public Ingresos(Int32 pDOC_CLIENTE,string pTIPO_INGRE,DateTime pFECHA_INGRE,Int32 pTOTAL)
        {
            
            this.DOC_CLIENTE = pDOC_CLIENTE;
            this.TIPO_INGRE = pTIPO_INGRE;
            this.FECHA_INGRE = pFECHA_INGRE;
            this.TOTAL = pTOTAL;
            //this.ID_USUARIO = pId_Usuario;
            //this.FECHA_INI = pFecha_inicio;
            //this.ID_INGRESO = pId_ingreso;
            
        }

    }
}
