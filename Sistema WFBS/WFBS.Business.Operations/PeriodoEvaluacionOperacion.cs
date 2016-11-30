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
    /// Clase PeriodoEvaluacionOperacion, contenedor de los metodos relacionados a la Entidad Periodo.
    /// </summary>
    public class PeriodoEvaluacionOperacion : IOperations<PeriodoEvaluacion>
    {
        private PeriodoEvaluacion _periodoEvaluacion { get; set; }
        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_periodoEva"> Recibe un parametro del tipo Área</param>
        public PeriodoEvaluacionOperacion(PeriodoEvaluacion _periodoEva)
        {
            this._periodoEvaluacion = _periodoEva;
        }

        #region IOperations
        /// <summary>
        /// Crea una entidad Periodo.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = new PERIODO_EVALUACION();

                pe.ID_PERIODO_EVALUACION = this._periodoEvaluacion.ID_PERIODO_EVALUACION;
                pe.FECHA_INICIO = this._periodoEvaluacion.FECHA_INICIO;
                pe.VIGENCIA = this._periodoEvaluacion.VIGENCIA;
                pe.PORCENTAJE_EVALUACION = this._periodoEvaluacion.PORCENTAJE_EVALUACION;
                pe.PORCENTAJE_AUTOEVALUACION = this._periodoEvaluacion.PORCENTAJE_AUTOEVALUACION;

                periodo.PERIODO_EVALUACION.Add(pe);
                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lee una entidad Periodo.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Read()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.ID_PERIODO_EVALUACION);

                this._periodoEvaluacion.ID_PERIODO_EVALUACION = Convert.ToInt32(pe.ID_PERIODO_EVALUACION);
                this._periodoEvaluacion.FECHA_INICIO = pe.FECHA_INICIO;
                this._periodoEvaluacion.VIGENCIA = Convert.ToInt32(pe.VIGENCIA);
                this._periodoEvaluacion.PORCENTAJE_EVALUACION = Convert.ToInt32(pe.PORCENTAJE_EVALUACION);
                this._periodoEvaluacion.PORCENTAJE_AUTOEVALUACION = Convert.ToInt32(pe.PORCENTAJE_AUTOEVALUACION);

                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Actualiza una entidad Periodo.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Update()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.ID_PERIODO_EVALUACION);

                pe.ID_PERIODO_EVALUACION = this._periodoEvaluacion.ID_PERIODO_EVALUACION;
                pe.FECHA_INICIO = this._periodoEvaluacion.FECHA_INICIO;
                pe.VIGENCIA = this._periodoEvaluacion.VIGENCIA;
                pe.PORCENTAJE_EVALUACION = this._periodoEvaluacion.PORCENTAJE_EVALUACION;
                pe.PORCENTAJE_AUTOEVALUACION = this._periodoEvaluacion.PORCENTAJE_AUTOEVALUACION;

                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Desactiva una entidad Periodo.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.ID_PERIODO_EVALUACION);

                pe.VIGENCIA = 0;

                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lista todas las entidades de Periodo.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Área almacenadas en la BD </returns>
        public List<PeriodoEvaluacion> Listar()
        {
            List<DAL.PERIODO_EVALUACION> periodosBDD = CommonBC.ModeloWFBS.PERIODO_EVALUACION.ToList();
            return GenerarListadoPeriodos(periodosBDD);
        }
        #endregion IOperations

        #region Metodos
        private List<PeriodoEvaluacion> GenerarListadoPeriodos(List<DAL.PERIODO_EVALUACION> periodosBDD)
        {
            List<PeriodoEvaluacion> periodosController = new List<PeriodoEvaluacion>();

            foreach (DAL.PERIODO_EVALUACION item in periodosBDD)
            {
                PeriodoEvaluacion pe = new PeriodoEvaluacion();

                pe.ID_PERIODO_EVALUACION = Convert.ToInt32(item.ID_PERIODO_EVALUACION);
                pe.FECHA_INICIO = item.FECHA_INICIO;
                pe.VIGENCIA = Convert.ToInt32(item.VIGENCIA);
                pe.PORCENTAJE_EVALUACION = Convert.ToInt32(item.PORCENTAJE_EVALUACION);
                pe.PORCENTAJE_AUTOEVALUACION = Convert.ToInt32(item.PORCENTAJE_AUTOEVALUACION);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(pe);
                periodoOp.Read();
                periodoOp = null;
                periodosController.Add(pe);
            }

            return periodosController;
        }
        /// <summary>
        /// Identifica que periodo de evaluacion se encuentra activo.
        /// </summary>
        /// <returns>Retorna el id del periodo. </returns>
        public int periodoEvaluacionActivo()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();

                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.Where(p => p.FECHA_INICIO <= DateTime.Now).ToList().Where
                    (p => DateTime.Now <= p.FECHA_INICIO.AddDays((double)p.VIGENCIA)).ToList().First();

                return (int)pe.ID_PERIODO_EVALUACION;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener periodo evaluación activo: " + ex.ToString());
                return 0;
            }
        }
        #endregion Metodos
    }
}
