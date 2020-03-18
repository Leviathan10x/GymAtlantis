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
    /// Lógica de interacción para CrearUsuarios.xaml
    /// </summary>
    public partial class CrearUsuarios : Window
    {
        public bool Pass = false;
        public string contraseña = "";
        public CrearUsuarios()
        {
            InitializeComponent();
            
        }


        public void CrearUs()
        {
            try
            {

                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM USUARIOS WHERE ID_USUARIO=@DOC";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt64(textNroDoc.Text));
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    MessageBox.Show("Este usuario ya existe", "ERROR ID", MessageBoxButton.OK, MessageBoxImage.Stop);
                    textNroDoc.Text = "";
                }
                else
                {
                    conectar.Cerrar();
                    string comando2 = "INSERT INTO USUARIOS(ID_USUARIO,NOMBRE,APELLIDO,DOC_USUARIO,TELEFONO,DIRECCION,TIPO_USUARIO,CONTRASEÑA) VALUES(@DOC,@NOMBRE,@APELLIDO,@DOC,@TELEFONO,@DIRECCION,@TIPO_USUARIO,@CONTRASEÑA)";
                    conectar.Abrir();

                    SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                    cmd2.Parameters.AddWithValue("@DOC", Convert.ToInt64(textNroDoc.Text));
                    cmd2.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                    cmd2.Parameters.AddWithValue("@APELLIDO", textApellido.Text);
                    cmd2.Parameters.AddWithValue("@TELEFONO", textTelefono.Text);
                    cmd2.Parameters.AddWithValue("@DIRECCION", textDireccion.Text);
                    cmd2.Parameters.AddWithValue("@TIPO_USUARIO", comboTipo_Usuario.Text);
                    
                    if(passwordNueva.Password!=textNroDoc.Text & passwordNueva.Password == passwordRepetir.Password)
                    {
                        Pass = true;
                    }
                    if(passwordNueva.Password != textNroDoc.Text & passwordNueva.Password != passwordRepetir.Password)
                    {
                        MessageBox.Show("Las contraseñas no coinciden", "Error de Contraseña", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    if (Pass)
                    {
                        contraseña = passwordNueva.Password;
                        cmd2.Parameters.AddWithValue("@CONTRASEÑA", contraseña);
                        
                        int ok = cmd2.ExecuteNonQuery();
                        if (ok == 1)
                        {
                            MessageBox.Show("Usuario creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al crear el usuario", "ERORR", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                    }
                    else
                    {
                        
                        MessageBoxResult op = MessageBox.Show("No ha creado una contraseña \n¿Desea crear una?", "Crear contraseña", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                        if (op == MessageBoxResult.Yes)
                        {
                            passwordNueva.Password = "";
                            passwordRepetir.Password = "";

                        }
                        else
                        {
                            if (op==MessageBoxResult.No)
                            {
                                MessageBox.Show("Se tomara la contraseña por defecto\nRecuerde cambiar la contraseña al iniciar", "Crear contraseña por defecto", MessageBoxButton.OK, MessageBoxImage.Information);

                                contraseña = textNroDoc.Text;
                                cmd2.Parameters.AddWithValue("@CONTRASEÑA", contraseña);
                                int ok = cmd2.ExecuteNonQuery();
                                if (ok == 1)
                                {
                                    MessageBox.Show("Usuario creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Error al crear el usuario", "ERORR", MessageBoxButton.OK, MessageBoxImage.Stop);
                                }
                            }
                        }
                        
                        
                    }
                }
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CrearUs();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextNroDoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordNueva.Password = textNroDoc.Text;
            passwordRepetir.Password = textNroDoc.Text;
        }
    }
}
