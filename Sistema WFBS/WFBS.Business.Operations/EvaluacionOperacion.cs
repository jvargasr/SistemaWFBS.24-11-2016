﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.Contracts;
using WFBS.Business.Entities;
using WFBS.Business.Log;

namespace WFBS.Business.Operations
{
    public class EvaluacionOperacion : IOperations<Evaluacion>
    {
        private Evaluacion _evaluacion { get; set; }

        public EvaluacionOperacion(Evaluacion _ev)
        {
            this._evaluacion = _ev;
        }

        #region IOperations
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
        public bool usuarioEvaluado()
        {
            try
            {
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = evaluacion.EVALUACION.First(b => b.ID_TIPO_EVALUACION == _evaluacion.ID_TIPO_EVALUACION
                && b.ID_PERIODO_EVALUACION == _evaluacion.ID_PERIODO_EVALUACION && b.RUT_EVALUADO == _evaluacion.RUT_EVALUADO);

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