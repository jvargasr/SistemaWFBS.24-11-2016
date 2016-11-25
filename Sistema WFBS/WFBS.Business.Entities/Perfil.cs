using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Perfil
    {
        public decimal ID_PERFIL { get; set; }
        public string TIPO_USUARIO { get; set; }

        public Perfil()
        {
            this.Init();
        }

        private void Init()
        {
            this.ID_PERFIL = 0;
            this.TIPO_USUARIO = string.Empty;
        }

        /*
        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }*/
    }
}
