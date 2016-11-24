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

            a.Id_Competencia = 1;
            a.Id_Habilidad = 2;
            a.Nombre = "aaaaaa";
            a.Orden_Asignado = 3;
            a.Alternativa_Pregunta = "¿lololo?";

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

            a.Id_Competencia = 1;
            a.Id_Habilidad = 1;
            a.Nombre = "palala";
            a.Orden_Asignado = 3;
            a.Alternativa_Pregunta = "¿pololo?";

    
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

            a.Id_Habilidad = 2;

            bool esperando = true;
            HabilidadOperacion habOp = new HabilidadOperacion(a);
            bool actua = habOp.Delete();
            habOp = null;
            Assert.AreEqual(esperando, actua);



        }

    }
}
