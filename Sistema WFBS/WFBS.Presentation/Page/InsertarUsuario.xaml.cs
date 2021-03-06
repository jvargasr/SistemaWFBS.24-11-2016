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
    /// <summary>holachao
    /// Lógica de interacción para InsertarUsuario.xaml
    /// </summary>
    public partial class InsertarUsuario : System.Windows.Controls.Page
    {
        Collections col = new Collections(); //<--- readllAreas
        public InsertarUsuario()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            rbMasculino.IsChecked = true;
            rbNoObsoleto.IsChecked = true;
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
        }
        private void cmbArea_Loaded(object sender, RoutedEventArgs e)
        {
            List<PerfildeCargo> areas = col.ReadAllPerfilesdeCargo();
            foreach (PerfildeCargo item in areas)
            {
                cmbArea.Items.Add(item.DESCRIPCION);
            }
            cmbArea.SelectedIndex = 0;
        }
        private void cmbJefe_Loaded(object sender, RoutedEventArgs e)
        {
            List<Usuario> jefes = col.ObtenerJefes();
            foreach (Usuario item in jefes)
            {
                cmbJefe.Items.Add(item.NOMBRE);
            }
            cmbJefe.SelectedIndex = 0;
        }
        private void cmbPerfil_Loaded(object sender, RoutedEventArgs e)
        {
            List<Perfil> perfiles = col.ReadAllPerfiles();
            foreach (Perfil item in perfiles)
            {
                cmbPerfil.Items.Add(item.TIPO_USUARIO);
            }
            cmbPerfil.SelectedIndex = 2;
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
            txtRut.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtPassword2.Password = string.Empty;
            rbMasculino.IsChecked = true;
            cmbPerfil.SelectedIndex = 2;
            rbNoObsoleto.IsChecked = true;
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            List<PerfildeCargo> PCargo = col.ReadAllPerfilesdeCargo();
            List<Perfil> perfiles = col.ReadAllPerfiles();
            try
            {
                Usuario us = new Usuario();
                us.RUT = txtRut.Text;
                UsuarioOperacion usOp = new UsuarioOperacion(us);

                XML formato = new XML();
                string xml = formato.Serializar(us);
                WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                if (servicio.LeerUsuario(xml) == null)
                {
                    if (txtNombre.Text.Length > 0 && txtRut.Text.Length > 0 && txtPassword.Password.Length > 0)
                    {
                        if (validarRut())
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

                                foreach (PerfildeCargo a in PCargo)
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
                                
                                if (servicio.CrearUsuario(xml2))
                                {
                                    MessageBox.Show("Agregado correctamente", "Éxito!");
                                    this.Limpiar();
                                    NavigationService navService = NavigationService.GetNavigationService(this);
                                    MantenedorUsuarios nextPage = new MantenedorUsuarios();
                                    navService.Navigate(nextPage);
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo agregar el Usuario, verifique que los datos sean correctos", "Aviso");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Las contraseñas no coinciden", "Aviso");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe ingresar un Rut valido", "Aviso");
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los campos antes de ingresar", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El rut ingresado ya posee un cuenta", "Aviso!");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo agregar el Usuario!", "Alerta");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorUsuarios nextPage = new MantenedorUsuarios();
            navService.Navigate(nextPage);
        }

        public bool validarRut()
        {

            try
            {
                if (string.IsNullOrEmpty(txtRut.Text.Trim()))
                {
                    return false;
                }
                else
                {
                    string rut = txtRut.Text.Trim().ToUpper();
                    rut = txtRut.Text.Trim().Replace("-", "");
                    int salida;
                    if (!int.TryParse(rut.Substring(0, rut.Length - 1), out salida))
                    {
                        return false;
                    }
                    else
                    {
                        int nrut = int.Parse(rut.Substring(0, rut.Length - 1));
                        char digitoVerfificador = char.Parse(rut.ToUpper().Substring(rut.Length - 1, 1));
                        int m = 0, s = 1;
                        for (; nrut != 0; nrut /= 10)
                        {
                            s = (s + nrut % 10 * (9 - m++ % 6)) % 11;
                        }
                        if (digitoVerfificador != (char)(s != 0 ? s + 47 : 75))
                        {
                            return false;
                        }
                        else
                        {
                            return true;

                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
