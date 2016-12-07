using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.Contracts;
using WFBS.Business.Entities;
using WFBS.Business.Log;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase EvaluacionOperacion, contenedor de los metodos relacionados a la Entidad Evaluación.
    /// </summary>
    public class EvaluacionOperacion : IOperations<Evaluacion>
    {
        private Evaluacion _evaluacion { get; set; }
        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_ev"> Recibe un parametro del tipo Evaluación</param>
        public EvaluacionOperacion(Evaluacion _ev)
        {
            this._evaluacion = _ev;
        }

        #region IOperations
        /// <summary>
        /// Crea una entidad Área.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.EVALUACION ev = new DAL.EVALUACION();

                PeriodoEvaluacion pe = new PeriodoEvaluacion();
                PeriodoEvaluacionOperacion peOp = new PeriodoEvaluacionOperacion(pe);

                ev.ID_EVALUACION = this._evaluacion.ID_EVALUACION;
                ev.ID_AREA = this._evaluacion.ID_AREA;
                ev.ID_PERIODO_EVALUACION = peOp.periodoEvaluacionActivo();
                ev.ID_COMPETENCIA = this._evaluacion.ID_COMPETENCIA;
                ev.RUT_EVALUADO = this._evaluacion.RUT_EVALUADO;
                ev.RUT_EVALUADOR = this._evaluacion.RUT_EVALUADOR;
                ev.NOTA_ESPERADA_COMPETENCIA = this._evaluacion.NOTA_ESPERADA_COMPETENCIA;
                ev.FECHA_CONTESTA_ENCUESTA = this._evaluacion.FECHA_CONTESTA_ENCUESTA;
                ev.NOTA_ENCUESTA = this._evaluacion.NOTA_ENCUESTA;
                ev.ID_TIPO_EVALUACION = this._evaluacion.ID_TIPO_EVALUACION;
                modelo.EVALUACION.Add(ev);
                modelo.SaveChanges();
                modelo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la evaluación:" + ex.ToString());
                return false;
            }
        }

        public Evaluacion notaFinalUsuarioPorCom()
        {
            try
            {
                PeriodoEvaluacion pe = new PeriodoEvaluacion();
                PeriodoEvaluacionOperacion peOp = new PeriodoEvaluacionOperacion(pe);
                pe.ID_PERIODO_EVALUACION = peOp.periodoEvaluacionActivo();
                peOp.Read();

                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev1 = evaluacion.EVALUACION.First(b => b.ID_TIPO_EVALUACION == 1
                && b.ID_PERIODO_EVALUACION == pe.ID_PERIODO_EVALUACION && b.RUT_EVALUADO == _evaluacion.RUT_EVALUADO &&
                b.ID_COMPETENCIA == _evaluacion.ID_COMPETENCIA);

                DAL.EVALUACION ev2 = evaluacion.EVALUACION.First(b => b.ID_TIPO_EVALUACION == 2
                && b.ID_PERIODO_EVALUACION == pe.ID_PERIODO_EVALUACION && b.RUT_EVALUADO == _evaluacion.RUT_EVALUADO &&
                b.ID_COMPETENCIA == _evaluacion.ID_COMPETENCIA);

                Evaluacion ev = new Evaluacion();
                EvaluacionOperacion evOp = new EvaluacionOperacion(ev);
                ev.ID_COMPETENCIA = Convert.ToDecimal(ev1.ID_COMPETENCIA);
                ev.NOTA_ENCUESTA = ev1.NOTA_ENCUESTA;
                ev.NOTA_ESPERADA_COMPETENCIA = Convert.ToDecimal(ev1.NOTA_ESPERADA_COMPETENCIA);
                ev.RUT_EVALUADO = ev1.RUT_EVALUADO;
                double porc_auto = (double)pe.PORCENTAJE_AUTOEVALUACION / 100;
                double porc_ev = (double)pe.PORCENTAJE_AUTOEVALUACION / 100;
                ev.RUT_EVALUADOR = ((ev1.NOTA_ENCUESTA * (pe.PORCENTAJE_AUTOEVALUACION / 100))+(ev2.NOTA_ENCUESTA * (pe.PORCENTAJE_EVALUACION / 100))).ToString();

                return ev;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener información de la evaluacion: " + ex.ToString());
                return null;
            }
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public List<Evaluacion> Listar()
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

        #region Metodos
        /// <summary>
        /// Identifica a un usuario ya evaluado.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool usuarioEvaluado()
        {
            try
            {
                PeriodoEvaluacion pe = new PeriodoEvaluacion();
                PeriodoEvaluacionOperacion peOp = new PeriodoEvaluacionOperacion(pe);
                decimal pe_act = peOp.periodoEvaluacionActivo();
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = evaluacion.EVALUACION.First(b => b.ID_TIPO_EVALUACION == _evaluacion.ID_TIPO_EVALUACION
                && b.ID_PERIODO_EVALUACION == pe_act && b.RUT_EVALUADO == _evaluacion.RUT_EVALUADO);

                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener información del usuario: " + ex.ToString());
                return false;
            }
        }
        #endregion
    }
}
