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
    /// Lógica de interacción para ModificarCompetencia.xaml
    /// </summary>
    public partial class ModificarCompetencia : System.Windows.Controls.Page
    {
        //Collections col = new Collections();
        static Competencia com = new Competencia();
        CompetenciaOperacion comOp = new CompetenciaOperacion(com);
        public ModificarCompetencia()
        {
            InitializeComponent();
        }

        public ModificarCompetencia(int id)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;

            com.ID_COMPETENCIA = id;
            comOp.Read();

            if (com.OBSOLETA == 0)
                rbNo.IsChecked = true;
            else
                rbSi.IsChecked = true;
            txtId_Competencia.Text = com.ID_COMPETENCIA.ToString();
            txtNombre.Text = com.NOMBRE;
            txtDescripcion.Text = com.DESCRIPCION;
            txtSigla.Text = com.SIGLA;
            txtPregunta.Text = com.PREGUNTA_ASOCIADA;
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (rbNo.IsChecked == false)
            {
                lbNivel.Visibility = Visibility.Hidden;
                cmbNivel.IsEnabled = false;
                cmbNivel.Visibility = Visibility.Hidden;
            }
            else
            {
                lbNivel.Visibility = Visibility.Visible;
                cmbNivel.IsEnabled = true;
                cmbNivel.Visibility = Visibility.Visible;
            }
        }

        private void cmbNivel_Loaded(object sender, RoutedEventArgs e)
        {

            cmbNivel.Items.Add("0");
            cmbNivel.Items.Add("1");
            cmbNivel.Items.Add("2");
            cmbNivel.Items.Add("3");
            cmbNivel.Items.Add("4");
            cmbNivel.Items.Add("5");
            cmbNivel.SelectedIndex = Convert.ToInt32(com.NIVEL_OPTIMO_ESPERADO);
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtSigla.Text = string.Empty;
            cmbNivel.SelectedIndex = 0;
            rbNo.IsChecked = true;
            txtPregunta.Text = string.Empty;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            List<Competencia> competencias = comOp.Listar();
            try
            {
                Competencia com = new Competencia();
                com.ID_COMPETENCIA = int.Parse(txtId_Competencia.Text);

                XML formato = new XML();
                string xml = formato.Serializar(com);
                WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                if (servicio.LeerCompetencia(xml) != null)
                {

                    //--------------------------------------
                    if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                    {
                        if (txtDescripcion.Text.Length > 0 && txtDescripcion.Text.Trim() != "")
                        {
                            if ((txtSigla.Text.Length > 0 && txtSigla.Text.Length <= 10) && txtSigla.Text.Trim() != "")
                            {
                                com.NOMBRE = txtNombre.Text;
                                com.DESCRIPCION = txtDescripcion.Text;
                                com.SIGLA = txtSigla.Text;
                                if (rbNo.IsChecked == true)
                                    com.OBSOLETA = 0;
                                if (rbSi.IsChecked == true)
                                    com.OBSOLETA = 1;
                                #region Nivel
                                switch (cmbNivel.SelectedIndex)
                                {
                                    case 0:
                                        com.NIVEL_OPTIMO_ESPERADO = 0;
                                        break;
                                    case 1:
                                        com.NIVEL_OPTIMO_ESPERADO = 1;
                                        break;
                                    case 2:
                                        com.NIVEL_OPTIMO_ESPERADO = 2;
                                        break;
                                    case 3:
                                        com.NIVEL_OPTIMO_ESPERADO = 3;
                                        break;
                                    case 4:
                                        com.NIVEL_OPTIMO_ESPERADO = 4;
                                        break;
                                    case 5:
                                        com.NIVEL_OPTIMO_ESPERADO = 5;
                                        break;

                                    default:
                                        com.NIVEL_OPTIMO_ESPERADO = 0;
                                        break;
                                }
                                #endregion
                                com.PREGUNTA_ASOCIADA = txtPregunta.Text;
                                
                                string xml2 = formato.Serializar(com);
                                if (servicio.ActualizarCompetencia(xml2))
                                {
                                    MessageBox.Show("Actualizado correctamente", "Éxito!");
                                    NavigationService navService = NavigationService.GetNavigationService(this);
                                    MantenedorCompetencias nextPage = new MantenedorCompetencias();
                                    navService.Navigate(nextPage);
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar la Competencia, verifique que los datos sean correctos", "Aviso");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El campo Sigla es obligatorio y admite como máximo 10 caracteres", "Aviso");
                            }

                        }
                        else
                        {
                            MessageBox.Show("El campo Descripción es obligatorio", "Aviso");
                        }

                    }
                    else
                    {
                        MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar los campos antses de continuar, verifique que los datos sean correctos", "Aviso");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo actualizar la Competencia!", "Alerta");
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

