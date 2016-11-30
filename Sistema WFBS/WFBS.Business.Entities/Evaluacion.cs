using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Evaluación, contenedor de las propiedades referente a la tabla Evaluación de la BD.
    /// </summary>
    public class Evaluacion
    {
        /// <summary>
        /// Propiedad ID Evaluacion, PK de la Clase.
        /// </summary>
        public decimal ID_EVALUACION { get; set; }
        /// <summary>
        /// Propiedad ID Area, FK_Area de la Clase.
        /// </summary>
        public decimal ID_AREA { get; set; }
        /// <summary>
        /// Propiedad ID PERIODO EVALUACION, FK_Periodo_Evaluacion de la Clase.
        /// </summary>
        public decimal ID_PERIODO_EVALUACION { get; set; }
        /// <summary>
        /// Propiedad ID TIPO EVALUACION, FK_Tipo_Evaluacion de la Clase.
        /// </summary>
        public decimal ID_TIPO_EVALUACION { get; set; }
        /// <summary>
        /// Propiedad ID Competencia, FK_Competencia de la Clase.
        /// </summary>
        public decimal ID_COMPETENCIA { get; set; }
        /// <summary>
        /// Propiedad Rut EVALUADO, referente al rut del funcionario que realiza la evaluación.
        /// </summary>
        public string RUT_EVALUADO { get; set; }
        /// <summary>
        /// Propiedad Rut EVALUADOR, referente al rut del jefe que realiza la evaluación hacia un funcionario.
        /// </summary>
        public string RUT_EVALUADOR { get; set; }
        /// <summary>
        /// Propiedad NOTA ESPERADA COMPETENCIA, referente a la nota optima de la competencia.
        /// </summary>
        public decimal NOTA_ESPERADA_COMPETENCIA { get; set; }
        /// <summary>
        /// Propiedad FECHA CONTESTA ENCUESTA.
        /// </summary>
        public System.DateTime FECHA_CONTESTA_ENCUESTA { get; set; }
        /// <summary>
        /// Propiedad NOTA ENCUESTA.
        /// </summary>
        public decimal NOTA_ENCUESTA { get; set; }

        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        public Evaluacion()
        {
            this.ID_EVALUACION = 0;
            this.ID_AREA = 0;
            this.ID_PERIODO_EVALUACION = 0;
            this.ID_TIPO_EVALUACION = 0;
            this.ID_COMPETENCIA = 0;
            this.RUT_EVALUADO = string.Empty;
            this.RUT_EVALUADOR = string.Empty;
            this.NOTA_ESPERADA_COMPETENCIA = 0;
            this.FECHA_CONTESTA_ENCUESTA = DateTime.Now;
            this.NOTA_ENCUESTA = 0;
        }
    }
    
}
