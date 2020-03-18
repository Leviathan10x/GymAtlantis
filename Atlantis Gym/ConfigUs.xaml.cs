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
    /// Lógica de interacción para ConfigUs.xaml
    /// </summary>
    public partial class ConfigUs : Window
    {
        public Int64 pUsActivo;
        public ConfigUs(Int64 UsActivo)
        {
            InitializeComponent();
            pUsActivo = UsActivo;
            Inicio();
        }
        public void Inicio()
        {
            try
            {

                string Nombre = "Nombre";
                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM USUARIOS WHERE ID_USUARIO=@DOC";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", pUsActivo);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Nombre = read["NOMBRE"].ToString();
                }
                conectar.Cerrar();

                textNombre.Text = Nombre;
                textDoc.Text = pUsActivo.ToString();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CambiarContraseña()
        {
            try
            {
                string comando = "SELECT * FROM USUARIOS WHERE ID_USUARIO=@DOC AND CONTRASEÑA=@CONTRASEÑA";
                Conexion conectar = new Conexion();
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", pUsActivo);
                cmd.Parameters.AddWithValue("@CONTRASEÑA", passwordActual.Password.ToString());
                if (passwordActual.Password != passwordNuevo.Password)
                {
                    if (passwordNuevo.Password == passwordRepetir.Password)
                    {
                        SqlDataReader read = cmd.ExecuteReader();
                        if (read.Read())
                        {
                            conectar.Cerrar();
                            string comando2 = "UPDATE USUARIOS SET CONTRASEÑA=@NCONTRASEÑA WHERE ID_USUARIO=@DOC";
                            conectar.Abrir();
                            SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                            cmd2.Parameters.AddWithValue("@NCONTRASEÑA", passwordNuevo.Password.ToString());
                            cmd2.Parameters.AddWithValue("@DOC", pUsActivo);
                            int ok = cmd2.ExecuteNonQuery();
                            if (ok == 1)
                            {
                                MessageBox.Show("Cambio de contraseña exitoso", "Actualizacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                                
                            }
                            else
                            {
                                MessageBox.Show("No se pudo Actualizar la contraseña", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("La Contraseña actual ingresada es Erronea", "Error de Login", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las nuevas contraseñas no coinciden", "Error de Coincidencia", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña nueva tiene que ser diferente a la actual", "Contraseñas iguales", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                passwordActual.Password = "";
                passwordNuevo.Password = "";
                passwordRepetir.Password = "";
                conectar.Cerrar();
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CambiarContraseña();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
