using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Evaluacion
    {
        public decimal ID_EVALUACION { get; set; }
        public decimal ID_AREA { get; set; }
        public decimal ID_PERIODO_EVALUACION { get; set; }
        public decimal ID_TIPO_EVALUACION { get; set; }
        public decimal ID_COMPETENCIA { get; set; }
        public string RUT_EVALUADO { get; set; }
        public string RUT_EVALUADOR { get; set; }
        public decimal NOTA_ESPERADA_COMPETENCIA { get; set; }
        public System.DateTime FECHA_CONTESTA_ENCUESTA { get; set; }
        public decimal NOTA_ENCUESTA { get; set; }

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
