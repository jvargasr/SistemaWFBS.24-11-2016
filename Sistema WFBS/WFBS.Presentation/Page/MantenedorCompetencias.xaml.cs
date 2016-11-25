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
    /// Lógica de interacción para MantenedorCompetencias.xaml
    /// </summary>
    public partial class MantenedorCompetencias : System.Windows.Controls.Page
    {

        public MantenedorCompetencias()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void dgCompetencias_Loaded(object sender, RoutedEventArgs e)
        {
            Competencia com = new Competencia();
            CompetenciaOperacion comOp = new CompetenciaOperacion(com);

            dgCompetencias.ItemsSource = comOp.Listar(); ;

            dgCompetencias.Columns[0].Visibility = Visibility.Collapsed;
            dgCompetencias.Columns[4].Visibility = Visibility.Collapsed;
            dgCompetencias.Columns[7].Visibility = Visibility.Collapsed;
            //dgCompetencias.Columns[8].Visibility = Visibility.Collapsed;
            dgCompetencias.Columns[9].Visibility = Visibility.Collapsed;
            //dgCompetencias.Columns[5].Visibility = Visibility.Collapsed;
            //dgCompetencias.Columns[7].Visibility = Visibility.Hidden;
            dgCompetencias.Columns[1].Header = "Nombre";
            dgCompetencias.Columns[2].Header = "Descripción";
            dgCompetencias.Columns[3].Header = "Sigla";
            dgCompetencias.Columns[5].Header = "Nivel Óptimo";
            dgCompetencias.Columns[6].Header = "Pregunta Asociada";
            dgCompetencias.Columns[8].Header = "Obsoleta";


        }

        private void btnAgregarCompetencia_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarCompetencia nextPage = new InsertarCompetencia();
            navService.Navigate(nextPage);
        }

        private void btnModificarCompetencia_Click(object sender, RoutedEventArgs e)
        {
            if (dgCompetencias.SelectedItem != null)
            {
                Competencia com = (Competencia)dgCompetencias.SelectedItem;
                int id = Convert.ToInt32(com.ID_COMPETENCIA);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarCompetencia nextPage = new ModificarCompetencia(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Competencia antes", "Aviso");
            }
        }
        private void btnEliminarCompetencia_Click(object sender, RoutedEventArgs e)
        {

            if (dgCompetencias.SelectedItem != null)
            {
                Competencia com = (Competencia)dgCompetencias.SelectedItem;

                if (com.OBSOLETA == 1)
                {
                    MessageBox.Show("La Competencia seleccionada se encuentra desactivada", "Aviso");
                }
                else
                {
                    XML formato = new XML();
                    string xml = formato.Serializar(com);
                    WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                    if (servicio.EliminarCompetencia(xml))
                    {
                        MessageBox.Show("La Competencia seleccionada ha sido desactivada", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorCompetencias nextPage = new MantenedorCompetencias();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar la Competencia", "Aviso");
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una Competencia antes", "Aviso");
            }
        }
        private void btnMantenerHabilidad_Click(object sender, RoutedEventArgs e)
        {
            if (dgCompetencias.SelectedItem != null)
            {
                Competencia com = (Competencia)dgCompetencias.SelectedItem;
                int id = Convert.ToInt32(com.ID_COMPETENCIA);
                NavigationService navService = NavigationService.GetNavigationService(this);
                MantenedorHabilidades nextPage = new MantenedorHabilidades(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Competencia antes", "Aviso");
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
