using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Usuario
    {
        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public string SEXO { get; set; }
        public decimal ID_PERFIL_DE_CARGO { get; set; }
        public decimal ID_PERFIL { get; set; }
        public string JEFE_RESPECTIVO { get; set; }
        public string PASSWORD { get; set; }
        public decimal OBSOLETO { get; set; }
        public string Area { get; set; }
        public string Perfil { get; set; }
        public string Obs { get; set; }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            this.RUT = string.Empty;
            this.NOMBRE = string.Empty;
            this.SEXO = string.Empty;
            this.ID_PERFIL_DE_CARGO = 0;
            this.ID_PERFIL = 0;
            this.JEFE_RESPECTIVO = string.Empty;
            this.PASSWORD = string.Empty;
            this.OBSOLETO = 0;
        }
    }
}
