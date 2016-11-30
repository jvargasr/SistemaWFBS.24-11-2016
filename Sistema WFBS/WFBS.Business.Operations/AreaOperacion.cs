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
    public class AreaOperacion : IOperations<Area>
    {
        private Area _area { get; set; }

        public AreaOperacion(Area _ar)
        {
            this._area = _ar;
        }

        #region IOperations
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = new AREA();

                ar.ID_AREA = this._area.ID_AREA;
                ar.NOMBRE = this._area.NOMBRE;
                ar.ABREVIACION = this._area.ABREVIACION;
                ar.OBSOLETA = this._area.OBSOLETA;

                area.AREA.Add(ar);
                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Área:" + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(b => b.ID_AREA == this._area.ID_AREA);

                this._area.ID_AREA = ar.ID_AREA;
                this._area.NOMBRE = ar.NOMBRE;
                this._area.ABREVIACION = ar.ABREVIACION;
                this._area.OBSOLETA = ar.OBSOLETA;
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Área:" + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(b => b.ID_AREA == this._area.ID_AREA);

                ar.NOMBRE = this._area.NOMBRE;
                ar.ID_AREA = this._area.ID_AREA;
                ar.ABREVIACION = this._area.ABREVIACION;
                ar.OBSOLETA = this._area.OBSOLETA;

                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Área: " + ex.ToString());
                return false;
            }
        }
      
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(c => c.ID_AREA == this._area.ID_AREA);

                ar.OBSOLETA = 1;

                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar la Área: " + ex.ToString());
                return false;
            }
        }

        public List<Area> Listar()
        {
            var AreasBDD = from c in CommonBC.ModeloWFBS.AREA

                           select new Area
                           {
                               ID_AREA = c.ID_AREA,
                               NOMBRE = c.NOMBRE,
                               ABREVIACION = c.ABREVIACION,
                               obs = (c.OBSOLETA == 0 ? "No" : c.OBSOLETA == 1 ? "Si" : "No determinado"),
                           };
            return AreasBDD.ToList();
        }
        #endregion IOperations

        #region Metodos
        public bool Insert(List<Competencia> comSelec)
        {
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.AREA ar = new AREA();
                DAL.COMPETENCIA c = new COMPETENCIA();

                ar.ID_AREA = this._area.ID_AREA;
                ar.NOMBRE = this._area.NOMBRE;
                ar.ABREVIACION = this._area.ABREVIACION;
                ar.OBSOLETA = this._area.OBSOLETA;

                modelo.AREA.Add(ar);
                foreach (Competencia item in comSelec)
                {
                    c = modelo.COMPETENCIA.First(b => b.ID_COMPETENCIA == item.ID_COMPETENCIA);
                    ar.COMPETENCIA.Add(c);
                }
                modelo.AREA.Add(ar);
                modelo.SaveChanges();
                modelo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Área:" + ex.ToString());
                return false;
            }
        }
        private List<Area> GenerarListado(List<DAL.AREA> areasBDD)
        {
            List<Area> areasController = new List<Area>();

            foreach (DAL.AREA item in areasBDD)
            {
                Area ar = new Area();

                ar.ID_AREA = item.ID_AREA;
                ar.OBSOLETA = item.OBSOLETA;
                ar.ABREVIACION = item.ABREVIACION;
                ar.NOMBRE = item.NOMBRE;
                Read();

                areasController.Add(ar);
            }

            return areasController;
        }
        public List<Area> areasPorPerfildeCargo(PerfildeCargo pc)
        {
            DAL.WFBSEntities modelo = new WFBSEntities();
            DAL.PERFIL_DE_CARGO pcargo = modelo.PERFIL_DE_CARGO.First(b => b.ID_PERFIL_DE_CARGO == pc.ID_PERFIL_DE_CARGO);
            return GenerarListado(pcargo.AREA.ToList());
        }
        public string competenciasArea(Area ar)
        {
            string txt = null;
            try
            {
                DAL.WFBSEntities modelo = new WFBSEntities();
                DAL.AREA area = modelo.AREA.First(b => b.ID_AREA == ar.ID_AREA);
                foreach (DAL.COMPETENCIA item in area.COMPETENCIA)
                {
                    txt += item.ID_COMPETENCIA.ToString() + ",";
                }
                return txt;
            }
            catch (Exception)
            {
                return txt;
            }
        }
        public bool Actualize(List<Competencia> comSelec)
        {
            string areas = string.Empty;
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.AREA p = modelo.AREA.First(pc => pc.ID_AREA == this._area.ID_AREA);
                DAL.COMPETENCIA a = new COMPETENCIA();
                var ComBDD = CommonBC.ModeloWFBS.COMPETENCIA;
                p.ID_AREA = this._area.ID_AREA;
                p.NOMBRE = this._area.NOMBRE;
                p.OBSOLETA = this._area.OBSOLETA;
                foreach (COMPETENCIA item in ComBDD)
                {
                    var delete = modelo.COMPETENCIA.First(b => b.ID_COMPETENCIA == item.ID_COMPETENCIA);
                    p.COMPETENCIA.Remove(delete);
                    modelo.SaveChanges();
                }

                foreach (Competencia item in comSelec)
                {
                    a = modelo.COMPETENCIA.First(b => b.ID_COMPETENCIA == item.ID_COMPETENCIA);
                    p.COMPETENCIA.Add(a);
                }

                modelo.SaveChanges();
                modelo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Área: " + ex.ToString());
                return false;
            }
        }
        #endregion Metodos
    }
}
