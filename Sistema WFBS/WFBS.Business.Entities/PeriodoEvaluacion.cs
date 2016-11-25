using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class PeriodoEvaluacion
    {
        public decimal ID_PERIODO_EVALUACION { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public decimal VIGENCIA { get; set; }
        public decimal PORCENTAJE_EVALUACION { get; set; }
        public decimal PORCENTAJE_AUTOEVALUACION { get; set; }


        public PeriodoEvaluacion()
        {
            this.Init();
        }

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
