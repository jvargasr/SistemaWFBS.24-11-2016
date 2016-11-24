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

            a.id_area = 6;
            a.area = "prueba6";
            a.abreviacion = "p6";
            a.obsoleta = 0;

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

            a.id_area = 3;
            
               a.area = "ppppppp";
                a.abreviacion = "p67";
                a.obsoleta = 0;
            

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

            a.Id_area = 1;

            bool esperando = true;
            AreaOperacion arOp = new AreaOperacion(a);
            bool actua = arOp.Delete();
            arOp = null;
            Assert.AreEqual(esperando, actua);



        }


    }
}
