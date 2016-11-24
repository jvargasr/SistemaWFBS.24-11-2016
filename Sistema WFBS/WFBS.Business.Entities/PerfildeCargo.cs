using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class PerfildeCargo
    {
        public int id_perfil_de_cargo { get; set; }
        public string descripcion { get; set; }
        public int Obsoleto { get; set; }
        public decimal Id_PC { get; set; }
        public string Obs { get; set; }
        public string id_areas { get; set; }
        public PerfildeCargo()
        {
            this.Init();
        }

        private void Init()
        {
            this.descripcion = string.Empty;
            this.id_perfil_de_cargo = 0;
            this.Obsoleto = 0;
            this.id_areas = string.Empty;
        }
    }
}
