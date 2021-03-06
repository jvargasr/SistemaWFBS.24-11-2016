﻿using System;
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
    public partial class MantenedorHabilidades : System.Windows.Controls.Page
    {
        Habilidad hab = new Habilidad();
        Competencia com = new Competencia();
        int id_com;
        public MantenedorHabilidades()
        {
            InitializeComponent();
        }

        public MantenedorHabilidades(int id)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            HabilidadOperacion habOp = new HabilidadOperacion(hab);
            com.ID_COMPETENCIA = id;
            id_com = id;
        }

        private void dgHabilidades_Loaded(object sender, RoutedEventArgs e)
        {

            Habilidad h = new Habilidad();
            HabilidadOperacion habOp = new HabilidadOperacion(h);
            WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();
            XML formato = new XML();
            string xml = formato.Serializar(id_com);

            dgHabilidades.ItemsSource = formato.Deserializar<List<Habilidad>>(servicio.LeerHabPorCom(xml));

            dgHabilidades.Columns[0].Visibility = Visibility.Collapsed;
            dgHabilidades.Columns[1].Visibility = Visibility.Collapsed;
            dgHabilidades.Columns[3].Visibility = Visibility.Collapsed;

            dgHabilidades.Columns[2].Header = "Nombre";
            dgHabilidades.Columns[4].Header = "Alternativa de Pregunta";
            dgHabilidades.Columns[5].Header = "Competencia";
            dgHabilidades.Columns[6].Header = "Orden Asignado";
            //dgHabilidades.Columns[7].Header = "Orden Asignado";

        }
        private void dgHabilidades_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*Para cambiar nombre de cabecera*/

        }

        private void btnAgregarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarHabilidad nextPage = new InsertarHabilidad(id_com);
            navService.Navigate(nextPage);
        }

        private void btnModificarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabilidades.SelectedItem != null)
            {
                Habilidad hab = (Habilidad)dgHabilidades.SelectedItem;
                int id_habi = Convert.ToInt32(hab.ID_HABILIDAD);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarHabilidad nextPage = new ModificarHabilidad(id_habi, id_com);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Habilidad antes", "Aviso");
            }
        }
        private void btnEliminarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabilidades.SelectedItem != null)
            {
                Habilidad hab = (Habilidad)dgHabilidades.SelectedItem;
                int id_habi = Convert.ToInt32(hab.ID_HABILIDAD);
                hab.ID_HABILIDAD = id_habi;

                XML formato = new XML();
                string xml = formato.Serializar(hab);
                WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                if (servicio.EliminarHabilidad(xml))
                {
                    MessageBox.Show("La Habilidad seleccionada ha sido eliminada", "Éxito!");
                    NavigationService navService = NavigationService.GetNavigationService(this);
                    MantenedorHabilidades nextPage = new MantenedorHabilidades(id_com);
                    navService.Navigate(nextPage);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la Habilidad", "Aviso");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Habilidad antes", "Aviso");
            }
        }
        
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorCompetencias nextPage = new MantenedorCompetencias();
            navService.Navigate(nextPage);
        }
    }
}
