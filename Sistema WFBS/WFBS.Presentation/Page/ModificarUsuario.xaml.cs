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
    /// Lógica de interacción para ModificarUsuario.xaml
    /// </summary>
    public partial class ModificarUsuario : System.Windows.Controls.Page
    {
        Collections col = new Collections();
        static Usuario us = new Usuario();
        UsuarioOperacion usOp = new UsuarioOperacion(us);

        public ModificarUsuario()
        {
            InitializeComponent();

        }

        public ModificarUsuario(string rut)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;

            us.RUT = rut;
            usOp.Read();
            if (us.SEXO[0] == 'M')
                rbMasculino.IsChecked = true;
            else
                rbFemenino.IsChecked = true;
            txtNombre.Text = us.NOMBRE;
            txtPassword.Password = us.PASSWORD;
            txtPassword2.Password = us.PASSWORD;
            txtRut.Text = us.RUT;
            if (us.OBSOLETO == 0)
                rbNoObsoleto.IsChecked = true;
            else
                rbSiObsoleto.IsChecked = true;
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {

        }

        private void cmbArea_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            int var;
            List<PerfildeCargo> areas = col.ReadAllPerfilesdeCargo();
            foreach (PerfildeCargo item in areas)
            {
                var = Convert.ToInt32(item.ID_PERFIL_DE_CARGO);
                cmbArea.Items.Add(item.DESCRIPCION);
                if (var == us.ID_PERFIL_DE_CARGO)
                    select = i;
                i++;
            }
            cmbArea.SelectedIndex = select;
        }
        private void cmbJefe_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Usuario> jefes = col.ObtenerJefes();
            foreach (Usuario item in jefes)
            {
                cmbJefe.Items.Add(item.NOMBRE);
                if (item.NOMBRE == us.JEFE_RESPECTIVO)
                    select = i;
                i++;
            }
            cmbJefe.SelectedIndex = select;
        }
        private void cmbPerfil_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Perfil> perfiles = col.ReadAllPerfiles();
            foreach (Perfil item in perfiles)
            {
                cmbPerfil.Items.Add(item.TIPO_USUARIO);
                if (item.ID_PERFIL == us.ID_PERFIL)
                    select = i;
                i++;
            }
            cmbPerfil.SelectedIndex = select;
        }
        private void cmbPerfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPerfil.SelectedIndex != 2)
            {
                lbJefe.Visibility = Visibility.Hidden;
                cmbJefe.IsEnabled = false;
                cmbJefe.Visibility = Visibility.Hidden;
            }
            else
            {
                cmbJefe.IsEnabled = true;
                cmbJefe.Visibility = Visibility.Visible;
                lbJefe.Visibility = Visibility.Visible;
            }
            if (cmbPerfil.SelectedIndex == 0)
            {
                lbArea.Visibility = Visibility.Hidden;
                cmbArea.IsEnabled = false;
                cmbArea.Visibility = Visibility.Hidden;
            }
            else
            {
                lbArea.Visibility = Visibility.Visible;
                cmbArea.IsEnabled = true;
                cmbArea.Visibility = Visibility.Visible;
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtPassword2.Password = string.Empty;
            rbMasculino.IsChecked = true;
            cmbPerfil.SelectedIndex = 2;
            rbNoObsoleto.IsChecked = true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            List<PerfildeCargo> areas = col.ReadAllPerfilesdeCargo();
            List<Perfil> perfiles = col.ReadAllPerfiles();
            try
            {
                Usuario us = new Usuario();
                us.RUT = txtRut.Text;

                XML formato = new XML();
                string xml = formato.Serializar(us);
                WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                if (servicio.LeerUsuario(xml) != null)
                {
                    if (txtNombre.Text.Length > 0 && txtRut.Text.Length > 0 && txtPassword.Password.Length > 0)
                    {
                        if (txtPassword.Password == txtPassword2.Password)
                        {
                            us.NOMBRE = txtNombre.Text;
                            us.PASSWORD = txtPassword.Password;
                            if (cmbPerfil.SelectedIndex == 2)
                                us.JEFE_RESPECTIVO = cmbJefe.SelectedItem.ToString();
                            else
                                us.JEFE_RESPECTIVO = "";
                            if (rbFemenino.IsChecked == true)
                                us.SEXO = "F";
                            if (rbMasculino.IsChecked == true)
                                us.SEXO = "M";
                            foreach (PerfildeCargo a in areas)
                            {
                                if (a.DESCRIPCION == (string)cmbArea.SelectedItem)
                                {
                                    us.ID_PERFIL_DE_CARGO = Convert.ToInt32(a.ID_PERFIL_DE_CARGO);
                                }
                            }
                            foreach (Perfil p in perfiles)
                            {
                                if (p.TIPO_USUARIO == (string)cmbPerfil.SelectedItem)
                                {
                                    us.ID_PERFIL = Convert.ToInt32(p.ID_PERFIL);
                                }
                            }
                            if (rbNoObsoleto.IsChecked == true)
                                us.OBSOLETO = 0;
                            if (rbSiObsoleto.IsChecked == true)
                                us.OBSOLETO = 1;

                            string xml2 = formato.Serializar(us);

                            if ((servicio.ActualizarUsuario(xml2)))
                            {
                                MessageBox.Show("Actualizado correctamente", "Éxito!");
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorUsuarios nextPage = new MantenedorUsuarios();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo Actualizar el Usuario, verifique que los datos sean correctos", "Aviso");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas ingresadas no coinciden", "Aviso");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los campos antes de continuar", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El Usuario no existe", "Alerta");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se ha podido actualizar el Usuario!", "Alerta");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorUsuarios nextPage = new MantenedorUsuarios();
            navService.Navigate(nextPage);
        }
    }
}
