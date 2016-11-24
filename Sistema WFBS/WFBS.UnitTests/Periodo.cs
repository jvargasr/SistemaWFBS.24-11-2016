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

            p.Id_Periodo = 3;
            p.fechaInicio = DateTime.Parse("10/12/2016");
            p.vigencia = 28;
            p.porcentajeE = 40;
            p.porcentajeAE = 60;
          

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

            p.idPeriodo = 24;
            p.fechaInicio = DateTime.Parse("10/10/2016");
            p.vigencia = 30;
            p.porcentajeE = 60;
            p.porcentajeAE = 40;


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

            p.idPeriodo = 24;

            bool esperando = true;
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(p);
            bool actua = periodoOp.Delete();
            periodoOp = null;
            Assert.AreEqual(esperando, actua);


        }
    }
}
