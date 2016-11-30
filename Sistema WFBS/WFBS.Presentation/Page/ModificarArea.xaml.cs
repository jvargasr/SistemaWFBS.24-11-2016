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
    /// Lógica de interacción para ModificarArea.xaml
    /// </summary>
    public partial class ModificarArea : System.Windows.Controls.Page
    {
        Collections col = new Collections();
        List<Competencia> competencias = new List<Competencia>();
        static Area ar = new Area();
        AreaOperacion arOp = new AreaOperacion(ar);
        public ModificarArea(int id)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            competencias = col.ReadAllCompetencias();

            ar.ID_AREA = id;
            arOp.Read();
            string txt = arOp.competenciasArea(ar);
            string[] comAr = new string[] { "" };
            if (txt != null)
                comAr = txt.Split(',');
            foreach (Competencia item in competencias)
            {
                if (item.OBSOLETA == 0)
                {
                    if (comAr.Contains(item.ID_COMPETENCIA.ToString()))
                    {
                        lbComSeleccionadas.Items.Add(item.NOMBRE);
                    }
                    else
                    {
                        lbCom.Items.Add(item.NOMBRE);
                    }
                }
            }

            if (ar.OBSOLETA == 0)
                rbNo.IsChecked = true;
            else
                rbSi.IsChecked = true;
            txtIdArea.Text = ar.ID_AREA.ToString();
            txtNombre.Text = ar.NOMBRE;
            txtAbreviacion.Text = ar.ABREVIACION;
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
            rbNo.IsChecked = true;

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Debe seleccionar las Competencias para el Área","Aviso");
            }
            else
            {
                try
                {
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

                            AreaOperacion aOp = new AreaOperacion(ar);
                            if (aOp.Actualize(comSelec))
                            {
                                MessageBox.Show("Agregado correctamente", "Éxito!");
                                this.Limpiar();
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorArea nextPage = new MantenedorArea();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar el Área, verifique que los datos sean correctos", "Aviso");

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
                    MessageBox.Show("No se pudo agregar el Área!", "Alerta");
                }
            }
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            /////
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorArea nextPage = new MantenedorArea();
            navService.Navigate(nextPage);
        }
    }
}
