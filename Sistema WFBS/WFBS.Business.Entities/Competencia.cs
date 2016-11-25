using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Competencia
    {
        public decimal ID_COMPETENCIA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string SIGLA { get; set; }
        public decimal OBSOLETA { get; set; }
        public decimal NIVEL_OPTIMO_ESPERADO { get; set; }
        public string PREGUNTA_ASOCIADA { get; set; }
        public string Obs { get; set; }
        
        public Competencia()
        {
            this.Init();
        }

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
