using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1 : System.Windows.Controls.Page
    {

        Usuario us;

        public Page1()
        {
            InitializeComponent();
            
        }


        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            us = new Usuario();
            UsuarioOperacion usOp = new UsuarioOperacion(us);
            us.RUT = txtUser.Text;
            us.PASSWORD = (string)txtPass.Password;
            try
            {

                if (txtUser.Text.Length > 0 && txtPass.Password.Length > 0)
                {
                    //if (validarRut())
                    //{
                        XML formato = new XML();
                        string xml = formato.Serializar(us);
                        WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                        if (servicio.ValidarUsuario(xml))
                        {
                            usOp.Read();
                            if (servicio.Desactivado(xml))
                            {
                                Global.RutUsuario = us.RUT;
                                Global.NombreUsuario = us.NOMBRE;
                                Global.AreaInvestigacion = "Por definir";
                                Global.JefeUsuario = us.JEFE_RESPECTIVO;
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                Page2 nextPage = new Page2();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("La cuenta utilizada se encuentra Desactivada", "Alerta");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Rut o contraseña inválidos. Asegúrese de entrar con perfil de administrador al sistema.", "Error!");
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Debe ingresar un Rut valido", "Aviso");
                    //}                    
                }
                else
                {
                    MessageBox.Show("Debe ingresar su RUT y contraseña", "Alerta");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Surgieron inconvenientes al conectarse","Alerta");
            }

        }

        public bool validarRut()
        {

            try
            {
                if (string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    return false;
                }
                else
                {
                    string rut = txtUser.Text.Trim().ToUpper();
                    rut = txtUser.Text.Trim().Replace("-", "");
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