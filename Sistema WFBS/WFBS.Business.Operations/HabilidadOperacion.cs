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
    public class HabilidadOperacion : IOperations<Habilidad>
    {
        private Habilidad _habilidad { get; set; }

        public HabilidadOperacion(Habilidad _habi)
        {
            this._habilidad = _habi;
        }

        #region IOperations
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = new HABILIDAD();
                hab.ID_HABILIDAD = this._habilidad.Id_Habilidad;
                hab.ID_COMPETENCIA = this._habilidad.Id_Competencia;
                hab.NOMBRE = this._habilidad.Nombre;
                hab.ORDEN_ASIGNADO = this._habilidad.Orden_Asignado;
                hab.ALTERNATIVA_PREGUNTA = this._habilidad.Alternativa_Pregunta;

                habi.HABILIDAD.Add(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.Id_Habilidad);

                this._habilidad.Id_Habilidad = Convert.ToInt32(hab.ID_HABILIDAD);
                this._habilidad.Id_Competencia = Convert.ToInt32(hab.ID_COMPETENCIA);
                this._habilidad.Nombre = hab.NOMBRE;
                this._habilidad.Orden_Asignado = Convert.ToInt32(hab.ORDEN_ASIGNADO);
                this._habilidad.Alternativa_Pregunta = hab.ALTERNATIVA_PREGUNTA;

                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.Id_Habilidad);

                hab.ID_HABILIDAD = this._habilidad.Id_Habilidad;
                hab.ID_COMPETENCIA = this._habilidad.Id_Competencia;
                hab.NOMBRE = this._habilidad.Nombre;
                hab.ORDEN_ASIGNADO = this._habilidad.Orden_Asignado;
                hab.ALTERNATIVA_PREGUNTA = this._habilidad.Alternativa_Pregunta;

                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this._habilidad.Id_Habilidad);

                habi.HABILIDAD.Remove(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Eliminar la Habilidad: " + ex.ToString());
                return false;
            }
        }

        public List<Habilidad> Listar() // default va sin parametros
        {
            List<DAL.HABILIDAD> habilidadesBDD = CommonBC.ModeloWFBS.HABILIDAD.ToList();
            return GenerarListado(habilidadesBDD);
        }
        #endregion IOperations
        #region Metodos
        private List<Habilidad> GenerarListado(List<DAL.HABILIDAD> habilidadesBDD)
        {
            List<Habilidad> habilidadesController = new List<Habilidad>();

            foreach (DAL.HABILIDAD item in habilidadesBDD)
            {
                Habilidad hab = new Habilidad();

                hab.Id_Habilidad = Convert.ToInt32(item.ID_HABILIDAD);
                hab.Id_Competencia = Convert.ToInt32(item.ID_COMPETENCIA);
                hab.Nombre = item.NOMBRE;
                hab.Orden_Asignado = Convert.ToInt32(item.ORDEN_ASIGNADO);
                hab.Alternativa_Pregunta = item.ALTERNATIVA_PREGUNTA;

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
                              Id_Hab = h.ID_HABILIDAD,
                              Competencia = c.NOMBRE,
                              Nombre = h.NOMBRE,
                              Orden = h.ORDEN_ASIGNADO,
                              Alternativa_Pregunta = h.ALTERNATIVA_PREGUNTA
                          };
            return HabiBDD.ToList();
            //return (GenerarListado(habilidad.ToList()));
        }
        #endregion Metodos
    }
}
