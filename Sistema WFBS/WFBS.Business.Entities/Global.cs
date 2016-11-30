using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Entities
{
    /// <summary>
    /// Clase Global estatica, contenedor de datos de login de un usuario.
    /// </summary>
    public static class Global
    {
        private static string _rutUsuario;
        private static string _nombreUsuario;
        private static string _areaInvestigacion;
        private static string _jefeUsuario;

        /// <summary>
        /// propiedad estatica correspondiente al rut.
        /// </summary>
        public static string RutUsuario
        {
            get { return _rutUsuario; }
            set { _rutUsuario = value; }
        }

        /// <summary>
        /// propiedad estatica correspondiente al nombre.
        /// </summary>
        public static string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        /// <summary>
        /// propiedad estatica correspondiente al Área en que se desempeña el usuario.
        /// </summary>
        public static string AreaInvestigacion
        {
            get { return _areaInvestigacion; }
            set { _areaInvestigacion = value; }
        }

        /// <summary>
        /// propiedad estatica correspondiente al Jefe a cargo que tiene asignado el usuario.
        /// </summary>
        public static string JefeUsuario
        {
            get { return _jefeUsuario; }
            set { _jefeUsuario = value; }
        }

        /// <summary>
        /// metodo estatico inicializador de la clase.
        /// </summary>
        public static void LimpiarVariables()
        {
            _rutUsuario = String.Empty;
            _nombreUsuario = String.Empty;
            _areaInvestigacion = String.Empty;
            _jefeUsuario = String.Empty;
        }
    }
}
