using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WFBS.Business.Operations;
using WFBS.Business.Entities;
using WFBS.IT.Communication;
using WFBS.Business.Log;

namespace WFBS.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceWFBS : IServiceWFBS
    {
        #region JavaServices
        public bool log(string msgxml)
        {
            try
            {
                XML formato = new XML();
                Logger.log(formato.Deserializar<string>(msgxml));
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo registrar log externo: " + ex.ToString());
                return false;
            }         
        }

        public bool InsertarEvaluacion(string evaluacionxml)
        {
            try
            {
                XML formato = new XML();
                Evaluacion ev = formato.Deserializar<Evaluacion>(evaluacionxml);
                EvaluacionOperacion evOp = new EvaluacionOperacion(ev);
                return evOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool insertarAuditoria(string auditoriaxml)
        {
            try
            {
                XML formato = new XML();
                Auditoria au=formato.Deserializar<Auditoria>(auditoriaxml);
                AuditoriaOperacion auOp = new AuditoriaOperacion(au);
                return auOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la auditoría: " + ex.ToString());
                return false;
            }
        }

        public string obtenerComptentenciasArea(string areaxml)
        {
            try
            {
                XML formato = new XML();
                Area ar = formato.Deserializar<Area>(areaxml);
                Competencia com = new Competencia();
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                return formato.Serializar<List<Competencia>>(comOp.competenciasArea(ar));
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtenet competencias por Área: " + ex.ToString());
                return null;
            }
        }

        public string obtenerHabilidadesCompetencia(string competenciaxml)
        {
            try
            {
                XML formato = new XML();
                Competencia com = formato.Deserializar<Competencia>(competenciaxml);
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                return formato.Serializar(comOp.ObtenerHabPorCom(com.ID_COMPETENCIA));
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la Competencia: " + ex.ToString());
                return null;
            }
        }

        public bool usuarioEvaluado(string evaluacionxml)
        {
            try
            {
                XML formato = new XML();
                Evaluacion ev = formato.Deserializar<Evaluacion>(evaluacionxml);
                EvaluacionOperacion evOp = new EvaluacionOperacion(ev);
                return evOp.usuarioEvaluado();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo verificar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public string obtenerFuncionariosPorJefe(string usuariojefexml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(usuariojefexml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return formato.Serializar<List<Usuario>>(usOp.ObtenerFuncionariosPorJefe(us.RUT));
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo realizar la petición en Usuarios: " + ex.ToString());
                return null;
            }
        }


        /*

        public bool InsertarEvaluacion(string evaluacionxml)
        {
            Evaluacion e = new Evaluacion(evaluacionxml);
            return e.Create();
        }
        public bool insertarAuditoria(string auditoriaxml)
        {
            Auditoria a = new Auditoria(auditoriaxml);
            return a.Create();
        }
        public string obtenerComptenteciasArea(string id_area)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Area), new XmlRootAttribute("Area"));
            StringReader stringReader = new StringReader(id_area);
            Area a = (Area)serializer.Deserialize(stringReader);

            ColeccionCompetencia cc = new ColeccionCompetencia();
            return cc.Serializar(cc.ReadAllCompetencias());
        }
        public string obtenerHabilidadesCompetencia(string id_competencia)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Competencia), new XmlRootAttribute("Competencia"));
            StringReader stringReader = new StringReader(id_competencia);
            Competencia c = (Competencia)serializer.Deserialize(stringReader);
            ColeccionHabilidad ch = new ColeccionHabilidad();
            return ch.Serializar(ch.ObtenerHabPorCom(c.Id_competencia));
        }
        public bool usuarioEvaluado(string evaluacionxml)
        {
            Evaluacion e = new Evaluacion(evaluacionxml);
            return e.usuarioEvaluado();
        }
        public string obtenerFuncionariosPorJefe(string usuariojefexml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Usuario), new XmlRootAttribute("Usuario"));
            StringReader stringReader = new StringReader(usuariojefexml);
            Usuario u = (Usuario)serializer.Deserialize(stringReader);

            ColeccionUsuario c = new ColeccionUsuario();
            return c.Serializar(c.ObtenerFuncionariosPorJefe(u.Rut));
        }*/

        #endregion

        #region Competencia
        // Competencia
        public bool CrearCompetencia(string xml)
        {
            try
            {
                XML formato = new XML();
                Competencia com = formato.Deserializar<Competencia>(xml);
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                return comOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarCompetencia(string xml)
        {
            try
            {
                XML formato = new XML();
                Competencia com = formato.Deserializar<Competencia>(xml);
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                return comOp.Update();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarCompetencia(string xml)
        {
            try
            {
                XML formato = new XML();
                Competencia com = formato.Deserializar<Competencia>(xml);
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                return comOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Desactivar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public string LeerCompetencia(string xml)
        {
            try
            {
                XML formato = new XML();
                Competencia com = formato.Deserializar<Competencia>(xml);
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                //comOp.Read();
                if (comOp.Read())
                {
                    return formato.Serializar(com);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer la Competencia: " + ex.ToString());
                return null;
            }
        }

        public string LeerCompetencias()
        {

            XML formato = new XML();
            //List<Competencia> competencias = formato.Deserializar<List<Competencia>>();

            //Sstring xmlS = formato.Serializar<List<Competencia>>(competencias);

            return null;
        }
        #endregion

        //---------------------------------------------------------//

        #region Habilidad
        public bool CrearHabilidad(string xml)
        {
            try
            {
                XML formato = new XML();
                Habilidad hab = formato.Deserializar<Habilidad>(xml);
                HabilidadOperacion habOp = new HabilidadOperacion(hab);
                return habOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarHabilidad(string xml)
        {
            try
            {
                XML formato = new XML();
                Habilidad hab = formato.Deserializar<Habilidad>(xml);
                HabilidadOperacion habOp = new HabilidadOperacion(hab);
                return habOp.Update();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarHabilidad(string xml)
        {
            try
            {
                XML formato = new XML();
                Habilidad hab = formato.Deserializar<Habilidad>(xml);
                HabilidadOperacion habOp = new HabilidadOperacion(hab);
                return habOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Eliminar la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public string LeerHabilidad(string xml)
        {
            try
            {
                XML formato = new XML();
                Habilidad hab = formato.Deserializar<Habilidad>(xml);
                HabilidadOperacion habOp = new HabilidadOperacion(hab);
                if (habOp.Read())
                {
                    return formato.Serializar(hab);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer la Habilidad: " + ex.ToString());
                return null;
            }
        }

        public string LeerHabilidades()
        {
            throw new NotImplementedException();
        }

        public string LeerHabPorCom(int id)
        {
            throw new NotImplementedException();
        }

        #endregion Habilidad

        //---------------------------------------------------------//

        #region PeriodoEvaluacion

        public bool CrearPeriodoEvaluacion(string xml)
        {
            try
            {
                XML formato = new XML();
                PeriodoEvaluacion periodoEva = formato.Deserializar<PeriodoEvaluacion>(xml);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(periodoEva);
                return periodoOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarPeriodoEvaluacion(string xml)
        {
            try
            {
                XML formato = new XML();
                PeriodoEvaluacion periodoEva = formato.Deserializar<PeriodoEvaluacion>(xml);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(periodoEva);
                return periodoOp.Update();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarPeriodoEvaluacion(string xml)
        {
            try
            {
                XML formato = new XML();
                PeriodoEvaluacion periodoEva = formato.Deserializar<PeriodoEvaluacion>(xml);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(periodoEva);
                return periodoOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Desactivar el Periodo de Evaluación: " + ex.ToString());
                return false;
            }
        }

        public string LeerPeriodoEvaluacion(string xml)
        {
            try
            {
                XML formato = new XML();
                PeriodoEvaluacion periodoEva = formato.Deserializar<PeriodoEvaluacion>(xml);
                PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(periodoEva);
                if (periodoOp.Read())
                {
                    return formato.Serializar(periodoEva);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer el Periodo de Evaluación: " + ex.ToString());
                return null;
            }
        }
        public string LeerPeriodosEvaluaciones()
        {
            throw new NotImplementedException();
        }

        #endregion PeriodoEvaluacion

        //---------------------------------------------------------//

        #region Usuario

        public bool ValidarUsuario(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.ValidarUsuario();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Desactivado(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.Desactivado();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo validar la Vigencia del Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool CrearUsuario(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarUsuario(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.Update();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarUsuario(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Desactivar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public string LeerUsuario(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                if (usOp.Read())
                {
                    return formato.Serializar(us);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer el Usuario: " + ex.ToString());
                return null;
            }
        }

        public string LeerUsuarios()
        {
            throw new NotImplementedException();
        }

        #endregion Usuario

        //---------------------------------------------------------//

        #region Area
        public bool CrearArea(string xml)
        {
            try
            {
                XML formato = new XML();
                Area ar = formato.Deserializar<Area>(xml);
                AreaOperacion areaOp = new AreaOperacion(ar);
                return areaOp.Create();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear la Área: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarArea(string xml)
        {
            try
            {
                XML formato = new XML();
                Area ar = formato.Deserializar<Area>(xml);
                AreaOperacion areaOp = new AreaOperacion(ar);
                return areaOp.Update();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar la Área: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarArea(string xml)
        {
            try
            {
                XML formato = new XML();
                Area ar = formato.Deserializar<Area>(xml);
                AreaOperacion areaOp = new AreaOperacion(ar);
                return areaOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Desactivar la Área: " + ex.ToString());
                return false;
            }
        }

        public string LeerArea(string xml)
        {
            try
            {
                XML formato = new XML();
                Area ar = formato.Deserializar<Area>(xml);
                AreaOperacion areaOp = new AreaOperacion(ar);
                if (areaOp.Read())
                {
                    return formato.Serializar(ar);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer la Área: " + ex.ToString());
                return null;
            }
        }

        public string LeerAreas()
        {
            throw new NotImplementedException();
        }

        #endregion Area

        //---------------------------------------------------------//

        #region PerfildeCargo

        public bool CrearPerfildeCargo(string xml)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                List<Area> area = new List<Area>();
                return perfilCOp.Insert(area);
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarPerfildeCargo(string xml)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                List<Area> area = new List<Area>();
                return perfilCOp.Actualize(area);
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Actualizar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public bool EliminarPerfildeCargo(string xml)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                return perfilCOp.Delete();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Desactivar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public string LeerPerfildeCargo(string xml)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                //comOp.Read();
                if (perfilCOp.Read())
                {
                    return formato.Serializar(perfilC);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Leer el Perfil de Cargo: " + ex.ToString());
                return null;
            }
        }

        public string LeerPerfilesdeCargo()
        {
            throw new NotImplementedException();
        }

        #endregion PerfildeCargo
    }
}
