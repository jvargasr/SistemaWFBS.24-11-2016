using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Área, contenedor de las propiedades referente a la tabla Área de la BD.
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_AREA { get; set; }
        /// <summary>
        /// Propiedad Nombre.
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Propiedad Abreviación.
        /// </summary>
        public string ABREVIACION { get; set; }
        /// <summary>
        /// Propiedad Obsoleta, indica el estado activo de la Entidad con 0 o 1.
        /// </summary>
        public decimal OBSOLETA { get; set; }
        /// <summary>
        /// Propiedad obs, indica el estado activo de la Entidad con "No" o "Si".
        /// </summary>
        public string obs { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public Area()
        {
            this.Init();
        }

        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.ID_AREA = 0;
            this.NOMBRE = string.Empty;
            this.ABREVIACION = string.Empty;
            this.OBSOLETA = 0;
        }
    }
}
