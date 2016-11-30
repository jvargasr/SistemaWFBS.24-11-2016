using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.DataAccess;
using WFBS.Business.Contracts;
using WFBS.Business.Entities;
using WFBS.DAL;
using WFBS.Business.Log;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase HabilidadOperacion, contenedor de los metodos relacionados a la Entidad Habilidad.
    /// </summary>
    public class HabilidadOperacion : IOperations<Habilidad>
    {
        private Habilidad _habilidad { get; set; }

        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_habi"> Recibe un parametro del tipo Habilidad</param>
        public HabilidadOperacion(Habilidad _habi)
        {
            this._habilidad = _habi;
        }

        #region IOperations
        /// <summary>
        /// Crea una entidad Habilidad.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = new HABILIDAD();
                hab.ID_HABILIDAD = this._habilidad.ID_HABILIDAD;
                hab.ID_COMPETENCIA = this._habilidad.ID_COMPETENCIA;
                hab.NOMBRE = this._habilidad.NOMBRE;
                hab.ORDEN_ASIGNADO = this._habilidad.ORDEN_ASIGNADO;
                hab.ALTERNATIVA_PREGUNTA = this._habilidad.ALTERNATIVA_PREGUNTA;

                habi.HABILIDAD.Add(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Habilidad: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lee una entidad Habilidad.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Read()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.ID_HABILIDAD);

                this._habilidad.ID_HABILIDAD = Convert.ToInt32(hab.ID_HABILIDAD);
                this._habilidad.ID_COMPETENCIA = Convert.ToInt32(hab.ID_COMPETENCIA);
                this._habilidad.NOMBRE = hab.NOMBRE;
                this._habilidad.ORDEN_ASIGNADO = Convert.ToInt32(hab.ORDEN_ASIGNADO);
                this._habilidad.ALTERNATIVA_PREGUNTA = hab.ALTERNATIVA_PREGUNTA;

                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Habilidad: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Actualiza una entidad Habilidad.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Update()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.ID_HABILIDAD);

                hab.ID_HABILIDAD = this._habilidad.ID_HABILIDAD;
                hab.ID_COMPETENCIA = this._habilidad.ID_COMPETENCIA;
                hab.NOMBRE = this._habilidad.NOMBRE;
                hab.ORDEN_ASIGNADO = this._habilidad.ORDEN_ASIGNADO;
                hab.ALTERNATIVA_PREGUNTA = this._habilidad.ALTERNATIVA_PREGUNTA;

                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Habilidad: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Elimina una entidad Habilidad.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.ID_HABILIDAD);

                habi.HABILIDAD.Remove(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Eliminar la Habilidad: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lista todas las entidades de Habilidad.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Área almacenadas en la BD </returns>
        public List<Habilidad> Listar() // default va sin parametros
        {
            List<DAL.HABILIDAD> habilidadesBDD = CommonBC.ModeloWFBS.HABILIDAD.ToList();
            return GenerarListado(habilidadesBDD);
        }
        #endregion IOperations
        #region Metodos
        private List<Habilidad> GenerarListado(List<DAL.HABILIDAD> habilidadesBDD)
        {
            List<Habilidad> habilidadesController = new List<Habilidad>();

            foreach (DAL.HABILIDAD item in habilidadesBDD)
            {
                Habilidad hab = new Habilidad();

                hab.ID_HABILIDAD = Convert.ToInt32(item.ID_HABILIDAD);
                hab.ID_COMPETENCIA = Convert.ToInt32(item.ID_COMPETENCIA);
                hab.NOMBRE = item.NOMBRE;
                hab.ORDEN_ASIGNADO = Convert.ToInt32(item.ORDEN_ASIGNADO);
                hab.ALTERNATIVA_PREGUNTA = item.ALTERNATIVA_PREGUNTA;

                habilidadesController.Add(hab);
            }

            return habilidadesController;

        }

        /// <summary>
        /// Genera un listado de las Habilidades almacenadas en el sistema del tipo de la entidad Habilidad.
        /// </summary>
        /// <param name="id">Recibe un parametro id referente a una Competencia</param>
        /// <returns>Retorna un listado con las Habilidades por Competencia almacenadas en el sistema</returns>
        public List<Habilidad> ObtenerHabPorCom(int id)
        {
            decimal id_com = Convert.ToDecimal(id);
            var HabiBDD = from h in CommonBC.ModeloWFBS.HABILIDAD
                          join c in CommonBC.ModeloWFBS.COMPETENCIA on h.ID_COMPETENCIA equals c.ID_COMPETENCIA
                          where h.ID_COMPETENCIA == id_com
                          select new Habilidad
                          {
                              ID_HABILIDAD = h.ID_HABILIDAD,
                              Competencia = c.NOMBRE,
                              NOMBRE = h.NOMBRE,
                              Orden = h.ORDEN_ASIGNADO,
                              ALTERNATIVA_PREGUNTA = h.ALTERNATIVA_PREGUNTA
                          };
            return HabiBDD.ToList();
        }
        #endregion Metodos
    }
}
