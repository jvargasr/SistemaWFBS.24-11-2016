using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Area
    {
        public int id_area { get; set; }
        public string area { get; set; }
        public string abreviacion { get; set; }
        public int obsoleta { get; set; }
        public decimal Id_area { get; set; }
        public string obs { get; set; }

        public Area()
        {
            this.Init();
        }

        private void Init()
        {
            this.abreviacion = string.Empty;
            this.area = string.Empty;
            this.id_area = 0;
            this.obsoleta = 0;
        }
    }
}
