﻿using System;
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

        [OperationContract]
        bool log(string msgxml);

        [OperationContract]
        bool InsertarEvaluacion(string evaluacionxml);

        [OperationContract]
        bool insertarAuditoria(string auditoriaxml);

        [OperationContract]
        string obtenerComptentenciasArea(string areaxml);

        [OperationContract]
        string obtenerHabilidadesCompetencia(string competenciaxml);

        [OperationContract]
        bool usuarioEvaluado(string evaluacionxml);

        [OperationContract]
        string obtenerFuncionariosPorJefe(string usuariojefexml);

        [OperationContract]
        bool validarFuncionarioJefe(string usuarioxml);

        [OperationContract]
        string periodoActivo();

        #endregion JavaServices

        #region C#Services
        #region Competencia
        [OperationContract]
        bool CrearCompetencia(string xml);

        [OperationContract]
        bool ActualizarCompetencia(string xml);

        [OperationContract]
        bool EliminarCompetencia(string xml);

        [OperationContract]
        string LeerCompetencia(string xml);

        [OperationContract]
        string LeerCompetencias();
        #endregion Competencia

        //---------------------------------------------------------//

        #region Habilidad
        [OperationContract]
        bool CrearHabilidad(string xml);

        [OperationContract]
        bool ActualizarHabilidad(string xml);

        [OperationContract]
        bool EliminarHabilidad(string xml);

        [OperationContract]
        string LeerHabilidad(string xml);

        [OperationContract]
        string LeerHabPorCom(string id);
        #endregion Habilidad

        //---------------------------------------------------------//

        #region PeriododeEvaluacion
        [OperationContract]
        bool CrearPeriodoEvaluacion(string xml);

        [OperationContract]
        bool ActualizarPeriodoEvaluacion(string xml);

        [OperationContract]
        bool EliminarPeriodoEvaluacion(string xml);

        [OperationContract]
        string LeerPeriodoEvaluacion(string xml);

        [OperationContract]
        string LeerPeriodosEvaluaciones();
        #endregion PeriododeEvaluacion

        //---------------------------------------------------------//

        #region Usuario
        [OperationContract]
        bool ValidarUsuario(string xml);

        [OperationContract]
        bool Desactivado(string xml);

        [OperationContract]
        bool CrearUsuario(string xml);

        [OperationContract]
        bool ActualizarUsuario(string xml);

        [OperationContract]
        bool EliminarUsuario(string xml);

        [OperationContract]
        string LeerUsuario(string xml);

        [OperationContract]
        string LeerUsuarios();
        #endregion Usuario

        //---------------------------------------------------------//

        #region Area
        [OperationContract]
        bool CrearArea(string xml);

        [OperationContract]
        bool ActualizarArea(string xml);

        [OperationContract]
        bool EliminarArea(string xml);

        [OperationContract]
        string LeerArea(string xml);

        [OperationContract]
        string LeerAreas();
        #endregion Area

        //---------------------------------------------------------//
        #region PerfildeCargo
        [OperationContract]
        bool CrearPerfildeCargo(string xml, string areas);
        //bool CrearPerfildeCargo(string xml, List<Area> areaSeleccionada);

        [OperationContract]
        bool ActualizarPerfildeCargo(string xml, string areas);

        [OperationContract]
        bool EliminarPerfildeCargo(string xml);

        [OperationContract]
        string LeerPerfildeCargo(string xml);

        [OperationContract]
        string LeerPerfilesdeCargo();
        #endregion PerfildeCargo

        #endregion C#Services

        // TODO: agregue aquí sus operaciones de servicio
    }
}
