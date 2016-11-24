using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Competencia
    {
        public int Id_competencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sigla { get; set; }
        public int Obsoleta { get; set; }
        public int Nivel_Optimo { get; set; }
        public string Pregunta_Asociada { get; set; }
        public decimal Id_com { get; set; }
        public string Obs { get; set; }
        public decimal Nivel_O { get; set; }

        public Competencia()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_competencia = 0;
            this.Nombre = string.Empty;
            this.Descripcion = string.Empty;
            this.Sigla = string.Empty;
            this.Obsoleta = 0;
            this.Nivel_Optimo = 0;
            this.Pregunta_Asociada = string.Empty;
        }
    }
}
