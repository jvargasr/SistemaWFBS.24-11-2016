using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class PeriodoEvaluacion
    {
        public int idPeriodo { get; set; }
        public DateTime fechaInicio { get; set; }
        public int vigencia { get; set; }
        public int porcentajeE { get; set; }
        public int porcentajeAE { get; set; }
        public decimal Id_Periodo { get; set; }

        public PeriodoEvaluacion()
        {
            this.Init();
        }

        private void Init()
        {
            this.idPeriodo = 0;
            this.fechaInicio = DateTime.Now;
            this.vigencia = 0;
            this.porcentajeE = 0;
            this.porcentajeAE = 0;
        }
    }
}
