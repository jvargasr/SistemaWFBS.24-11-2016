using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Perfil, contenedor de las propiedades referente a la tabla Perfil de la BD.
    /// </summary>
    public class Perfil
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public decimal ID_PERFIL { get; set; }
        /// <summary>
        /// Propiedad TIPO USUARIO, referente al nivel de usuario con el que ingresa al sistema.
        /// </summary>
        public string TIPO_USUARIO { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public Perfil()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
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
