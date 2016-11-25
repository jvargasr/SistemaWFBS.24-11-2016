using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Area
    {
        public decimal ID_AREA { get; set; }
        public string NOMBRE { get; set; }
        public string ABREVIACION { get; set; }
        public decimal OBSOLETA { get; set; }
        public string obs { get; set; }

        public Area()
        {
            this.Init();
        }

        private void Init()
        {
            this.ID_AREA = 0;
            this.NOMBRE = string.Empty;
            this.ABREVIACION = string.Empty;
            this.OBSOLETA = 0;
        }
    }
}
