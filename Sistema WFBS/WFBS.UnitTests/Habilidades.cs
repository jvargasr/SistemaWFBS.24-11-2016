using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Habilidades
    {
        [TestMethod]
        public void crearHabilidad()
        {

            WFBS.Business.Entities.Habilidad a = new WFBS.Business.Entities.Habilidad();

            a.ID_COMPETENCIA = 1;
            a.ID_HABILIDAD = 2;
            a.NOMBRE = "aaaaaa";
            a.ORDEN_ASIGNADO = 3;
            a.ALTERNATIVA_PREGUNTA = "¿lololo?";

            bool esperando = true;
            HabilidadOperacion habOp = new HabilidadOperacion(a);
            bool actua = habOp.Create();
            habOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarHabilidad()
        {

            WFBS.Business.Entities.Habilidad a = new WFBS.Business.Entities.Habilidad();

            a.ID_COMPETENCIA = 1;
            a.ID_HABILIDAD = 1;
            a.NOMBRE = "palala";
            a.ORDEN_ASIGNADO = 3;
            a.ALTERNATIVA_PREGUNTA = "¿pololo?";

    
            bool esperando = true;
            HabilidadOperacion habOp = new HabilidadOperacion(a);
            bool actua = habOp.Update();
            habOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarHabilidad()
        {

            WFBS.Business.Entities.Habilidad a = new WFBS.Business.Entities.Habilidad();

            a.ID_HABILIDAD = 2;

            bool esperando = true;
            HabilidadOperacion habOp = new HabilidadOperacion(a);
            bool actua = habOp.Delete();
            habOp = null;
            Assert.AreEqual(esperando, actua);



        }

    }
}
