using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase PerfildeCargo, contenedor de las propiedades referente a la tabla PerfildeCargo de la BD.
    /// </summary>
    public class PerfildeCargo
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_PERFIL_DE_CARGO { get; set; }
        /// <summary>
        /// Propiedad DESCRIPCION.
        /// </summary>
        public string DESCRIPCION { get; set; }
        /// <summary>
        /// Propiedad OBSOLETO, indica el estado activo de la Entidad con 0 o 1.
        /// </summary>
        public decimal OBSOLETO { get; set; }
        /// <summary>
        /// Propiedad Obs, indica el estado activo de la Entidad con "No" o "Si".
        /// </summary>
        public string Obs { get; set; }

        /// <summary>
        /// Propiedad areas, referente al nombre del Área relacionada.
        /// </summary>
        public string areas { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public PerfildeCargo()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.DESCRIPCION = string.Empty;
            this.ID_PERFIL_DE_CARGO = 0;
            this.OBSOLETO = 0;
        }
    }
}
