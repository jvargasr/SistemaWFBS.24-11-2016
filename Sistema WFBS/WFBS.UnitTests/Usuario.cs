using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Usuario
    {
        [TestMethod]
        public void crearUsuario()
        {

            WFBS.Business.Entities.Usuario a = new WFBS.Business.Entities.Usuario();
            a.RUT = "5555555-5";
            a.ID_PERFIL = 2;
            a.ID_PERFIL_DE_CARGO = 3;
            a.NOMBRE = "miguel";
            a.SEXO = "M";
            a.JEFE_RESPECTIVO = null;
            a.PASSWORD = "1111111";
            a.OBSOLETO = 0;

            bool esperando = true;
            UsuarioOperacion usOp = new UsuarioOperacion(a);
            bool actua = usOp.Create();
            usOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarUsuario()
        {

            WFBS.Business.Entities.Usuario a = new WFBS.Business.Entities.Usuario();
            a.RUT = "5555555-5";
            a.ID_PERFIL = 2;
            a.ID_PERFIL_DE_CARGO = 3;
            a.NOMBRE = "miguel";
            a.SEXO = "M";
            a.JEFE_RESPECTIVO = null;
            a.PASSWORD = "1111111";
            a.OBSOLETO = 1;


            bool esperando = true;
            UsuarioOperacion usOp = new UsuarioOperacion(a);
            bool actua = usOp.Update();
            usOp = null;
            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarUsuario()
        {

            WFBS.Business.Entities.Usuario a = new WFBS.Business.Entities.Usuario();
            a.RUT = "18661447-k";

            bool esperando = true;
            UsuarioOperacion usOp = new UsuarioOperacion(a);
            bool actua = usOp.Delete();
            usOp = null;
            Assert.AreEqual(esperando, actua);



        }

    }
}
