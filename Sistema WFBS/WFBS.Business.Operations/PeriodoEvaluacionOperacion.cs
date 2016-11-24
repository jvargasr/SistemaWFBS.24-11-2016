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
    public class PeriodoEvaluacionOperacion : IOperations<PeriodoEvaluacion>
    {
        private PeriodoEvaluacion _periodoEvaluacion { get; set; }

        public PeriodoEvaluacionOperacion(PeriodoEvaluacion _periodoEva)
        {
            this._periodoEvaluacion = _periodoEva;
        }

        #region IOperations
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = new PERIODO_EVALUACION();

                pe.ID_PERIODO_EVALUACION = this._periodoEvaluacion.idPeriodo;
                pe.FECHA_INICIO = this._periodoEvaluacion.fechaInicio;
                pe.VIGENCIA = this._periodoEvaluacion.vigencia;
                pe.PORCENTAJE_EVALUACION = this._periodoEvaluacion.porcentajeE;
                pe.PORCENTAJE_AUTOEVALUACION = this._periodoEvaluacion.porcentajeAE;

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

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.idPeriodo);

                this._periodoEvaluacion.idPeriodo = Convert.ToInt32(pe.ID_PERIODO_EVALUACION);
                this._periodoEvaluacion.fechaInicio = pe.FECHA_INICIO;
                this._periodoEvaluacion.vigencia = Convert.ToInt32(pe.VIGENCIA);
                this._periodoEvaluacion.porcentajeE = Convert.ToInt32(pe.PORCENTAJE_EVALUACION);
                this._periodoEvaluacion.porcentajeAE = Convert.ToInt32(pe.PORCENTAJE_AUTOEVALUACION);

                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.idPeriodo);

                pe.ID_PERIODO_EVALUACION = this._periodoEvaluacion.idPeriodo;
                pe.FECHA_INICIO = this._periodoEvaluacion.fechaInicio;
                pe.VIGENCIA = this._periodoEvaluacion.vigencia;
                pe.PORCENTAJE_EVALUACION = this._periodoEvaluacion.porcentajeE;
                pe.PORCENTAJE_AUTOEVALUACION = this._periodoEvaluacion.porcentajeAE;

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

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this._periodoEvaluacion.idPeriodo);

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

                pe.idPeriodo = Convert.ToInt32(item.ID_PERIODO_EVALUACION);
                pe.fechaInicio = item.FECHA_INICIO;
                pe.vigencia = Convert.ToInt32(item.VIGENCIA);
                pe.porcentajeE = Convert.ToInt32(item.PORCENTAJE_EVALUACION);
                pe.porcentajeAE = Convert.ToInt32(item.PORCENTAJE_AUTOEVALUACION);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(pe);
                periodoOp.Read();
                periodoOp = null;
                periodosController.Add(pe);
            }

            return periodosController;
        }
        #endregion Metodos
    }
}
