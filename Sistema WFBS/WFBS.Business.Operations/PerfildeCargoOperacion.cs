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
using WFBS.Business.Operations;

namespace WFBS.Business.Operations
{
    /// <summary>
    /// Clase AreaOperacion, contenedor de los metodos relacionados a la Entidad Área.
    /// </summary>
    public class PerfildeCargoOperacion : IOperations<PerfildeCargo>
    {
        private PerfildeCargo _perfildeCargo { get; set; }
        /// <summary>
        /// Constructor inicializador de la Clase.
        /// </summary>
        /// <param name="_ar"> Recibe un parametro del tipo Área</param>
        public PerfildeCargoOperacion(PerfildeCargo _perfildeC)
        {
            this._perfildeCargo = _perfildeC;
        }

        #region IOperations
        public bool Create()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lee una entidad Área.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Read()
        {
            try
            {
                DAL.WFBSEntities perfilesDC = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO pc = perfilesDC.PERFIL_DE_CARGO.First(p => p.ID_PERFIL_DE_CARGO == this._perfildeCargo.ID_PERFIL_DE_CARGO);
                Area a = new Area();
                AreaOperacion ao = new AreaOperacion(a);

                this._perfildeCargo.ID_PERFIL_DE_CARGO = Convert.ToInt32(pc.ID_PERFIL_DE_CARGO);
                this._perfildeCargo.DESCRIPCION = pc.DESCRIPCION;
                this._perfildeCargo.OBSOLETO = Convert.ToInt32(pc.OBSOLETO);
                string txt = "";
                foreach (AREA item in pc.AREA)
                {
                    txt += item.ID_AREA.ToString() + ",";
                }
                this._perfildeCargo.areas = txt;

                perfilesDC = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Desactiva una entidad Área.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities perfilesDC = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO pc = perfilesDC.PERFIL_DE_CARGO.First(p => p.ID_PERFIL_DE_CARGO == this._perfildeCargo.ID_PERFIL_DE_CARGO);
                // pcargo.OBSOLETA = 1;
                pc.OBSOLETO = 1;
                perfilesDC.SaveChanges();
                perfilesDC = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public List<PerfildeCargo> Listar()
        {
            throw new NotImplementedException();
        }
        #endregion IOperations

        #region Metodos
        /// <summary>
        /// Inserta en la base de datos un perfil de cargo
        /// </summary>
        /// <param name="areasSelec"> Recibe un parametro del tipo entidad Área</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Insert(List<Area> areasSelec)
        {
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO p = new PERFIL_DE_CARGO();
                DAL.AREA a = new AREA();
                p.DESCRIPCION = this._perfildeCargo.DESCRIPCION;
                p.OBSOLETO = this._perfildeCargo.OBSOLETO;
                modelo.PERFIL_DE_CARGO.Add(p);
                foreach (Area areaselec in areasSelec)
                {
                    a = modelo.AREA.First(b => b.ID_AREA == areaselec.ID_AREA);
                    p.AREA.Add(a);
                }

                modelo.PERFIL_DE_CARGO.Add(p);
                modelo.SaveChanges();
                modelo = null;
                return true;

            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Modifica en la base de datos un perfil de cargo
        /// </summary>
        /// <param name="areasSelec"> Recibe un parametro del tipo entidad Área</param>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        public bool Actualize(List<Area> areasSelec)
        {
            string areas = string.Empty;
            try
            {
                DAL.WFBSEntities modelo = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO p = modelo.PERFIL_DE_CARGO.First(pc => pc.ID_PERFIL_DE_CARGO == this._perfildeCargo.ID_PERFIL_DE_CARGO);
                DAL.AREA a = new AREA();
                var AreasBDD = CommonBC.ModeloWFBS.AREA;
                p.ID_PERFIL_DE_CARGO = this._perfildeCargo.ID_PERFIL_DE_CARGO;
                p.DESCRIPCION = this._perfildeCargo.DESCRIPCION;
                p.OBSOLETO = this._perfildeCargo.OBSOLETO;
                foreach (AREA item in AreasBDD)
                {
                    var delete = modelo.AREA.First(b => b.ID_AREA == item.ID_AREA);
                    p.AREA.Remove(delete);
                    modelo.SaveChanges();
                }

                foreach (Area areaselec in areasSelec)
                {
                    a = modelo.AREA.First(b => b.ID_AREA == areaselec.ID_AREA);
                    p.AREA.Add(a);
                }

                modelo.SaveChanges();
                modelo = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

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

                pc.ID_PERFIL_DE_CARGO = item.ID_PERFIL_DE_CARGO;
                pc.DESCRIPCION = item.DESCRIPCION;
                pc.OBSOLETO = Convert.ToInt32(item.OBSOLETO);

                perfilesdecargoController.Add(pc);
            }

            return perfilesdecargoController;
        }
        #endregion Metodos
    }
}
