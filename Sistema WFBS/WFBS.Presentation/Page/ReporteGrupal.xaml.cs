﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFBS.Business.Entities;
using WFBS.Business.Operations;

namespace MasterPages.Page
{
    /// <summary>
    /// Lógica de interacción para ReporteGrupal.xaml
    /// </summary>
    public partial class ReporteGrupal : System.Windows.Controls.Page
    {
        List<PerfildeCargo> Perfiles = new List<PerfildeCargo>();
        bool bajonivelesperado = false;
        public ReporteGrupal()
        {
            Collections col = new Collections();
            InitializeComponent();

            btnTodas.Visibility = Visibility.Hidden;
            lblUserInfo.Content = Global.NombreUsuario;
            lblUserInfo.Content = Global.NombreUsuario;

            Perfiles = col.ReadAllPerfilesdeCargo();/**//**/
            TabControl1.ItemsSource = Perfiles;

        }
        private void myTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PerfildeCargo PerfilSeleccionado=(PerfildeCargo)TabControl1.SelectedItem;
            Area a = new Area();
            AreaOperacion aOp = new AreaOperacion(a);

            List<Area> areasPc = new List<Area>();
            areasPc = aOp.areasPorPerfildeCargo(PerfilSeleccionado);/**//**/
            if (areasPc != null)
            {
                this.dgEvaluaciones_Loaded(areasPc);

            }
        }
        private void dgEvaluaciones_Loaded(List<Area> areaspc)
        {
            Collections col = new Collections();

            List<Area> areas = col.ReadAllAreas();
            List<Competencia> competencias = col.ReadAllCompetencias();/**//**/
            List<float> brechas = new List<float>();
            List<PerfildeCargo> perfilesdecargo = new List<PerfildeCargo>();

            //Calcular cantidad máxima de notas, para definir el ancho de la tabla
            int nbrechas = 0;          
              
            foreach (Area a in areas)
            {
                foreach (Area item in areaspc)
                {
                    if (a.ID_AREA == item.ID_AREA)
                    {
                        foreach (Competencia com in competencias)
                        {
                            brechas = col.ObtenerNotasCompetencia((int)a.ID_AREA, (int)com.ID_COMPETENCIA);/**//**/
                            if (brechas.Count > nbrechas)
                                nbrechas = brechas.Count;
                        }
                    }
                }
            }

            //dar formato a la tabla
            DataTable table = new DataTable();
            DataColumn column;
            column = table.Columns.Add();
            column.ColumnName = "Cargo";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Competencia";
            column.DataType = typeof(string);
            if (nbrechas > 0)
            {
                for (int i = 0; i < nbrechas; i++)
                {
                    column = table.Columns.Add();
                    column.ColumnName = "N" + (i + 1);
                    column.DataType = typeof(string);
                }
            }
            column = table.Columns.Add();
            column.ColumnName = "Brecha Promedio";
            column.DataType = typeof(string);
            //Final formato tabla

            //Listar resultados en la tabla
            DataRow row;
            float sumabrechas = 0;

            foreach (Area a in areas)
            {
                    foreach (Area item in areaspc)
                    {
                    if (a.ID_AREA == item.ID_AREA)
                    {
                        foreach (Competencia com in competencias)
                        {
                            sumabrechas = 0;
                            brechas = col.ObtenerNotasCompetencia((int)a.ID_AREA, (int)com.ID_COMPETENCIA);/**//**/
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
                                float brpromedio = sumabrechas / brechas.Count;
                                row["Brecha Promedio"] = brpromedio.ToString("0.0");
                                if (bajonivelesperado == true&& Convert.ToDecimal(brpromedio)<com.NIVEL_OPTIMO_ESPERADO)
                                    table.Rows.Add(row);
                                if(bajonivelesperado==false)
                                    table.Rows.Add(row);
                            }
                        }
                    }                  
                    }
                }

            dgEvaluaciones.ItemsSource = table.AsDataView();

        }

        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgEvaluaciones.SelectAllCells();
                dgEvaluaciones.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dgEvaluaciones);
                String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                dgEvaluaciones.UnselectAllCells();
                string dir = @"C:\Reportes por grupo";
                if (!Directory.Exists(dir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir);
                }
                string arc = @"C:\Reportes por grupo\ReporteGrupal.xls";
                int i = 1;
                while(File.Exists(arc))
                {
                    arc = @"C:\Reportes por grupo\ReporteGrupal" + i+".xls";
                    i++;
                }
                System.IO.StreamWriter file1 = new System.IO.StreamWriter(arc);
                file1.WriteLine(result.Replace(',', ' '));
                file1.Close();
                MessageBox.Show("El reporte se ha descargado en la carpeta Reportes de evaluaciones, ubicada en su disco duro");
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que posea la carpeta 'Reportes de evaluaciones' en su disco C", "Alerta");
            }
        }

        private void btnBajoNivel_Click(object sender, RoutedEventArgs e)
        {
            bajonivelesperado = true;

            PerfildeCargo PerfilSeleccionado = (PerfildeCargo)TabControl1.SelectedItem;
            Area a = new Area();
            AreaOperacion aOp = new AreaOperacion(a);

            List<Area> areasPc = new List<Area>();
            areasPc = aOp.areasPorPerfildeCargo(PerfilSeleccionado);/**//**/
            if (areasPc != null)
            {
                this.dgEvaluaciones_Loaded(areasPc);

            }
            btnTodas.Visibility = Visibility.Visible;
        }
        private void btnTodas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ReporteGrupal nextPage = new ReporteGrupal();
            navService.Navigate(nextPage);
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Page2 nextPage = new Page2();
            navService.Navigate(nextPage);
        }
    }
}
