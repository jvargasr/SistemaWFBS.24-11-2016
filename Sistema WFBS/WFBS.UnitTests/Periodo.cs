using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Periodo
    {
        [TestMethod]
        public void crearPeriodo()
        {

            WFBS.Business.Entities.PeriodoEvaluacion p = new WFBS.Business.Entities.PeriodoEvaluacion();

            p.ID_PERIODO_EVALUACION = 3;
            p.FECHA_INICIO = DateTime.Parse("10/12/2016");
            p.VIGENCIA = 28;
            p.PORCENTAJE_EVALUACION = 40;
            p.PORCENTAJE_AUTOEVALUACION = 60;
          

            bool esperando = true;
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(p);
            bool actua = periodoOp.Create();
            periodoOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarPeriodo()
        {

            WFBS.Business.Entities.PeriodoEvaluacion p = new WFBS.Business.Entities.PeriodoEvaluacion();

            p.ID_PERIODO_EVALUACION = 24;
            p.FECHA_INICIO = DateTime.Parse("10/10/2016");
            p.VIGENCIA = 30;
            p.PORCENTAJE_EVALUACION = 60;
            p.PORCENTAJE_AUTOEVALUACION = 40;


            bool esperando = true;
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(p);
            bool actua = periodoOp.Update();
            periodoOp = null;
            Assert.AreEqual(esperando, actua);

        }

        [TestMethod]
        public void EliminarPeriodo()
        {

            WFBS.Business.Entities.PeriodoEvaluacion p = new WFBS.Business.Entities.PeriodoEvaluacion();

            p.ID_PERIODO_EVALUACION = 24;

            bool esperando = true;
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(p);
            bool actua = periodoOp.Delete();
            periodoOp = null;
            Assert.AreEqual(esperando, actua);


        }
    }
}
