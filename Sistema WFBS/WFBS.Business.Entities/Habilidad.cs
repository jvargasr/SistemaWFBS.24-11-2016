using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Habilidad
    {
        public int Id_Habilidad { get; set; }
        public int Id_Competencia { get; set; }
        public string Nombre { get; set; }
        public int Orden_Asignado { get; set; }
        public string Alternativa_Pregunta { get; set; }
        public decimal Id_Hab { get; set; }
        public string Competencia { get; set; }
        public decimal Orden { get; set; }

        public Habilidad()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_Competencia = 0;
            this.Id_Competencia = 0;
            this.Nombre = string.Empty;
            this.Orden_Asignado = 0;
            this.Alternativa_Pregunta = string.Empty;
        }
    }
}
