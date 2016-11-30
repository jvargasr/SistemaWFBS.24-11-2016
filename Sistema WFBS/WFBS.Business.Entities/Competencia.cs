using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Competencia, contenedor de las propiedades referente a la tabla Competencia de la BD.
    /// </summary>
    public class Competencia
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_COMPETENCIA { get; set; }
        /// <summary>
        /// Propiedad NOMBRE.
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Propiedad DESCRIPCION.
        /// </summary>
        public string DESCRIPCION { get; set; }
        /// <summary>
        /// Propiedad SIGLA.
        /// </summary>
        public string SIGLA { get; set; }
        /// <summary>
        /// Propiedad OBSOLETA, indica el estado activo de la Entidad con 0 o 1.
        /// </summary>
        public decimal OBSOLETA { get; set; }
        /// <summary>
        /// Propiedad NIVEL OPTIMO ESPERADO.
        /// </summary>
        public decimal NIVEL_OPTIMO_ESPERADO { get; set; }
        /// <summary>
        /// Propiedad PREGUNTA_ASOCIADA.
        /// </summary>
        public string PREGUNTA_ASOCIADA { get; set; }
        /// <summary>
        /// Propiedad Obs, indica el estado activo de la Entidad con "No" o "Si".
        /// </summary>
        public string Obs { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public Competencia()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.ID_COMPETENCIA = 0;
            this.NOMBRE = string.Empty;
            this.DESCRIPCION = string.Empty;
            this.SIGLA = string.Empty;
            this.OBSOLETA = 0;
            this.NIVEL_OPTIMO_ESPERADO = 0;
            this.PREGUNTA_ASOCIADA = string.Empty;
        }
    }
}
