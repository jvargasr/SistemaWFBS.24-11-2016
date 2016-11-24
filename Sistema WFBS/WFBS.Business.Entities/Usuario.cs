using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    public class Usuario
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public int Id_Area { get; set; }
        public int Id_Perfil { get; set; }
        public string Jefe { get; set; }
        public string Password { get; set; }
        public int Obsoleto { get; set; }
        public string Area { get; set; }
        public string Perfil { get; set; }
        public string Obs { get; set; }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            this.Rut = string.Empty;
            this.Nombre = string.Empty;
            this.Sexo = string.Empty;
            this.Id_Area = 0;
            this.Id_Perfil = 0;
            this.Jefe = string.Empty;
            this.Password = string.Empty;
            this.Obsoleto = 0;
        }
    }
}
