//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFBS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AREA
    {
        public AREA()
        {
            this.EVALUACION = new HashSet<EVALUACION>();
            this.OBSERVACION_COMPETENCIA = new HashSet<OBSERVACION_COMPETENCIA>();
            this.PERFIL_DE_CARGO = new HashSet<PERFIL_DE_CARGO>();
            this.COMPETENCIA = new HashSet<COMPETENCIA>();
        }
    
        public decimal ID_AREA { get; set; }
        public string NOMBRE { get; set; }
        public string ABREVIACION { get; set; }
        public decimal OBSOLETA { get; set; }
    
        public virtual ICollection<EVALUACION> EVALUACION { get; set; }
        public virtual ICollection<OBSERVACION_COMPETENCIA> OBSERVACION_COMPETENCIA { get; set; }
        public virtual ICollection<PERFIL_DE_CARGO> PERFIL_DE_CARGO { get; set; }
        public virtual ICollection<COMPETENCIA> COMPETENCIA { get; set; }
    }
}
