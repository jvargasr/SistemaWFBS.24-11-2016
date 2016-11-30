using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Área, contenedor de las propiedades referente a la tabla Auditoria de la BD.
    /// </summary>
    public class Auditoria
    {
        /// <summary>
        /// PK de la Clase.
        /// </summary>
        public decimal ID_AUDITORIA { get; set; }
        /// <summary>
        /// Dirección IP del computador utilizado para realizar una evaluación.
        /// </summary>
        public string IP_CONEXION { get; set; }
        /// <summary>
        /// Rut de usuario que realizo una evaluación.
        /// </summary>
        public string RUT { get; set; }
        /// <summary>
        /// Fecha en la que un usuario realizo la evaluación.
        /// </summary>
        public System.DateTime FECHA_INGRESO { get; set; }

        /// <summary>
        /// Constructor que llama al metodo inicializador de la Clase.
        /// </summary>
        public Auditoria()
        {
            this.Init();
        }

        /// <summary>
        /// Inicializador de la Clase.
        /// </summary>
        private void Init()
        {
            this.ID_AUDITORIA = 0;
            this.IP_CONEXION = string.Empty;
            this.RUT = string.Empty;
            this.FECHA_INGRESO = DateTime.Now;
        }
    }
}
