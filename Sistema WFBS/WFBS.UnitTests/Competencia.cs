using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Competencia
    {
        [TestMethod]
        public void crearCompetencia()
        {

            WFBS.Business.Entities.Competencia a = new WFBS.Business.Entities.Competencia();

            a.ID_COMPETENCIA = 2;
            a.NOMBRE = "Prueba 3";
            a.DESCRIPCION = "Descripcion de prueba";
            a.SIGLA = "p3";
            a.OBSOLETA = 0;
            a.NIVEL_OPTIMO_ESPERADO = 3 ;

            bool esperando = true;
            CompetenciaOperacion comOp = new CompetenciaOperacion(a);
            bool actua = comOp.Create();
            comOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarCompetencia()
        {

            WFBS.Business.Entities.Competencia a = new WFBS.Business.Entities.Competencia();

            a.ID_COMPETENCIA = 1;

            a.NOMBRE = "Prueba 5";
            a.DESCRIPCION = "Descripcion de prueba";
            a.SIGLA = "pu5";
            a.OBSOLETA = 0;
            a.NIVEL_OPTIMO_ESPERADO = 4;


            bool esperando = true;
            CompetenciaOperacion comOp = new CompetenciaOperacion(a);
            bool actua = comOp.Update();
            comOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarCompetencia()
        {

            WFBS.Business.Entities.Competencia a = new WFBS.Business.Entities.Competencia();

            a.ID_COMPETENCIA = 21;

            bool esperando = true;
            CompetenciaOperacion comOp = new CompetenciaOperacion(a);
            bool actua = comOp.Delete();
            comOp = null;
            Assert.AreEqual(esperando, actua);



        }
    }
}
