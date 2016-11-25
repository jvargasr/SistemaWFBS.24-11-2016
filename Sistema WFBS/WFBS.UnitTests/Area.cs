using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Area
    {
        [TestMethod]
        public void crearArea()
        {

            WFBS.Business.Entities.Area a = new WFBS.Business.Entities.Area();

            a.ID_AREA = 6;
            a.NOMBRE = "prueba6";
            a.ABREVIACION = "p6";
            a.OBSOLETA = 0;

            bool esperando = true;
            AreaOperacion arOp = new AreaOperacion(a);
            bool actua = arOp.Create();
            arOp = null;

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarArea()
        {

            WFBS.Business.Entities.Area a = new WFBS.Business.Entities.Area();

            a.ID_AREA = 3;            
            a.NOMBRE = "ppppppp";
            a.ABREVIACION = "p67";
            a.OBSOLETA = 0;
            

            bool esperando = true;
            AreaOperacion arOp = new AreaOperacion(a);
            bool actua = arOp.Update();
            arOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarArea()
        {

            WFBS.Business.Entities.Area a = new WFBS.Business.Entities.Area();

            a.ID_AREA = 1;

            bool esperando = true;
            AreaOperacion arOp = new AreaOperacion(a);
            bool actua = arOp.Delete();
            arOp = null;
            Assert.AreEqual(esperando, actua);



        }


    }
}
