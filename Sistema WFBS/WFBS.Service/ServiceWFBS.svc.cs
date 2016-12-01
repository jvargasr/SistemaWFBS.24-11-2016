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

        public string competenciasPerfildeCargo(string perfildecargoxml)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(perfildecargoxml);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);

                Competencia c = new Competencia();
                CompetenciaOperacion coOp = new CompetenciaOperacion(c);

                return formato.Serializar<List<Competencia>>(coOp.competenciasPerfilC(perfilC.ID_PERFIL_DE_CARGO));
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo leer el perfil de cargo: " + ex.ToString());
                return null;
            }
        }

        public string notaFinalUsuarioPorComp(string evaluacionxml)
        {
            try
            {
                XML formato = new XML();
                Evaluacion ev = formato.Deserializar<Evaluacion>(evaluacionxml);
                EvaluacionOperacion evOp = new EvaluacionOperacion(ev);
                return formato.Serializar<Evaluacion>(evOp.notaFinalUsuarioPorCom());
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener información de la evaluación: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Operación de servicio log.
        /// </summary>
        /// <param name="msgxml">Recibe un parametro msgxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
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

        public bool validarFuncionarioJefe(string xml)
        {
            try
            {
                XML formato = new XML();
                Usuario us = formato.Deserializar<Usuario>(xml);
                UsuarioOperacion usOp = new UsuarioOperacion(us);
                return usOp.validarFuncionarioJefe();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }

        #endregion

        #region C#Services

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

            Competencia com = new Competencia();
            CompetenciaOperacion comOp = new CompetenciaOperacion(com);
            List<Competencia> compes = comOp.Listar();
            return formato.Serializar(compes);
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

        public string LeerHabPorCom(string id)
        {
            XML formato = new XML();

            Habilidad hab = new Habilidad();
            HabilidadOperacion habOp = new HabilidadOperacion(hab);

            List<Habilidad> habis = habOp.ObtenerHabPorCom(formato.Deserializar<int>(id));
            return formato.Serializar(habis);
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
            XML formato = new XML();

            PeriodoEvaluacion pe = new PeriodoEvaluacion();
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(pe);
            List<PeriodoEvaluacion> periodos = periodoOp.Listar();
            return formato.Serializar(periodos);
        }
        public int periodoActivo()
        {
            try
            {
                XML formato = new XML();
                PeriodoEvaluacion pe = new PeriodoEvaluacion();
                PeriodoEvaluacionOperacion peOp = new PeriodoEvaluacionOperacion(pe);
                int id = peOp.periodoEvaluacionActivo();
                pe.ID_PERIODO_EVALUACION = id;
                peOp.Read();
                return Convert.ToInt32(pe.ID_PERIODO_EVALUACION);
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo obtener el periodo actual: " + ex.ToString());
                return 0;
            }

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
            XML formato = new XML();

            Usuario us = new Usuario();
            UsuarioOperacion usOp = new UsuarioOperacion(us);
            List<Usuario> usuarios = usOp.Listar();
            return formato.Serializar(usuarios);
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
            XML formato = new XML();

            Area ar = new Area();
            AreaOperacion arOp = new AreaOperacion(ar);
            List<Area> areas = arOp.Listar();
            return formato.Serializar(areas);
        }

        #endregion Area

        //---------------------------------------------------------//

        #region PerfildeCargo

        public bool CrearPerfildeCargo(string xml, string areas)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                List<Area> areasP = formato.Deserializar<List<Area>>(areas);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                return perfilCOp.Insert(areasP);
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Crear el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public bool ActualizarPerfildeCargo(string xml, string areas)
        {
            try
            {
                XML formato = new XML();
                PerfildeCargo perfilC = formato.Deserializar<PerfildeCargo>(xml);
                List<Area> areasP = formato.Deserializar<List<Area>>(areas);
                PerfildeCargoOperacion perfilCOp = new PerfildeCargoOperacion(perfilC);
                return perfilCOp.Actualize(areasP);
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
            XML formato = new XML();

            PerfildeCargo pc = new PerfildeCargo();
            PerfildeCargoOperacion perfilOp = new PerfildeCargoOperacion(pc);
            List<PerfildeCargo> perfiles = perfilOp.ReadAllPerfilesdeCargo();
            return formato.Serializar(perfiles);
        }       

        #endregion PerfildeCargo

        #endregion C#Services
    }
}
