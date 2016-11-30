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
    /// Lógica de interacción para MantenedorArea.xaml
    /// </summary>
    public partial class MantenedorArea : System.Windows.Controls.Page
    {
        public MantenedorArea()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void btnModificarArea_Click(object sender, RoutedEventArgs e)
        {
            if (dgArea.SelectedItem != null)
            {
                Area ar = (Area)dgArea.SelectedItem;
                int id = Convert.ToInt32(ar.ID_AREA);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarArea nextPage = new ModificarArea(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Área antes. Aviso:");
            }
        }

        private void btnEliminarArea_Click(object sender, RoutedEventArgs e)
        {
            if (dgArea.SelectedItem != null)
            {
                Area ar = (Area)dgArea.SelectedItem;
                if (ar.obs == "Si")
                {
                    MessageBox.Show("La Área seleccionada se encuentra desactivada", "Aviso");
                }
                else
                {
                    ar.ID_AREA = ar.ID_AREA;
                    XML formato = new XML();
                    string xml = formato.Serializar(ar);
                    WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                    if (servicio.EliminarArea(xml))
                    {
                        MessageBox.Show("La Área seleccionada ha sido desactivada", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorArea nextPage = new MantenedorArea();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar la Área", "Aviso");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Área antes", "Aviso:");
            }
        }

        private void btnAgregarArea_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarArea nextPage = new InsertarArea();
            navService.Navigate(nextPage);
        }

        private void dgArea_Loaded(object sender, RoutedEventArgs e)
        {
            Area ar = new Area();
            AreaOperacion arOp = new AreaOperacion(ar);
            WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();
            XML formato = new XML();

            dgArea.ItemsSource = formato.Deserializar<List<Area>>(servicio.LeerAreas());

            dgArea.Columns[3].Visibility = Visibility.Hidden;
            dgArea.Columns[1].Header = "Nombre";
            dgArea.Columns[2].Header = "Abreviación";
            dgArea.Columns[4].Header = "Obsoleta";


            dgArea.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void dgArea_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            /*Para cambiar nombre de cabecera*/
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Page2 nextPage = new Page2();
            navService.Navigate(nextPage);
        }
    }
}
