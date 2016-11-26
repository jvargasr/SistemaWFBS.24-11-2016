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
    /// Lógica de interacción para ModificarHabilidad.xaml
    /// </summary>
    public partial class ModificarHabilidad : System.Windows.Controls.Page
    {
        static Habilidad hab = new Habilidad();
        HabilidadOperacion habOp = new HabilidadOperacion(hab);
        int id_comp;
        public ModificarHabilidad()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        public ModificarHabilidad(int id, int id_com)
        {
            InitializeComponent();
            hab.ID_HABILIDAD = id;
            habOp.Read();
            id_comp = id_com;

            txtId_Habilidad.Text = hab.ID_HABILIDAD.ToString();
            /* cmbId_Competencia.SelectedIndex = hab.ID_COMPETENCIA;*/
            txtNombre.Text = hab.NOMBRE;
            txtAlternativa.Text = hab.ALTERNATIVA_PREGUNTA;
        }

        private void cmbId_Competencia_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            Competencia com = new Competencia();
            CompetenciaOperacion comOp = new CompetenciaOperacion(com);
            List<Competencia> competencias = comOp.Listar();
            foreach (Competencia item in competencias)
            {
                if (item.ID_COMPETENCIA == hab.ID_COMPETENCIA)
                {
                    cmbId_Competencia.Items.Add(item.NOMBRE);
                    select = i;
                    i++;
                }
            }
            cmbId_Competencia.SelectedIndex = select;
            cmbId_Competencia.IsEnabled = false;

        }

        private void cmbOrden_Loaded(object sender, RoutedEventArgs e)
        {
            cmbOrden.Items.Add("0");
            cmbOrden.Items.Add("1");
            cmbOrden.Items.Add("2");
            cmbOrden.Items.Add("3");
            cmbOrden.Items.Add("4");
            cmbOrden.Items.Add("5");
            cmbOrden.SelectedIndex = Convert.ToInt32(hab.ORDEN_ASIGNADO);
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            cmbId_Competencia.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            cmbOrden.SelectedIndex = 0;
            txtAlternativa.Text = string.Empty;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Habilidad hab = new Habilidad();
                HabilidadOperacion habOp = new HabilidadOperacion(hab);
                hab.ID_HABILIDAD = int.Parse(txtId_Habilidad.Text);

                XML formato = new XML();
                string xml = formato.Serializar(hab);
                WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                if (servicio.LeerHabilidad(xml) != null)
                {
                    if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                    {

                        hab.NOMBRE = txtNombre.Text;
                        #region Nivel
                        switch (cmbOrden.SelectedIndex)
                        {
                            case 0:
                                hab.ORDEN_ASIGNADO = 0;
                                break;
                            case 1:
                                hab.ORDEN_ASIGNADO = 1;
                                break;
                            case 2:
                                hab.ORDEN_ASIGNADO = 2;
                                break;
                            case 3:
                                hab.ORDEN_ASIGNADO = 3;
                                break;
                            case 4:
                                hab.ORDEN_ASIGNADO = 4;
                                break;
                            case 5:
                                hab.ORDEN_ASIGNADO = 5;
                                break;

                            default:
                                hab.ORDEN_ASIGNADO = 0;
                                break;
                        }
                        #endregion Nivel
                        hab.ALTERNATIVA_PREGUNTA = txtAlternativa.Text;
                        hab.ID_COMPETENCIA = id_comp;

                        string xml2 = formato.Serializar(hab);

                        if (servicio.ActualizarHabilidad(xml2))
                        {
                            MessageBox.Show("Actualizado correctamente", "Éxito!");
                            NavigationService navService = NavigationService.GetNavigationService(this);
                            MantenedorHabilidades nextPage = new MantenedorHabilidades(id_comp);
                            navService.Navigate(nextPage);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar la Habilidad de Cargo, verifique que los datos sean correctos", "Aviso");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar los campos antes de continuar, verifique que los datos sean correctos", "Aviso");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("No se ha podido actualizar la Habilidad!", "Alerta");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorHabilidades nextPage = new MantenedorHabilidades(id_comp);
            navService.Navigate(nextPage);
        }
    }
}

