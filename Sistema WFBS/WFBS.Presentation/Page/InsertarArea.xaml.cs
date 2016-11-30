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
    /// Lógica de interacción para InsertarArea.xaml
    /// </summary>
    public partial class InsertarArea : System.Windows.Controls.Page
    {
        WFBS.Business.Operations.Collections col = new Collections();
        List<Competencia> competencias = new List<Competencia>();
        List<Area> areas = new List<Area>();
        public InsertarArea()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            rbNo.IsChecked = true;
            competencias = col.ReadAllCompetencias();
            foreach (Competencia item in competencias)
            {
                if (item.Obs == "No")
                {
                    lbCom.Items.Add(item.NOMBRE);
                }
            }
        }
        private void btnToRight_Click(object sender, RoutedEventArgs e)
        {
            lbComSeleccionadas.Items.Add(lbCom.SelectedItem);
            lbCom.Items.Remove(lbCom.SelectedItem);
            lbComSeleccionadas.Items.Refresh();
            lbCom.Items.Refresh();

        }
        private void btnToLeft_Click(object sender, RoutedEventArgs e)
        {
            lbCom.Items.Add(lbComSeleccionadas.SelectedItem);
            lbComSeleccionadas.Items.Remove(lbComSeleccionadas.SelectedItem);
            lbCom.Items.Refresh();
            lbComSeleccionadas.Items.Refresh();

        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtAbreviacion.Text = string.Empty;

        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            List<Competencia> comSelec = new List<Competencia>();
            competencias = col.ReadAllCompetencias();
            Area a = new Area();
            foreach (string item in lbComSeleccionadas.Items)
            {
                foreach (Competencia c in competencias)
                {
                    if (c.NOMBRE == item)
                    {
                        comSelec.Add(c);
                    }
                }
            }
            if (lbComSeleccionadas.Items.Count == 0)
            {
                MessageBox.Show("Debe seleccionar las competencias para el área");
            }
            else
            {
                try
                {
                    Area ar = new Area();
                    if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                    {
                        if (txtAbreviacion.Text.Length > 0 && txtAbreviacion.Text.Trim() != "")
                        {
                            ar.NOMBRE = txtNombre.Text;
                            ar.ABREVIACION = txtAbreviacion.Text;
                            if (rbNo.IsChecked == true)
                                ar.OBSOLETA = 0;
                            if (rbSi.IsChecked == true)
                                ar.OBSOLETA = 1;

                            //XML formato = new XML();
                            //string xml = formato.Serializar(ar);
                            //WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                            AreaOperacion aOp = new AreaOperacion(ar);
                            if (aOp.Insert(comSelec))
                            {
                                MessageBox.Show("Agregado correctamente", "Éxito!");
                                this.Limpiar();
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorArea nextPage = new MantenedorArea();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar la Área, verifique que los datos sean correctos", "Aviso");

                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo Abreviación es obligatorio", "Aviso");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo agregar la Área!", "Alerta");
                }
            }
        }

        private void rbNo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorArea nextPage = new MantenedorArea();
            navService.Navigate(nextPage);
        }
    }
}
