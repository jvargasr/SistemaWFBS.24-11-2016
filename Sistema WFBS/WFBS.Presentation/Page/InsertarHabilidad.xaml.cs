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
    /// Lógica de interacción para InsertarHabilidad.xaml
    /// </summary>
    public partial class InsertarHabilidad : System.Windows.Controls.Page
    {
        Habilidad hab = new Habilidad();
        int id_comp;
        public InsertarHabilidad(int id_com)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;

            id_comp = id_com;

        }

        private void cmbId_Competencia_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            Competencia com = new Competencia();
            CompetenciaOperacion comOp = new CompetenciaOperacion(com);
            List<Competencia> competencias = comOp.Listar();
            foreach (Competencia item in competencias)
            {
                if (item.ID_COMPETENCIA == id_comp)
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

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            Competencia com = new Competencia();
            CompetenciaOperacion comOp = new CompetenciaOperacion(com);
            List<Competencia> competencias = comOp.Listar();
            try
            {
                Habilidad hab = new Habilidad();
                if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                {
                    foreach (Competencia c in competencias)
                    {
                        if (c.NOMBRE == (string)cmbId_Competencia.SelectedItem)
                        {
                            hab.ID_COMPETENCIA = Convert.ToInt32(c.ID_COMPETENCIA);
                            id_comp = Convert.ToInt32(c.ID_COMPETENCIA);
                        }
                    }
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

                    XML formato = new XML();
                    string xml = formato.Serializar(hab);
                    WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                    if (servicio.CrearHabilidad(xml))
                    {
                        MessageBox.Show("Habilidad agregada correctamente", "Éxito!");
                        this.Limpiar();
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorHabilidades nextPage = new MantenedorHabilidades(Convert.ToInt32(hab.ID_COMPETENCIA));
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la Habilidad, verifique que los datos sean correctos", "Aviso");

                    }
                }
                else
                {
                    MessageBox.Show("El campo Descripción es obligatorio", "Aviso");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo agregar la Habilidad!", "Alerta");
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
