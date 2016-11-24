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
            a.Rut = "5555555-5";
            a.Id_Perfil = 2;
            a.Id_Area = 3;
            a.Nombre = "miguel";
            a.Sexo = "M";
            a.Jefe = null;
            a.Password = "1111111";
            a.Obsoleto = 0;

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
            a.Rut = "5555555-5";
            a.Id_Perfil = 2;
            a.Id_Area = 3;
            a.Nombre = "miguel";
            a.Sexo = "M";
            a.Jefe = null;
            a.Password = "1111111";
            a.Obsoleto = 1;


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
            a.Rut = "18661447-k";

            bool esperando = true;
            UsuarioOperacion usOp = new UsuarioOperacion(a);
            bool actua = usOp.Delete();
            usOp = null;
            Assert.AreEqual(esperando, actua);



        }

    }
}
