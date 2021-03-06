﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.DataAccess;
using WFBS.Business.Contracts;
using WFBS.Business.Entities;
using WFBS.DAL;
using WFBS.Business.Log;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase UsuarioOperacion, contenedor de los metodos relacionados a la Entidad Usuario.
    /// </summary>
    public class UsuarioOperacion : IOperations<Usuario>
    {
        private Usuario _usuario { get; set; }
        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_us"> Recibe un parametro del tipo Usuario</param>
        public UsuarioOperacion(Usuario _us)
        {
            this._usuario = _us;
        }

        #region IOperations
        /// <summary>
        /// Crea una entidad Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = new USUARIO();

                us.RUT = this._usuario.RUT;
                us.NOMBRE = this._usuario.NOMBRE;
                us.ID_PERFIL_DE_CARGO = this._usuario.ID_PERFIL_DE_CARGO;
                us.ID_PERFIL = this._usuario.ID_PERFIL;
                us.SEXO = this._usuario.SEXO;
                us.JEFE_RESPECTIVO = this._usuario.JEFE_RESPECTIVO;
                us.PASSWORD = this._usuario.PASSWORD;
                us.OBSOLETO = this._usuario.OBSOLETO;

                user.USUARIO.Add(us);
                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Usuario: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lee una entidad Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Read()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                this._usuario.RUT = us.RUT;
                this._usuario.NOMBRE = us.NOMBRE;
                this._usuario.SEXO = us.SEXO;
                this._usuario.ID_PERFIL_DE_CARGO = Convert.ToInt32(us.ID_PERFIL_DE_CARGO);
                this._usuario.ID_PERFIL = Convert.ToInt32(us.ID_PERFIL);
                this._usuario.JEFE_RESPECTIVO = us.JEFE_RESPECTIVO;
                this._usuario.PASSWORD = us.PASSWORD;
                this._usuario.OBSOLETO = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Usuario: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Actualiza una entidad Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Update()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                us.NOMBRE = this._usuario.NOMBRE;
                us.ID_PERFIL_DE_CARGO = this._usuario.ID_PERFIL_DE_CARGO;
                us.ID_PERFIL = this._usuario.ID_PERFIL;
                us.SEXO = this._usuario.SEXO;
                us.JEFE_RESPECTIVO = this._usuario.JEFE_RESPECTIVO;
                us.PASSWORD = this._usuario.PASSWORD;
                us.OBSOLETO = this._usuario.OBSOLETO;

                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Usuario: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Desactiva una entidad Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                us.OBSOLETO = 1;

                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar el Usuario: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Lista todas las entidades de Usuario.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Área almacenadas en la BD </returns>
        public List<Usuario> Listar()
        {
            var usuariosBDD = from u in CommonBC.ModeloWFBS.USUARIO
                              join a in CommonBC.ModeloWFBS.PERFIL_DE_CARGO on u.ID_PERFIL_DE_CARGO equals a.ID_PERFIL_DE_CARGO
                              join p in CommonBC.ModeloWFBS.PERFIL on u.ID_PERFIL equals p.ID_PERFIL

                              select new Usuario
                              {
                                  RUT = u.RUT,
                                  NOMBRE = u.NOMBRE,
                                  SEXO = (u.SEXO == "M" ? "Masculino" : u.SEXO == "F" ? "Femenino" : "No determinado"),
                                  JEFE_RESPECTIVO = u.JEFE_RESPECTIVO,
                                  Perfil = p.TIPO_USUARIO,
                                  //Area=a.DESCRIPCION,
                                  Area = (p.TIPO_USUARIO == "Administrador" ? "" : (p.TIPO_USUARIO == "Jefe" || p.TIPO_USUARIO == "Funcionario") ? a.DESCRIPCION : "No determinado"),
                                  PASSWORD = u.PASSWORD,
                                  Obs = (u.OBSOLETO == 0 ? "No" : u.OBSOLETO == 1 ? "Si" : "No determinado"),
                              };
            return usuariosBDD.ToList();
        }
        #endregion IOperations

        #region Metodos
        /// <summary>
        /// Valida que los datos ingresados correspandan al Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool ValidarUsuario()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT && b.PASSWORD == this._usuario.PASSWORD && b.ID_PERFIL == 1);

                this._usuario.RUT = us.RUT;
                this._usuario.NOMBRE = us.NOMBRE;
                this._usuario.SEXO = us.SEXO;
                this._usuario.ID_PERFIL_DE_CARGO = Convert.ToInt32(us.ID_PERFIL_DE_CARGO);
                this._usuario.ID_PERFIL = Convert.ToInt32(us.ID_PERFIL);
                this._usuario.JEFE_RESPECTIVO = us.JEFE_RESPECTIVO;
                this._usuario.PASSWORD = us.JEFE_RESPECTIVO;
                this._usuario.OBSOLETO = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Desactiva una entidad Usuario.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Desactivado()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                if (us.OBSOLETO == 0)
                {
                    user = null;
                    return true;
                }
                user = null;
                return false;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar la vigencia del Usuario: " + ex.ToString());
                return false;
            }
        }

        private List<Usuario> GenerarListado(List<DAL.USUARIO> usuariosBDD)
        {
            List<Usuario> usuariosController = new List<Usuario>();

            foreach (DAL.USUARIO item in usuariosBDD)
            {
                Usuario us = new Usuario();

                us.RUT = item.RUT;
                us.NOMBRE = item.NOMBRE;
                us.SEXO = item.SEXO;
                us.ID_PERFIL_DE_CARGO = Convert.ToInt32(item.ID_PERFIL_DE_CARGO);
                us.ID_PERFIL = Convert.ToInt32(item.ID_PERFIL);
                us.JEFE_RESPECTIVO = item.JEFE_RESPECTIVO;
                us.OBSOLETO = Convert.ToInt32(item.OBSOLETO);

                usuariosController.Add(us);
            }
            return usuariosController;
        }
        /// <summary>
        /// Obtiene todos los funcinoarios asociados a un jefe.
        /// </summary>
        /// returns>Retorna una variable Lista con todas los funcionarios asociados a un jefe </returns>
        public List<Usuario> ObtenerFuncionariosPorJefe(string rut)
        {
            Usuario us = new Usuario();
            us.RUT = rut;
            UsuarioOperacion usOp = new UsuarioOperacion(us);
            try
            {
                usOp.Read();
                string nombre = us.NOMBRE;
                var Jefes = CommonBC.ModeloWFBS.USUARIO.Where(usu => usu.JEFE_RESPECTIVO == nombre);
                return (GenerarListado(Jefes.ToList()));
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener funcionarios por jefe: " + ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// Valida que los datos ingresados correspandan al perfil de jefe.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool validarFuncionarioJefe()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT && b.PASSWORD == this._usuario.PASSWORD && b.ID_PERFIL != 1);

                this._usuario.RUT = us.RUT;
                this._usuario.NOMBRE = us.NOMBRE;
                this._usuario.SEXO = us.SEXO;
                this._usuario.ID_PERFIL_DE_CARGO = Convert.ToInt32(us.ID_PERFIL_DE_CARGO);
                this._usuario.ID_PERFIL = Convert.ToInt32(us.ID_PERFIL);
                this._usuario.JEFE_RESPECTIVO = us.JEFE_RESPECTIVO;
                this._usuario.PASSWORD = us.JEFE_RESPECTIVO;
                this._usuario.OBSOLETO = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }

        #endregion Metodos
    }


}
