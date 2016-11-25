using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class PerfilesdeCargo
    {
        [TestMethod]
        public void crearPerfildeCargo()
        {

            WFBS.Business.Entities.PerfildeCargo pc = new WFBS.Business.Entities.PerfildeCargo();
            pc.DESCRIPCION = "Descripción de prueba";
            pc.OBSOLETO = 0;

            List<WFBS.Business.Entities.Area> areas = new List<WFBS.Business.Entities.Area>();
            WFBS.Business.Entities.Area a = new WFBS.Business.Entities.Area();
            a.ID_AREA = 1;
            areas.Add(a);

            bool esperando = true;
            PerfildeCargoOperacion perfilOp = new PerfildeCargoOperacion(pc);
            bool actua = perfilOp.Insert(areas);
            perfilOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarArea()
        {

            WFBS.Business.Entities.PerfildeCargo pc = new WFBS.Business.Entities.PerfildeCargo();
            pc.ID_PERFIL_DE_CARGO = 21;
            pc.DESCRIPCION = "Actualización de prueba";
            pc.OBSOLETO = 0;

            List<WFBS.Business.Entities.Area> areas = new List<WFBS.Business.Entities.Area>();
            WFBS.Business.Entities.Area a = new WFBS.Business.Entities.Area();
            a.ID_AREA = 2;
            areas.Add(a);


            bool esperando = true;
            PerfildeCargoOperacion perfilOp = new PerfildeCargoOperacion(pc);
            bool actua = perfilOp.Actualize(areas);
            perfilOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarArea()
        {

            WFBS.Business.Entities.PerfildeCargo pc = new WFBS.Business.Entities.PerfildeCargo();

            pc.ID_PERFIL_DE_CARGO = 22;

            bool esperando = true;
            PerfildeCargoOperacion perfilOp = new PerfildeCargoOperacion(pc);
            bool actua = perfilOp.Delete();
            perfilOp = null;
            Assert.AreEqual(esperando, actua);



        }


    }
}
