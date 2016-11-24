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
    /// Lógica de interacción para ModificarArea.xaml
    /// </summary>
    public partial class ModificarArea : System.Windows.Controls.Page
    {
        public ModificarArea(int id)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;

            Area ar = new Area();
            AreaOperacion arOp = new AreaOperacion(ar);
            ar.id_area = id;
            arOp.Read();

            if (ar.obsoleta == 0)
                rbNo.IsChecked = true;
            else
                rbSi.IsChecked = true;
            txtIdArea.Text = ar.id_area.ToString();
            txtNombre.Text = ar.area;
            txtAbreviacion.Text = ar.abreviacion;
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

            try
            {
                Area ar = new Area();
                ar.id_area = int.Parse(txtIdArea.Text);
                AreaOperacion arOp = new AreaOperacion(ar);
                if (arOp.Read())
                {
                    if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                    {
                        if (txtAbreviacion.Text.Length > 0 && txtAbreviacion.Text.Trim() != "")
                        {
                            ar.area = txtNombre.Text;
                            ar.abreviacion = txtAbreviacion.Text;
                            if (rbNo.IsChecked == true)
                                ar.obsoleta = 0;
                            if (rbSi.IsChecked == true)
                                ar.obsoleta = 1;

                            XML formato = new XML();
                            string xml = formato.Serializar(ar);
                            WFBS.Presentation.ServiceWFBS.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWFBS.ServiceWFBSClient();

                            if (servicio.ActualizarArea(xml))
                            {
                                MessageBox.Show("Actualizado correctamente", "Éxito!");
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorArea nextPage = new MantenedorArea();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar la Área, verifique que los datos sean correctos", "Aviso");
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
                else
                {
                    MessageBox.Show("Debe completar los campos antes de continuar", "Aviso");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo Actualizar la Área!", "Alerta");
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