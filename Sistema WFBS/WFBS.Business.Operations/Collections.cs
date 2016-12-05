using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.Business.Entities;
using WFBS.Business.DataAccess;
using WFBS.DAL;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase Collections, encargada de devolver listas relacionadas a las entidades de Business.Entities
    /// </summary>
    public class Collections
    {
        /* Usuario */
        /// <summary>
        /// Lista todas las entidades de Usuario.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Usuario almacenadas en la BD</returns>
        public List<Usuario> ReadAllUsuarios()
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
                                  Area = (p.TIPO_USUARIO == "administrador" ? "" : (p.TIPO_USUARIO == "jefe" || p.TIPO_USUARIO == "funcionario") ? a.DESCRIPCION : "No determinado"),
                                  PASSWORD = u.PASSWORD,
                                  Obs = (u.OBSOLETO == 0 ? "No" : u.OBSOLETO == 1 ? "Si" : "No determinado"),
                              };
            return usuariosBDD.ToList();
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

        /* Area */
        /// <summary>
        /// Lista todas las entidades de Áreas.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Áreas almacenadas en la BD</returns>
        public List<Area> ReadAllAreas()
        {
            var AreasBDD = from c in CommonBC.ModeloWFBS.AREA /*AreasBDD*/

                           select new Area
                           {
                               ID_AREA = c.ID_AREA,
                               NOMBRE = c.NOMBRE,
                               ABREVIACION = c.ABREVIACION,
                               obs = (c.OBSOLETA == 0 ? "No" : c.OBSOLETA == 1 ? "Si" : "No determinado"),
                           };
            return AreasBDD.ToList();
        }

        private List<Area> GenerarListado(List<DAL.AREA> areasBDD)
        {
            List<Area> areasController = new List<Area>();

            foreach (DAL.AREA item in areasBDD)
            {
                Area ar = new Area();

                ar.ID_AREA =item.ID_AREA;
                ar.OBSOLETA = item.OBSOLETA;
                ar.ABREVIACION = item.ABREVIACION;
                ar.NOMBRE = item.NOMBRE;

                areasController.Add(ar);
            }

            return areasController;
        }
        /* Perfiles de cargo */
        /// <summary>
        /// Lista todas las entidades de PerfildeCargo.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de PerfildeCargo almacenadas en la BD</returns>
        public List<PerfildeCargo> ReadAllPerfilesdeCargo()
        {
            var PCargoBDD = from pc in CommonBC.ModeloWFBS.PERFIL_DE_CARGO

                            select new PerfildeCargo
                            {
                                ID_PERFIL_DE_CARGO = pc.ID_PERFIL_DE_CARGO,
                                DESCRIPCION = pc.DESCRIPCION,
                                Obs = (pc.OBSOLETO == 0 ? "No" : pc.OBSOLETO == 1 ? "Si" : "No determinado"),
                            };
            return PCargoBDD.ToList();
        }

        private List<PerfildeCargo> GenerarListado(List<DAL.PERFIL_DE_CARGO> perfilesdecargoBDD)
        {
            List<PerfildeCargo> perfilesdecargoController = new List<PerfildeCargo>();

            foreach (DAL.PERFIL_DE_CARGO item in perfilesdecargoBDD)
            {
                PerfildeCargo pc = new PerfildeCargo();

                pc.ID_PERFIL_DE_CARGO = Convert.ToInt32(item.ID_PERFIL_DE_CARGO);
                pc.DESCRIPCION = item.DESCRIPCION;
                pc.OBSOLETO = Convert.ToInt32(item.OBSOLETO);

                perfilesdecargoController.Add(pc);
            }

            return perfilesdecargoController;
        }

        /* Perfil */
        /// <summary>
        /// Lista todas las entidades de Perfil.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Perfil almacenadas en la BD</returns>
        public List<Perfil> ReadAllPerfiles()
        {
            List<DAL.PERFIL> perfilesBDD = CommonBC.ModeloWFBS.PERFIL.ToList();
            return GenerarListadoPerfil(perfilesBDD);
        }

        private List<Perfil> GenerarListadoPerfil(List<PERFIL> perfilesBDD)
        {
            List<Perfil> perfilesController = new List<Perfil>();

            foreach (DAL.PERFIL item in perfilesBDD)
            {
                Perfil p = new Perfil();

                p.ID_PERFIL = Convert.ToInt32(item.ID_PERFIL);
                p.TIPO_USUARIO = item.TIPO_USUARIO;

                perfilesController.Add(p);
            }

            return perfilesController;
        }


        /* Obtener competencias por area */
        /// <summary>
        /// Lista todas las entidades de Competencias.
        /// </summary>
        /// <param name="id_area">Recibe un parametro entero correspondiente al id de un Área</param>
        /// <returns>Retorna todas las id de Competencias que corresponden a la Área relacionada</returns>
        public List<int> CompetenciasPorArea(int id_area)
        {
            List<int> ids_competencias = new List<int>();
            var CompetenciasDB = CommonBC.ModeloWFBS.COMPETENCIA; //.Where(relacion n:m para sacar competencia por área)
            foreach (var item in CompetenciasDB)
            {
                ids_competencias.Add((int)item.ID_COMPETENCIA);
            }
            return ids_competencias;
        }

        /* Obtener periodo de evaluación activo */
        /// <summary>
        /// Lista el Periodo de Evaluación vigente.
        /// </summary>
        /// <returns> Retorna el id del Periodo de Evaluación vigente en el sistema</returns>
        public int ObtenerPeriodoEvaluacion()
        {
            DateTime hoy = DateTime.Now;
            var id = CommonBC.ModeloWFBS.PERIODO_EVALUACION.Where(p => hoy >= p.FECHA_INICIO && hoy <= (p.FECHA_INICIO.AddDays((double)p.VIGENCIA))).Select(p => p.ID_PERIODO_EVALUACION).First();
            return (int)id;
        }

        /* Reporte evaluación por grupo */
        /// <summary>
        /// Lista de las notas de los funcionarios por cada Competencia.
        /// </summary>
        /// <param name="id_area">Recibe un parametro id referente a un Área</param>
        /// <param name="id_competencia">Recibe un parametro id referente a una Competencia</param>
        /// <returns>Retorna las notas de los funcionarios evaluados por Área</returns>
        public List<float> ObtenerNotasCompetencia(int id_area, int id_competencia)
        {//      Obtener las brechas de todos los funcionarios
            PeriodoEvaluacion pe = new PeriodoEvaluacion();
            PeriodoEvaluacionOperacion peOp = new PeriodoEvaluacionOperacion(pe);

            int id_periodo = peOp.periodoEvaluacionActivo();

            var notasDB = CommonBC.ModeloWFBS.EVALUACION.Where(e => e.ID_PERIODO_EVALUACION == id_periodo && e.ID_AREA == id_area
            && e.ID_COMPETENCIA == id_competencia).Select(e => e.NOTA_ENCUESTA);
            List<float> notas = new List<float>();
            foreach (var item in notasDB)
            {
                notas.Add((float)item);
            }
            return notas;
        }
        /* Fin reporte grupal en construcción */

        /* Usuario Jefe */
        /// <summary>
        /// Lista los Jefes almacenados en el sistema.
        /// </summary>
        /// <returns>Retorna un listado con todos los Jefes registrados en el sistema</returns>
        public List<Usuario> ObtenerJefes()
        {
            var Jefes = CommonBC.ModeloWFBS.USUARIO.Where(usu => usu.ID_PERFIL == 2);
            return (GenerarListado(Jefes.ToList()));
        }

        /* Competencia */
        /// <summary>
        /// Lista todas las entidades de Competencia.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Competencia almacenadas en la BD</returns>
        public List<Competencia> ReadAllCompetencias()
        {
            /*List<Modelo.COMPETENCIA> CompetenciasBDD = CommonBC.ModeloWfbs.COMPETENCIA.ToList();
            return GenerarListadoCompetencia(CompetenciasBDD);*/

            var CompetenciasBDD = from c in CommonBC.ModeloWFBS.COMPETENCIA /*usuariosBDD*/

                                  select new Competencia
                                  {
                                      ID_COMPETENCIA = c.ID_COMPETENCIA,
                                      NOMBRE = c.NOMBRE,
                                      DESCRIPCION = c.DESCRIPCION,
                                      SIGLA = c.SIGLA,
                                      Obs = (c.OBSOLETA == 0 ? "No" : c.OBSOLETA == 1 ? "Si" : "No determinado"),
                                      NIVEL_OPTIMO_ESPERADO = c.NIVEL_OPTIMO_ESPERADO,
                                      PREGUNTA_ASOCIADA = c.PREGUNTA_ASOCIADA
                                  };
            return CompetenciasBDD.ToList();
        }

        private List<Competencia> GenerarListadoCompetencia(List<DAL.COMPETENCIA> CompetenciaBDD)
        {
            List<Competencia> competenciasController = new List<Competencia>();

            foreach (DAL.COMPETENCIA item in CompetenciaBDD)
            {
                Competencia com = new Competencia();

                com.ID_COMPETENCIA = Convert.ToInt32(item.ID_COMPETENCIA);
                com.NOMBRE = item.NOMBRE;
                com.DESCRIPCION = item.DESCRIPCION;
                com.SIGLA = item.SIGLA;
                com.OBSOLETA = Convert.ToInt32(item.OBSOLETA);
                com.NIVEL_OPTIMO_ESPERADO = Convert.ToInt32(item.NIVEL_OPTIMO_ESPERADO);
                com.PREGUNTA_ASOCIADA = item.PREGUNTA_ASOCIADA;

                competenciasController.Add(com);
            }

            return competenciasController;
        }


        // Periodo Evaluacion
        /// <summary>
        /// Lista todas las entidades de PeriodoEvaluacion.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de PeriodoEvaluacion almacenadas en la BD</returns>
        public List<PeriodoEvaluacion> ReadAllPeriodos()
        {
            List<DAL.PERIODO_EVALUACION> periodosBDD = CommonBC.ModeloWFBS.PERIODO_EVALUACION.ToList();
            return GenerarListadoPeriodos(periodosBDD);
        }

        private List<PeriodoEvaluacion> GenerarListadoPeriodos(List<DAL.PERIODO_EVALUACION> periodosBDD)
        {
            List<PeriodoEvaluacion> periodosController = new List<PeriodoEvaluacion>();

            foreach (DAL.PERIODO_EVALUACION item in periodosBDD)
            {
                PeriodoEvaluacion ar = new PeriodoEvaluacion();

                ar.ID_PERIODO_EVALUACION = Convert.ToInt32(item.ID_PERIODO_EVALUACION);
                ar.FECHA_INICIO = item.FECHA_INICIO;
                ar.VIGENCIA = Convert.ToInt32(item.VIGENCIA);
                ar.PORCENTAJE_EVALUACION = Convert.ToInt32(item.PORCENTAJE_EVALUACION);
                ar.PORCENTAJE_AUTOEVALUACION = Convert.ToInt32(item.PORCENTAJE_AUTOEVALUACION);

                periodosController.Add(ar);
            }

            return periodosController;
        }


        /* Habilidad */
        /// <summary>
        /// Lista todas las entidades de Habilidades.
        /// </summary>
        /// <returns>Retorna una variable Lista con todas las entidades de Habilidades almacenadas en la BD</returns>
        public List<Habilidad> ReadAllHabilidades()
        {
            List<DAL.HABILIDAD> habilidadesBDD = CommonBC.ModeloWFBS.HABILIDAD.ToList();
            return GenerarListado(habilidadesBDD);
        }

        private List<Habilidad> GenerarListado(List<DAL.HABILIDAD> habilidadesBDD)
        {
            List<Habilidad> habilidadesController = new List<Habilidad>();

            foreach (DAL.HABILIDAD item in habilidadesBDD)
            {
                Habilidad hab = new Habilidad();

                hab.ID_HABILIDAD = Convert.ToInt32(item.ID_HABILIDAD);
                hab.ID_COMPETENCIA = Convert.ToInt32(item.ID_COMPETENCIA);
                hab.NOMBRE = item.NOMBRE;
                hab.ORDEN_ASIGNADO = Convert.ToInt32(item.ORDEN_ASIGNADO);
                hab.ALTERNATIVA_PREGUNTA = item.ALTERNATIVA_PREGUNTA;

                habilidadesController.Add(hab);
            }

            return habilidadesController;

        }

        /// <summary>
        /// Lista todas las entidades de Habilidades.
        /// </summary>
        /// <param name="id">Recibe un parametro id Competencia</param>
        /// <returns>Retorna una variable Lista con todas las entidades de Habilidades por Competencia almacenadas en la BD</returns>
        public List<Habilidad> ObtenerHabPorCom(int id)
        {
            decimal id_com = Convert.ToDecimal(id);
            //var habilidad = CommonBC.ModeloWfbs.HABILIDAD.Where(h => h.ID_COMPETENCIA == id);
            var HabiBDD = from h in CommonBC.ModeloWFBS.HABILIDAD
                          join c in CommonBC.ModeloWFBS.COMPETENCIA on h.ID_COMPETENCIA equals c.ID_COMPETENCIA
                          where h.ID_COMPETENCIA == id_com
                          select new Habilidad
                          {
                              ID_HABILIDAD = h.ID_HABILIDAD,
                              Competencia = c.NOMBRE,
                              NOMBRE = h.NOMBRE,
                              Orden = h.ORDEN_ASIGNADO,
                              ALTERNATIVA_PREGUNTA = h.ALTERNATIVA_PREGUNTA
                          };
            return HabiBDD.ToList();
            //return (GenerarListado(habilidad.ToList()));
        }
    }
}
