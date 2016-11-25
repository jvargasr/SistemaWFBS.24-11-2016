using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Auditoria
    {
        public decimal ID_AUDITORIA { get; set; }
        public string IP_CONEXION { get; set; }
        public string RUT { get; set; }
        public System.DateTime FECHA_INGRESO { get; set; }

        public Auditoria()
        {
            this.Init();
        }

        private void Init()
        {
            this.ID_AUDITORIA = 0;
            this.IP_CONEXION = string.Empty;
            this.RUT = string.Empty;
            this.FECHA_INGRESO = DateTime.Now;
        }
    }
}
