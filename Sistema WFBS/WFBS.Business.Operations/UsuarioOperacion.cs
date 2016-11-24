using System;
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
    public class UsuarioOperacion : IOperations<Usuario>
    {
        private Usuario _usuario { get; set; }

        public UsuarioOperacion(Usuario _us)
        {
            this._usuario = _us;
        }

        #region IOperations
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = new USUARIO();

                us.RUT = this._usuario.Rut;
                us.NOMBRE = this._usuario.Nombre;
                us.ID_AREA = this._usuario.Id_Area;
                us.ID_PERFIL = this._usuario.Id_Perfil;
                us.SEXO = this._usuario.Sexo;
                us.JEFE_RESPECTIVO = this._usuario.Jefe;
                us.PASSWORD = this._usuario.Password;
                us.OBSOLETO = this._usuario.Obsoleto;

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

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.Rut);

                this._usuario.Rut = us.RUT;
                this._usuario.Nombre = us.NOMBRE;
                this._usuario.Sexo = us.SEXO;
                this._usuario.Id_Area = Convert.ToInt32(us.ID_AREA);
                this._usuario.Id_Perfil = Convert.ToInt32(us.ID_PERFIL);
                this._usuario.Jefe = us.JEFE_RESPECTIVO;
                this._usuario.Password = us.PASSWORD;
                this._usuario.Obsoleto = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.Rut);

                us.NOMBRE = this._usuario.Nombre;
                us.ID_AREA = this._usuario.Id_Area;
                us.ID_PERFIL = this._usuario.Id_Perfil;
                us.SEXO = this._usuario.Sexo;
                us.JEFE_RESPECTIVO = this._usuario.Jefe;
                us.PASSWORD = this._usuario.Password;
                us.OBSOLETO = this._usuario.Obsoleto;

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

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.Rut);

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

        public List<Usuario> Listar()
        {
            var usuariosBDD = from u in CommonBC.ModeloWFBS.USUARIO
                              join a in CommonBC.ModeloWFBS.AREA on u.ID_AREA equals a.ID_AREA
                              join p in CommonBC.ModeloWFBS.PERFIL on u.ID_PERFIL equals p.ID_PERFIL

                              select new Usuario
                              {
                                  Rut = u.RUT,
                                  Nombre = u.NOMBRE,
                                  Sexo = (u.SEXO == "M" ? "Masculino" : u.SEXO == "F" ? "Femenino" : "No determinado"),
                                  Jefe = u.JEFE_RESPECTIVO,
                                  Perfil = p.TIPO_USUARIO,
                                  Area = (p.TIPO_USUARIO == "administrador" ? "" : (p.TIPO_USUARIO == "jefe" || p.TIPO_USUARIO == "funcionario") ? a.NOMBRE : "No determinado"),
                                  Password = u.PASSWORD,
                                  Obs = (u.OBSOLETO == 0 ? "No" : u.OBSOLETO == 1 ? "Si" : "No determinado"),
                              };
            return usuariosBDD.ToList();
        }
        #endregion IOperations

        #region Metodos

        public bool ValidarUsuario()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.Rut && b.PASSWORD == this._usuario.Password && b.ID_PERFIL == 1);

                this._usuario.Rut = us.RUT;
                this._usuario.Nombre = us.NOMBRE;
                this._usuario.Sexo = us.SEXO;
                this._usuario.Id_Area = Convert.ToInt32(us.ID_AREA);
                this._usuario.Id_Perfil = Convert.ToInt32(us.ID_PERFIL);
                this._usuario.Jefe = us.JEFE_RESPECTIVO;
                this._usuario.Password = us.JEFE_RESPECTIVO;
                this._usuario.Obsoleto = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Desactivado()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.Rut);

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

                us.Rut = item.RUT;
                us.Nombre = item.NOMBRE;
                us.Sexo = item.SEXO;
                us.Id_Area = Convert.ToInt32(item.ID_AREA);
                us.Id_Perfil = Convert.ToInt32(item.ID_PERFIL);
                us.Jefe = item.JEFE_RESPECTIVO;
                us.Obsoleto = Convert.ToInt32(item.OBSOLETO);

                usuariosController.Add(us);
            }
            return usuariosController;
        }
        #endregion Metodos
    }


}
