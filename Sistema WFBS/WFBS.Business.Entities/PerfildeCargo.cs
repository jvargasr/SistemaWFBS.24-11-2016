using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class PerfildeCargo
    {
        public decimal ID_PERFIL_DE_CARGO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal OBSOLETO { get; set; }
        public string Obs { get; set; }
        public string areas { get; set; }
        public PerfildeCargo()
        {
            this.Init();
        }

        private void Init()
        {
            this.DESCRIPCION = string.Empty;
            this.ID_PERFIL_DE_CARGO = 0;
            this.OBSOLETO = 0;
        }
    }
}
