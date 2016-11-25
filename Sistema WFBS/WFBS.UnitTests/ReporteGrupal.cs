using System;
using System.Collections.Generic;
using System.Data;
using WFBS.Business.Entities;
using WFBS.Business.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class ReporteGrupal
    {
        [TestMethod]
        public void cantidadMaximaNotas()
        {
            Collections col = new Collections();
            List<WFBS.Business.Entities.Area> areas = new List<WFBS.Business.Entities.Area>();
            List<WFBS.Business.Entities.Competencia> competencias = new List<WFBS.Business.Entities.Competencia>();
            List<float> brechas = new List<float>();


            int nbrechas = 0;
            foreach (WFBS.Business.Entities.Area a in areas)
            {
                foreach (WFBS.Business.Entities.Competencia com in competencias)
                {
                    brechas = col.ObtenerNotasCompetencia((int)a.ID_AREA, (int)com.ID_COMPETENCIA);
                    if (brechas.Count > nbrechas)
                        nbrechas = brechas.Count;

                }
            }
        }

        [TestMethod]
        public void ListarResultados()
        {
            Collections col = new Collections();
            List<WFBS.Business.Entities.Area> areas = new List<WFBS.Business.Entities.Area>();
            List<WFBS.Business.Entities.Competencia> competencias = new List<WFBS.Business.Entities.Competencia>();
            List<float> brechas = new List<float>();
            DataRow row;
            DataTable table = new DataTable();
            float sumabrechas = 0;
            foreach (WFBS.Business.Entities.Area a in areas)
            {
                foreach (WFBS.Business.Entities.Competencia com in competencias)
                {
                    sumabrechas = 0;
                    brechas = col.ObtenerNotasCompetencia((int)a.ID_AREA, (int)com.ID_COMPETENCIA);
                    if (brechas.Count > 0)
                    {
                        row = table.NewRow();
                        row["Cargo"] = a.NOMBRE;
                        row["Competencia"] = com.NOMBRE;
                        for (int i = 0; i < brechas.Count; i++)
                        {
                            row["N" + (i + 1)] = brechas[i];
                            sumabrechas = brechas[i] + sumabrechas;
                        }
                        row["Brecha Promedio"] = (sumabrechas / brechas.Count).ToString("0.0");
                        table.Rows.Add(row);
                    }
                }
            }
        }
    }
}
