//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Atlantis_Gym
{
    using System;
    using System.Collections.Generic;
    
    public partial class REGISTRO
    {
        public long NUM { get; set; }
        public Nullable<long> ID { get; set; }
        public Nullable<System.DateTime> FECHAYHORA { get; set; }
    
        public virtual HUELLASCLIENTE HUELLASCLIENTE { get; set; }
    }
}
