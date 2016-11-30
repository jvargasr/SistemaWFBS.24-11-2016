using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Periodo Evaluacion, contenedor de las propiedades referente a la tabla PeriodoEvaluacion de la BD.
    /// </summary>
    public class PeriodoEvaluacion
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_PERIODO_EVALUACION { get; set; }
        /// <summary>
        /// Propiedad FECHA INICIO, referente a cuando se dio inicio el Periodo de Evaluación.
        /// </summary>
        public DateTime FECHA_INICIO { get; set; }
        /// <summary>
        /// Propiedad VIGENCIA, referente a los dias que le quedan activo al Periodo de Evaluación.
        /// </summary>
        public decimal VIGENCIA { get; set; }
        /// <summary>
        /// Propiedad PORCENTAJE EVALUACION, referente a la evaluación del jefe.
        /// </summary>
        public decimal PORCENTAJE_EVALUACION { get; set; }
        /// <summary>
        /// Propiedad PORCENTAJE AUTOEVALUACION, referente a la evaluación del funcionario.
        /// </summary>
        public decimal PORCENTAJE_AUTOEVALUACION { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public PeriodoEvaluacion()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.ID_PERIODO_EVALUACION = 0;
            this.FECHA_INICIO = DateTime.Now;
            this.VIGENCIA = 0;
            this.PORCENTAJE_EVALUACION = 0;
            this.PORCENTAJE_AUTOEVALUACION = 0;
        }
    }
}
