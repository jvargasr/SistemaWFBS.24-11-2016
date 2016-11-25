using System;
using System.Collections.Generic;
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
using WFBS.IT.Communication;

namespace MasterPages.Page
{
    /// <summary>
    /// Lógica de interacción para MantenedorPeriodoEvaluacion.xaml
    /// </summary>
    public partial class MantenedorPeriodoEvaluacion : System.Windows.Controls.Page
    {
        public MantenedorPeriodoEvaluacion()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void btnAgregarPeriodo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarPeriodo nextPage = new InsertarPeriodo();
            navService.Navigate(nextPage);
        }

        private void dgPeriodo_Loaded(object sender, RoutedEventArgs e)
        {
            PeriodoEvaluacion pe = new PeriodoEvaluacion();
            PeriodoEvaluacionOperacion periodoOp = new PeriodoEvaluacionOperacion(pe);
            dgPeriodo.ItemsSource = periodoOp.Listar();
            dgPeriodo.Columns[0].Visibility = Visibility.Hidden;
            dgPeriodo.Columns[2].Header = "Vigencia (dias)";

            dgPeriodo.Columns[5].Visibility = Visibility.Collapsed;
        }

        private void btnModificarPeriodo_Click(object sender, RoutedEventArgs e)
        {
            if (dgPeriodo.SelectedItem != null)
            {
                PeriodoEvaluacion ar = (PeriodoEvaluacion)dgPeriodo.SelectedItem;
                int id = Convert.ToInt32(ar.ID_PERIODO_EVALUACION);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarPeriodo nextPage = new ModificarPeriodo(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Periodo de Evaluación antes", "Aviso:");
            }
        }

        private void btnEliminarPeriodo_Click(object sender, RoutedEventArgs e)
        {
            if (dgPeriodo.SelectedItem != null)
            {
                PeriodoEvaluacion pe = (PeriodoEvaluacion)dgPeriodo.SelectedItem;
                if (pe.VIGENCIA == 0)
                {
                    MessageBox.Show("El Periodo de Evaluación se encuentra desactivado", "Aviso");
                }
                else
                {
                    XML formato = new XML();
                    string xml = formato.Serializar(pe);
                    WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                    if (servicio.EliminarPeriodoEvaluacion(xml))
                    {
                        MessageBox.Show("El Periodo de Evaluación seleccionado ha sido desactivado", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorPeriodoEvaluacion nextPage = new MantenedorPeriodoEvaluacion();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Periodo de Evaluación", "Aviso");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Periodo de Evaluación antes", "Aviso:");
            }
        }

        private void dgPeriodo_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "fechaInicio")
            {
                e.Column.Header = "Fecha Inicio";
            }
            if (e.Column.Header.ToString() == "vigencia")
            {
                e.Column.Header = "Vigencia";
            }
            if (e.Column.Header.ToString() == "porcentajeE")
            {
                e.Column.Header = "% Evaluación";
            }
            if (e.Column.Header.ToString() == "porcentajeAE")
            {
                e.Column.Header = "% Auto-Evaluación";
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Page2 nextPage = new Page2();
            navService.Navigate(nextPage);
        }
    }
}
