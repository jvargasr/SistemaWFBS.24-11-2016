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
    /// Lógica de interacción para MantenedorPerfilesdeCargo.xaml
    /// </summary>
    public partial class MantenedorPerfilesdeCargo : System.Windows.Controls.Page
    {
        public MantenedorPerfilesdeCargo()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void dgPerfildeCargo_Loaded(object sender, RoutedEventArgs e)
        {
            Collections col = new Collections();
            dgPerfildeCargo.ItemsSource = col.ReadAllPerfilesdeCargo();
            dgPerfildeCargo.Columns[0].Visibility = Visibility.Collapsed;
            dgPerfildeCargo.Columns[2].Visibility = Visibility.Collapsed;
            dgPerfildeCargo.Columns[4].Visibility = Visibility.Collapsed;
            dgPerfildeCargo.Columns[1].Header = "Descripción";
            dgPerfildeCargo.Columns[3].Header = "Obsoleto";


        }
        private void dgPerfildeCargo_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*Para cambiar nombre de cabecera*/


        }

        private void btnAgregaPerfildeCargo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            IngresarPerfildeCargo nextPage = new IngresarPerfildeCargo();
            navService.Navigate(nextPage);
        }

        private void btnModificarPerfildeCargo_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (dgPerfildeCargo.SelectedItem != null)
            {
                PerfildeCargo pc = (PerfildeCargo)dgPerfildeCargo.SelectedItem;
                id = Convert.ToInt32(pc.ID_PERFIL_DE_CARGO);
                //MessageBox.Show(us.Rut, "Éxito!");
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarPerfildeCargo nextPage = new ModificarPerfildeCargo(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Perfil de Cargo antes", "Aviso");
            }
        }
        private void btnEliminarPerfildeCargo_Click(object sender, RoutedEventArgs e)
        {
            if (dgPerfildeCargo.SelectedItem != null)
            {
                PerfildeCargo pc = (PerfildeCargo)dgPerfildeCargo.SelectedItem;
                if (pc.Obs == "Si")
                {
                    MessageBox.Show("El Perfil de Cargo seleccionado se encuentra desactivado", "Aviso");
                }
                else
                {
                    XML formato = new XML();
                    string xml = formato.Serializar(pc);
                    WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                    pc.ID_PERFIL_DE_CARGO = Convert.ToInt32(pc.ID_PERFIL_DE_CARGO);
                    if (servicio.EliminarPerfildeCargo(xml))
                    {
                        MessageBox.Show("El Perfil de Cargo seleccionado ha sido desactivado", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorPerfilesdeCargo nextPage = new MantenedorPerfilesdeCargo();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar el Perfil de Cargo", "Aviso");
                    }                    
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Perfil de Cargo antes", "Aviso");
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
