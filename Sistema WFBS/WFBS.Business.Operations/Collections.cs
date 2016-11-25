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
    public class Collections
    {
        /* Usuario */
        public List<Usuario> ReadAllUsuarios()
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

        /* Area */
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
        public List<PerfildeCargo> ReadAllPerfilesdeCargo()
        {
            //List<Modelo.PERFIL_DE_CARGO> perfilesdecargoBDD = CommonBC.ModeloWfbs.PERFIL_DE_CARGO.ToList();
            //return GenerarListado(perfilesdecargoBDD);
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


        /* reporte grupal en construcción */
        /* Obtener competencias por area */
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
        public int ObtenerPeriodoEvaluacion()
        {
            DateTime hoy = DateTime.Now;
            var id = CommonBC.ModeloWFBS.PERIODO_EVALUACION.Where(p => hoy >= p.FECHA_INICIO && hoy <= (p.FECHA_INICIO.AddDays((double)p.VIGENCIA))).Select(p => p.ID_PERIODO_EVALUACION).First();
            return (int)id;
        }

        /* Reporte evaluación por grupo */
        public List<float> ObtenerNotasCompetencia(int id_area, int id_competencia)
        {//      Obtener las brechas de todos los funcionarios
            int id_periodo = 1;//ObtenerPeriodoEvaluacion();
            var notasDB = CommonBC.ModeloWFBS.EVALUACION.Where(e => e.ID_PERIODO_EVALUACION == id_periodo && e.ID_AREA == id_area
            && e.ID_COMPETENCIA == id_competencia).Select(e => e.NOTA_ESPERADA_COMPETENCIA - e.NOTA_ENCUESTA);
            List<float> notas = new List<float>();
            foreach (var item in notasDB)
            {
                notas.Add((float)item);
            }
            return notas;
        }
        /* Fin reporte grupal en construcción */

        /* Usuario Jefe */
        public List<Usuario> ObtenerJefes()
        {
            var Jefes = CommonBC.ModeloWFBS.USUARIO.Where(usu => usu.ID_PERFIL == 2);
            return (GenerarListado(Jefes.ToList()));
        }

        /* Competencia */
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
