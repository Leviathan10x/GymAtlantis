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
    /// Lógica de interacción para EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        public string Idselec;
        public EditarUsuario(string pIdselec)
        {
            InitializeComponent();
            this.Idselec = pIdselec;
            textDoc.Text = pIdselec;
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            BuscarUsuario buscarUsuario = new BuscarUsuario();
            buscarUsuario.Show();
            this.Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (textDoc.Text != "")
            {
                Llenar();
            }
            else
            {
                MessageBox.Show("No se ha agregado ningun numero de documento", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        public void Llenar()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT NOMBRE,APELLIDO,TELEFONO,DIRECCION,RTRIM(CONTRASEÑA) AS CONTRASEÑA,TIPO_USUARIO FROM USUARIOS WHERE ID_USUARIO=@DOC";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt64(textDoc.Text));
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    textNombre.Text = read["NOMBRE"].ToString();
                    textApellido.Text = read["APELLIDO"].ToString();
                    textTelefono.Text = read["TELEFONO"].ToString();
                    textDireccion.Text = read["DIRECCION"].ToString();
                    passwordBox1.Password = read["CONTRASEÑA"].ToString();
                    passwordBoxRepetir.Password= read["CONTRASEÑA"].ToString();
                    if((read["TIPO_USUARIO"].ToString())== "ADMINISTRADOR            ")
                    {
                        comboTipoUsuario.SelectedIndex = 0;
                    }
                    else
                    {
                        comboTipoUsuario.SelectedIndex = 1;
                    }
                }
                else
                {
                    MessageBox.Show("Error al buscar el usuario", "ERRIOR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox1.Password == passwordBoxRepetir.Password)
            {
                Editar();
            }
            else
            {
                MessageBox.Show("Las contraseñas no son iguales", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Editar()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "UPDATE USUARIOS SET NOMBRE=@NOMBRE, APELLIDO=@APELLIDO, TELEFONO=@TELEFONO, DIRECCION=@DIRECCION, TIPO_USUARIO=@TIPO_USUARIO, CONTRASEÑA=@CONTRASEÑA WHERE ID_USUARIO=@DOC ";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                cmd.Parameters.AddWithValue("@APELLIDO", textApellido.Text);
                cmd.Parameters.AddWithValue("@TELEFONO", textTelefono.Text);
                cmd.Parameters.AddWithValue("@DIRECCION", textDireccion.Text);
                cmd.Parameters.AddWithValue("@CONTRASEÑA", passwordBox1.Password);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO", comboTipoUsuario.Text);
                cmd.Parameters.AddWithValue("@DOC", textDoc.Text);
                int ok = cmd.ExecuteNonQuery();
                if(ok == 1)
                {
                    MessageBox.Show("Usuario editado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al editar el usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
