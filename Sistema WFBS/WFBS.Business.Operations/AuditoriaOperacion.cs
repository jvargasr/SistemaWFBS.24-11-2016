using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.Contracts;
using WFBS.Business.Entities;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase AuditoriaOperacion, encargada de registrar los datos del Usuario que efectue una evaluación.
    /// </summary>
    public class AuditoriaOperacion : IOperations<Auditoria>
    {
        private Auditoria _auditoria { get; set; }

        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_au">Recibe un parametro del tipo Auditoria</param>
        public AuditoriaOperacion(Auditoria _au)
        {
            this._auditoria = _au;
        }

        #region IOperations
        /// <summary>
        /// Crea un registro en la tabla Auditoria de la BD.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.AUDITORIA au = new DAL.AUDITORIA();

                au.ID_AUDITORIA = _auditoria.ID_AUDITORIA;
                au.FECHA_INGRESO = _auditoria.FECHA_INGRESO;
                au.IP_CONEXION = _auditoria.IP_CONEXION;
                au.RUT = _auditoria.RUT;

                modelo.AUDITORIA.Add(au);
                modelo.SaveChanges();
                modelo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la auditoría:" + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public List<Auditoria> Listar()
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
        #endregion
    }
}
