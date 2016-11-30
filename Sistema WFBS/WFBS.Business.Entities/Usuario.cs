using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Usuarios, contenedor de las propiedades referente a la tabla Usuario de la BD.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Propiedad id, PK de la Clase.
        /// </summary>
        public string RUT { get; set; }
        /// <summary>
        /// Propiedad NOMBRE.
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Propiedad SEXO.
        /// </summary>
        public string SEXO { get; set; }
        /// <summary>
        /// Propiedad ID PERFIL DE CARGO, FK_PerfildeCargo de la Clase.
        /// </summary>
        public decimal ID_PERFIL_DE_CARGO { get; set; }
        /// <summary>
        /// Propiedad id, FK_Perfil de la Clase.
        /// </summary>
        public decimal ID_PERFIL { get; set; }
        /// <summary>
        /// Propiedad JEFE RESPECTIVO.
        /// </summary>
        public string JEFE_RESPECTIVO { get; set; }
        /// <summary>
        /// Propiedad PASSWORD.
        /// </summary>
        public string PASSWORD { get; set; }
        /// <summary>
        /// Propiedad OBSOLETO, indica el estado activo de la Entidad con 0 o 1.
        /// </summary>
        public decimal OBSOLETO { get; set; }
        /// <summary>
        /// Propiedad Area, referente al nombre del Área relacionada.
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// Propiedad Perfil, referente al nivel de usuario con el que ingresa al sistema.
        /// </summary>
        public string Perfil { get; set; }
        /// <summary>
        /// Propiedad Obs, indica el estado de la Entidad con "No" o "Si".
        /// </summary>
        public string Obs { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public Usuario()
        {
            this.Init();
        }
        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
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
