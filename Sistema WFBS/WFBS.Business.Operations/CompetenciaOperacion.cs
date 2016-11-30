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
    /// Clase CompetenciaOperacion, contenedor de los metodos relacionados a la Entidad Competencia.
    /// </summary>
    public class CompetenciaOperacion : IOperations<Competencia>
    {
        private Competencia _competencia { get; set; }

        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_com">Recibe un parametro del tipo Competencia</param>
        public CompetenciaOperacion(Competencia _com)
        {
            this._competencia = _com;
        }

        #region IOperations

        /// <summary>
        /// Crea una entidad Competencia.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = new COMPETENCIA();

                com.ID_COMPETENCIA = this._competencia.ID_COMPETENCIA;
                com.NOMBRE = this._competencia.NOMBRE;
                com.DESCRIPCION = this._competencia.DESCRIPCION;
                com.SIGLA = this._competencia.SIGLA;
                com.OBSOLETA = this._competencia.OBSOLETA;
                com.NIVEL_OPTIMO_ESPERADO = this._competencia.NIVEL_OPTIMO_ESPERADO;
                com.PREGUNTA_ASOCIADA = this._competencia.PREGUNTA_ASOCIADA;

                compe.COMPETENCIA.Add(com);
                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Competencia: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Lee una entidad Competencia.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Read()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.ID_COMPETENCIA);

                this._competencia.ID_COMPETENCIA = Convert.ToInt32(com.ID_COMPETENCIA);
                this._competencia.NOMBRE = com.NOMBRE;
                this._competencia.DESCRIPCION = com.DESCRIPCION;
                this._competencia.SIGLA = com.SIGLA;
                this._competencia.OBSOLETA = Convert.ToInt32(com.OBSOLETA);
                this._competencia.NIVEL_OPTIMO_ESPERADO = Convert.ToInt32(com.NIVEL_OPTIMO_ESPERADO);
                this._competencia.PREGUNTA_ASOCIADA = com.PREGUNTA_ASOCIADA;
                if (this._competencia.OBSOLETA == 0)
                {
                    this._competencia.Obs = "No";
                }
                else
                {
                    this._competencia.Obs = "Si";
                }

                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Competencia: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Actualiza una entidad Competencia.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Update()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.ID_COMPETENCIA);

                com.ID_COMPETENCIA = this._competencia.ID_COMPETENCIA;
                com.NOMBRE = this._competencia.NOMBRE;
                com.DESCRIPCION = this._competencia.DESCRIPCION;
                com.SIGLA = this._competencia.SIGLA;
                com.OBSOLETA = this._competencia.OBSOLETA;
                com.NIVEL_OPTIMO_ESPERADO = this._competencia.NIVEL_OPTIMO_ESPERADO;
                com.PREGUNTA_ASOCIADA = this._competencia.PREGUNTA_ASOCIADA;

                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Competencia: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Desactiva una entidad Competencia.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.ID_COMPETENCIA);

                com.OBSOLETA = 1;
                compe.SaveChanges();
                compe = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar la Competencia: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Lista todas las entidades de Competencia.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Competencia almacenadas en la BD</returns>
        public List<Competencia> Listar()
        {
            List<DAL.COMPETENCIA> CompetenciasBDD = CommonBC.ModeloWFBS.COMPETENCIA.ToList();
            return GenerarListadoCompetencia(CompetenciasBDD);
        }
        #endregion IOperations

        #region Metodos
        private List<Competencia> GenerarListadoCompetencia(List<DAL.COMPETENCIA> CompetenciaBDD)
        {
            List<Competencia> competenciasController = new List<Competencia>();

            foreach (DAL.COMPETENCIA item in CompetenciaBDD)
            {
                Competencia com = new Competencia();

                com.ID_COMPETENCIA = Convert.ToInt32(item.ID_COMPETENCIA);
                com.NOMBRE = item.NOMBRE;
                com.DESCRIPCION = item.DESCRIPCION;
                com.SIGLA = item.SIGLA;
                com.OBSOLETA = Convert.ToInt32(item.OBSOLETA);
                com.NIVEL_OPTIMO_ESPERADO = Convert.ToInt32(item.NIVEL_OPTIMO_ESPERADO);
                com.PREGUNTA_ASOCIADA = item.PREGUNTA_ASOCIADA;
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                comOp.Read();
                comOp = null;
                competenciasController.Add(com);
            }

            return competenciasController;
        }

        /// <summary>
        /// Genera un listado de las Habilidades almacenadas en el sistema del tipo de la entidad Habilidad.
        /// </summary>
        /// <param name="id_comp">Recibe un parametro id referente a una Competencia</param>
        /// <returns>Retorna un listado con las Habilidades por Competencia almacenadas en el sistema</returns>
        public List<Habilidad> ObtenerHabPorCom(decimal id_comp)
        {
            var HabiBDD = from h in CommonBC.ModeloWFBS.HABILIDAD
                          join c in CommonBC.ModeloWFBS.COMPETENCIA on h.ID_COMPETENCIA equals c.ID_COMPETENCIA
                          where h.ID_COMPETENCIA == id_comp
                          select new Habilidad
                          {
                              ID_HABILIDAD = h.ID_HABILIDAD,
                              Competencia = c.NOMBRE,
                              NOMBRE = h.NOMBRE,
                              ORDEN_ASIGNADO = h.ORDEN_ASIGNADO,
                              ALTERNATIVA_PREGUNTA = h.ALTERNATIVA_PREGUNTA
                          };
            return HabiBDD.ToList();
        }

        /// <summary>
        /// Genera un listado de las Competencias almacenadas en el sistema del tipo de la entidad Competencia.
        /// </summary>
        /// <param name="ar">Recibe un parametro ar del Tipo Área</param>
        /// <returns>Retorna un listado con las Competencias por Área almacenadas en el sistema</returns>
        public List<Competencia> competenciasArea(Area ar)
        {
            try
            {
                DAL.AREA area = CommonBC.ModeloWFBS.AREA.First(a => a.ID_AREA == ar.ID_AREA);
                DAL.WFBSEntities modelo = new WFBSEntities();
                var competencias = area.COMPETENCIA;
                //var competencias = CommonBC.ModeloWFBS.COMPETENCIA.Where(comp => comp.AREA == area);

                return GenerarListadoCompetencia(competencias.ToList());
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener competencias por área: " + ex.ToString());
                return null;
            }
            
        }
        #endregion Metodos
    }
}
