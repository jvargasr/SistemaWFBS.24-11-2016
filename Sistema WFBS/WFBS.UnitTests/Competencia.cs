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

            a.Id_com = 2;
            a.Nombre = "Prueba 3";
            a.Descripcion = "Descripcion de prueba";
            a.Sigla = "p3";
            a.Obsoleta = 0;
            a.Nivel_Optimo = 3 ;

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

            a.Id_competencia = 1;

            a.Nombre = "Prueba 5";
            a.Descripcion = "Descripcion de prueba";
            a.Sigla = "pu5";
            a.Obsoleta = 0;
            a.Nivel_Optimo = 4;


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

            a.Id_com = 21;

            bool esperando = true;
            CompetenciaOperacion comOp = new CompetenciaOperacion(a);
            bool actua = comOp.Delete();
            comOp = null;
            Assert.AreEqual(esperando, actua);



        }
    }
}
