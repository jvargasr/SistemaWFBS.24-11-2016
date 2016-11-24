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
    public class CompetenciaOperacion : IOperations<Competencia>
    {
        private Competencia _competencia { get; set; }

        public CompetenciaOperacion(Competencia _com)
        {
            this._competencia = _com;
        }

        #region IOperations
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = new COMPETENCIA();

                com.ID_COMPETENCIA = this._competencia.Id_competencia;
                com.NOMBRE = this._competencia.Nombre;
                com.DESCRIPCION = this._competencia.Descripcion;
                com.SIGLA = this._competencia.Sigla;
                com.OBSOLETA = this._competencia.Obsoleta;
                com.NIVEL_OPTIMO_ESPERADO = this._competencia.Nivel_Optimo;
                com.PREGUNTA_ASOCIADA = this._competencia.Pregunta_Asociada;

                compe.COMPETENCIA.Add(com);
                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.Id_competencia);

                this._competencia.Id_competencia = Convert.ToInt32(com.ID_COMPETENCIA);
                this._competencia.Nombre = com.NOMBRE;
                this._competencia.Descripcion = com.DESCRIPCION;
                this._competencia.Sigla = com.SIGLA;
                this._competencia.Obsoleta = Convert.ToInt32(com.OBSOLETA);
                this._competencia.Nivel_Optimo = Convert.ToInt32(com.NIVEL_OPTIMO_ESPERADO);
                this._competencia.Pregunta_Asociada = com.PREGUNTA_ASOCIADA;
                if (this._competencia.Obsoleta == 0)
                {
                    this._competencia.Obs = "No";
                }
                else
                {
                    this._competencia.Obs = "Si";
                }

                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.Id_competencia);

                com.ID_COMPETENCIA = this._competencia.Id_competencia;
                com.NOMBRE = this._competencia.Nombre;
                com.DESCRIPCION = this._competencia.Descripcion;
                com.SIGLA = this._competencia.Sigla;
                com.OBSOLETA = this._competencia.Obsoleta;
                com.NIVEL_OPTIMO_ESPERADO = this._competencia.Nivel_Optimo;
                com.PREGUNTA_ASOCIADA = this._competencia.Pregunta_Asociada;

                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this._competencia.Id_competencia);

                com.OBSOLETA = 1;
                compe.SaveChanges();
                compe = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public List<Competencia> Listar()
        {
            List<DAL.COMPETENCIA> CompetenciasBDD = CommonBC.ModeloWFBS.COMPETENCIA.ToList();
            return GenerarListadoCompetencia(CompetenciasBDD);
        }
        #endregion IOperations

        #region Metodos
        private List<Competencia> GenerarListadoCompetencia(List<DAL.COMPETENCIA> CompetenciaBDD)
        {
            List<Competencia> competenciasController = new List<Competencia>();

            foreach (DAL.COMPETENCIA item in CompetenciaBDD)
            {
                Competencia com = new Competencia();

                com.Id_competencia = Convert.ToInt32(item.ID_COMPETENCIA);
                com.Nombre = item.NOMBRE;
                com.Descripcion = item.DESCRIPCION;
                com.Sigla = item.SIGLA;
                com.Obsoleta = Convert.ToInt32(item.OBSOLETA);
                com.Nivel_Optimo = Convert.ToInt32(item.NIVEL_OPTIMO_ESPERADO);
                com.Pregunta_Asociada = item.PREGUNTA_ASOCIADA;
                CompetenciaOperacion comOp = new CompetenciaOperacion(com);
                comOp.Read();
                comOp = null;
                competenciasController.Add(com);
            }

            return competenciasController;
        }
        #endregion Metodos
    }
}
