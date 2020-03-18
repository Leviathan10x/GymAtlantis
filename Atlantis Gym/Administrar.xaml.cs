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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Administrar.xaml
    /// </summary>
    public partial class Administrar : Window
    {
        public Administrar()
        {
            InitializeComponent();
            Boton();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrearUsuarios crearUsuarios = new CrearUsuarios();
            crearUsuarios.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Reportes reportes = new Reportes();
            reportes.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Productos productos = new Productos();
            productos.Show();
        }
        public void Boton()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM CargarRegistros Where ID=1";
                int valor=0;
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    valor =Convert.ToInt32(read["VALOR"].ToString());
                }
                conectar.Cerrar();
                if (valor == 1)
                {
                    checkBoxCD.IsChecked = true;
                }
                else
                {
                    checkBoxCD.IsChecked = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CambiarBoton()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando="UPDATE CargarRegistros SET VALOR=@VALOR WHERE ID=1";
                conectar.Abrir();
                int valor;
                if (checkBoxCD.IsChecked == true)
                {
                    
                    valor = 1;
                }
                else
                {
                    
                    valor = 0;
                }
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@VALOR", valor);
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Proceso exitoso", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CheckBoxCD_Checked(object sender, RoutedEventArgs e)
        {
            CambiarBoton();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            EditarUsuario editarUsuario = new EditarUsuario("");
            editarUsuario.Show();
            this.Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Descuentos descuentos = new Descuentos();
            descuentos.Show();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            SuperEdicion superEdicion = new SuperEdicion(0,0,"",0,0,DateTime.Now,DateTime.Now);
            superEdicion.Show();
        }

        private void ConfiguracionSMS_Click(object sender, RoutedEventArgs e)
        {
            SMS sMS = new SMS();
            sMS.Show();
        }
    }
}
