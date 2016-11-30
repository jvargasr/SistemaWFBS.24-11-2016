using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Habilidad, contenedor de las propiedades referente a la tabla Habilidad de la BD.
    /// </summary>
    public class Habilidad
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_HABILIDAD { get; set; }
        /// <summary>
        /// Propiedad id, FK_Competencia de la Clase.
        /// </summary>
        public decimal ID_COMPETENCIA { get; set; }
        /// <summary>
        /// Propiedad NOMBRE.
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Propiedad ORDEN ASIGNADO.
        /// </summary>
        public decimal ORDEN_ASIGNADO { get; set; }
        /// <summary>
        /// Propiedad ALTERNATIVA PREGUNTA.
        /// </summary>
        public string ALTERNATIVA_PREGUNTA { get; set; }
        /// <summary>
        /// Propiedad Competencia, Referente al nombre de la Competencia relacionada.
        /// </summary>
        public string Competencia { get; set; }
        /// <summary>
        /// Propiedad Orden.
        /// </summary>
        public decimal Orden { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>

        public Habilidad()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.ID_COMPETENCIA = 0;
            this.ID_COMPETENCIA = 0;
            this.NOMBRE = string.Empty;
            this.ORDEN_ASIGNADO = 0;
            this.ALTERNATIVA_PREGUNTA = string.Empty;
        }
    }
}
