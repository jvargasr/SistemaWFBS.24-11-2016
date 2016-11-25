using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Habilidad
    {
        public decimal ID_HABILIDAD { get; set; }
        public decimal ID_COMPETENCIA { get; set; }
        public string NOMBRE { get; set; }
        public decimal ORDEN_ASIGNADO { get; set; }
        public string ALTERNATIVA_PREGUNTA { get; set; }
        public string Competencia { get; set; }
        public decimal Orden { get; set; }

        public Habilidad()
        {
            this.Init();
        }

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
