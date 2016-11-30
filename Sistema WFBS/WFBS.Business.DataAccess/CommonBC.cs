using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;
using System.Data;

namespace WFBS.Business.DataAccess
{
    /// <summary>
    /// Clase CommonBC, accesadora a la capa de datos DAL.
    /// </summary>
    public class CommonBC
    {
        private static DAL.WFBSEntities _modeloWfbs;

        /// <summary>
        /// Metodo estatico, crea una instancia del Entity generado en la capa DAL.
        /// </summary>
        public static DAL.WFBSEntities ModeloWFBS
        {
            get
            {
                if (_modeloWfbs == null)
                {
                    _modeloWfbs = new WFBS.DAL.WFBSEntities();
                }
                return _modeloWfbs;
            }
        }

        /// <summary>
        /// Constructos por defecto de la Clase.
        /// </summary>
        public CommonBC()
        {
        }
    }
}
