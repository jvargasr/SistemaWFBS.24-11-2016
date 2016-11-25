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

                us.RUT = this._usuario.RUT;
                us.NOMBRE = this._usuario.NOMBRE;
                us.ID_AREA = this._usuario.ID_AREA;
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

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                this._usuario.RUT = us.RUT;
                this._usuario.NOMBRE = us.NOMBRE;
                this._usuario.SEXO = us.SEXO;
                this._usuario.ID_AREA = Convert.ToInt32(us.ID_AREA);
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

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT);

                us.NOMBRE = this._usuario.NOMBRE;
                us.ID_AREA = this._usuario.ID_AREA;
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

        public List<Usuario> Listar()
        {
            var usuariosBDD = from u in CommonBC.ModeloWFBS.USUARIO
                              join a in CommonBC.ModeloWFBS.AREA on u.ID_AREA equals a.ID_AREA
                              join p in CommonBC.ModeloWFBS.PERFIL on u.ID_PERFIL equals p.ID_PERFIL

                              select new Usuario
                              {
                                  RUT = u.RUT,
                                  NOMBRE = u.NOMBRE,
                                  SEXO = (u.SEXO == "M" ? "Masculino" : u.SEXO == "F" ? "Femenino" : "No determinado"),
                                  JEFE_RESPECTIVO = u.JEFE_RESPECTIVO,
                                  Perfil = p.TIPO_USUARIO,
                                  Area = (p.TIPO_USUARIO == "administrador" ? "" : (p.TIPO_USUARIO == "jefe" || p.TIPO_USUARIO == "funcionario") ? a.NOMBRE : "No determinado"),
                                  PASSWORD = u.PASSWORD,
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
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this._usuario.RUT && b.PASSWORD == this._usuario.PASSWORD && b.ID_PERFIL == 1);

                this._usuario.RUT = us.RUT;
                this._usuario.NOMBRE = us.NOMBRE;
                this._usuario.SEXO = us.SEXO;
                this._usuario.ID_AREA = Convert.ToInt32(us.ID_AREA);
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
                us.ID_AREA = Convert.ToInt32(item.ID_AREA);
                us.ID_PERFIL = Convert.ToInt32(item.ID_PERFIL);
                us.JEFE_RESPECTIVO = item.JEFE_RESPECTIVO;
                us.OBSOLETO = Convert.ToInt32(item.OBSOLETO);

                usuariosController.Add(us);
            }
            return usuariosController;
        }
        #endregion Metodos
    }


}
