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
    public class PerfildeCargoOperacion : IOperations<PerfildeCargo>
    {
        private PerfildeCargo _perfildeCargo { get; set; }

        public PerfildeCargoOperacion(PerfildeCargo _perfildeC)
        {
            this._perfildeCargo = _perfildeC;
        }

        #region IOperations
        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities perfilesDC = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO pc = perfilesDC.PERFIL_DE_CARGO.First(p => p.ID_PERFIL_DE_CARGO == this._perfildeCargo.ID_PERFIL_DE_CARGO);

                this._perfildeCargo.ID_PERFIL_DE_CARGO = Convert.ToInt32(pc.ID_PERFIL_DE_CARGO);
                this._perfildeCargo.DESCRIPCION = pc.DESCRIPCION;
                this._perfildeCargo.OBSOLETO = Convert.ToInt32(pc.OBSOLETO);

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
        public bool Insert(List<Area> areasSelec)
        {
            try
            {
                DAL.WFBSEntities perfilesDC = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO pc = new PERFIL_DE_CARGO();

                string areas = string.Empty;
                pc.OBSOLETO = this._perfildeCargo.OBSOLETO;
                pc.DESCRIPCION = this._perfildeCargo.DESCRIPCION;
                perfilesDC.PERFIL_DE_CARGO.Add(pc);
                perfilesDC.SaveChanges();
                perfilesDC = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

        public bool Actualize(List<Area> areasSelec)
        {
            string areas = string.Empty;
            try
            {
                DAL.WFBSEntities perfilesDC = new DAL.WFBSEntities();
                DAL.PERFIL_DE_CARGO pc = perfilesDC.PERFIL_DE_CARGO.First(p => p.ID_PERFIL_DE_CARGO == this._perfildeCargo.ID_PERFIL_DE_CARGO);
                pc.ID_PERFIL_DE_CARGO = this._perfildeCargo.ID_PERFIL_DE_CARGO;
                pc.DESCRIPCION = this._perfildeCargo.DESCRIPCION;
                pc.OBSOLETO = this._perfildeCargo.OBSOLETO;
                perfilesDC.SaveChanges();
                perfilesDC = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Perfil de Cargo: " + ex.ToString());
                return false;
            }
        }

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
