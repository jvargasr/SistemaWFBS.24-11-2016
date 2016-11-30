using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WFBS.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceWFBS
    {
        #region JavaServices

        /// <summary>
        /// Operación de contrato log.
        /// </summary>
        /// <param name="msgxml">Recibe un parametro msgxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool log(string msgxml);

        /// <summary>
        /// Operación de contrato InsertarEvaluación.
        /// </summary>
        /// <param name="evaluacionxml">Recibe un parametro evaluacionxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool InsertarEvaluacion(string evaluacionxml);

        /// <summary>
        /// Operación de contrato insertarAuditoria.
        /// </summary>
        /// <param name="auditoriaxml">Recibe un parametro auditoriaxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool insertarAuditoria(string auditoriaxml);

        /// <summary>
        /// Operación de contrato obtenerComptentenciasArea.
        /// </summary>
        /// <param name="areaxml">Recibe un parametro areaxml serializado en xml</param>
        /// <returns>Retorna una lista de Competencias por Área, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string obtenerComptentenciasArea(string areaxml);

        /// <summary>
        /// Operación de contrato obtenerHabilidadesCompetencia.
        /// </summary>
        /// <param name="competenciaxml">Recibe un parametro competenciaxml serializado en xml</param>
        /// <returns>Retorna una lista de Habilidades por Competencia, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string obtenerHabilidadesCompetencia(string competenciaxml);

        /// <summary>
        /// Operación de contrato usuarioEvaluado.
        /// </summary>
        /// <param name="evaluacionxml">Recibe un parametro evaluacionxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool usuarioEvaluado(string evaluacionxml);

        /// <summary>
        /// Operación de contrato obtenerFuncionariosPorJefe.
        /// </summary>
        /// <param name="usuariojefexml">Recibe un parametro usuariojefexml serializado en xml</param>
        /// <returns>Retorna una lista de Funcionarios por jefe a cargo, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string obtenerFuncionariosPorJefe(string usuariojefexml);

        /// <summary>
        /// Operación de contrato validarFuncionarioJefe.
        /// </summary>
        /// <param name="usuarioxml">Recibe un parametro usuarioxml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool validarFuncionarioJefe(string usuarioxml);

        #endregion JavaServices

        #region C#Services
        #region Competencia

        /// <summary>
        /// Operación de contrato CrearCompetencia.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearCompetencia(string xml);

        /// <summary>
        /// Operación de contrato ActualizarCompetencia.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarCompetencia(string xml);

        /// <summary>
        /// Operación de contrato EliminarCompetencia.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarCompetencia(string xml);

        /// <summary>
        /// Operación de contrato LeerCompetencia.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna una entidad de Competencias, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerCompetencia(string xml);

        /// <summary>
        /// Operación de contrato LeerCompetencias.
        /// </summary>
        /// <returns>Retorna una lista de Competencias, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerCompetencias();
        #endregion Competencia

        //---------------------------------------------------------//

        #region Habilidad
        /// <summary>
        /// Operación de contrato CrearHabilidad.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearHabilidad(string xml);

        /// <summary>
        /// Operación de contrato ActualizarHabilidad.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarHabilidad(string xml);

        /// <summary>
        /// Operación de contrato EliminarHabilidad.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarHabilidad(string xml);

        /// <summary>
        /// Operación de contrato LeerHabilidad.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un una entidad Habilidad, serializada acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerHabilidad(string xml);

        /// <summary>
        /// Operación de contrato LeerHabPorCom.
        /// </summary>
        /// <param name="id">Recibe un parametro id serializado en xml</param>
        /// <returns>Retorna una lista de Habilidades por Competencia, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerHabPorCom(string id);
        #endregion Habilidad

        //---------------------------------------------------------//

        #region PeriododeEvaluacion
        /// <summary>
        /// Operación de contrato CrearPeriodoEvaluacion.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearPeriodoEvaluacion(string xml);

        /// <summary>
        /// Operación de contrato ActualizarPeriodoEvaluacion.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarPeriodoEvaluacion(string xml);

        /// <summary>
        /// Operación de contrato EliminarPeriodoEvaluacion.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarPeriodoEvaluacion(string xml);

        /// <summary>
        /// Operación de contrato LeerPeriodoEvaluacion.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna una entidad de Periodo de Evaluación, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerPeriodoEvaluacion(string xml);

        /// <summary>
        /// Operación de contrato LeerPeriodosEvaluaciones.
        /// </summary>
        /// <returns>Retorna una lista de Periodos de Evaluación, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerPeriodosEvaluaciones();


        [OperationContract]
        int periodoActivo();

        #endregion PeriododeEvaluacion

        //---------------------------------------------------------//

        #region Usuario
        /// <summary>
        /// Operación de contrato ValidarUsuario.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ValidarUsuario(string xml);

        /// <summary>
        /// Operación de contrato Desactivado.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool Desactivado(string xml);

        /// <summary>
        /// Operación de contrato CrearUsuario.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearUsuario(string xml);

        /// <summary>
        /// Operación de contrato ActualizarUsuario.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarUsuario(string xml);

        /// <summary>
        /// Operación de contrato EliminarUsuario.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarUsuario(string xml);

        /// <summary>
        /// Operación de contrato ActualizarUsuario.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna una entidad de Usuario, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerUsuario(string xml);

        /// <summary>
        /// Operación de contrato LeerUsuarios.
        /// </summary>
        /// <returns>Retorna una lista de los Usuarios, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerUsuarios();
        #endregion Usuario

        //---------------------------------------------------------//

        #region Area
        /// <summary>
        /// Operación de contrato CrearArea.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearArea(string xml);

        /// <summary>
        /// Operación de contrato ActualizarArea.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarArea(string xml);

        /// <summary>
        /// Operación de contrato EliminarArea.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarArea(string xml);

        /// <summary>
        /// Operación de contrato LeerArea.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna una entidad de Área, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerArea(string xml);

        /// <summary>
        /// Operación de contrato LeerAreas.
        /// </summary>
        /// <returns>Retorna una lista de Áreas, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerAreas();
        #endregion Area

        //---------------------------------------------------------//
        #region PerfildeCargo
        /// <summary>
        /// Operación de contrato CrearPerfildeCargo.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <param name="areas">Recibe una lista areas serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool CrearPerfildeCargo(string xml, string areas);
        //bool CrearPerfildeCargo(string xml, List<Area> areaSeleccionada);

        /// <summary>
        /// Operación de contrato ActualizarPerfildeCargo.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <param name="areas">Recibe una lista areas serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool ActualizarPerfildeCargo(string xml, string areas);

        /// <summary>
        /// Operación de contrato EliminarPerfildeCargo.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        bool EliminarPerfildeCargo(string xml);

        /// <summary>
        /// Operación de contrato LeerPerfildeCargo.
        /// </summary>
        /// <param name="xml">Recibe un parametro xml serializado en xml</param>
        /// <returns>Retorna una entidad de Perfil de Cargo, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerPerfildeCargo(string xml);

        /// <summary>
        /// Operación de contrato LeerPerfilesdeCargo.
        /// </summary>
        /// <returns>Retorna una lista de Perfiles de Cargo, serializada en xml acorde a la ejecucion satisfactoria del metodo</returns>
        [OperationContract]
        string LeerPerfilesdeCargo();
        #endregion PerfildeCargo

        #endregion C#Services

        // TODO: agregue aquí sus operaciones de servicio
    }
}
