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
    /// Lógica de interacción para CargarDatos.xaml
    /// </summary>
    public partial class CargarDatos : Window
    {
        public Int64 UsActivo;
        public CargarDatos(Int64 pUsActivo)
        {
            InitializeComponent();
            this.UsActivo = pUsActivo;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Guardar()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM CLIENTES WHERE ID_CLIENTE=@DOC";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt64(textDoc.Text));
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    MessageBox.Show("El usuario ya existe", "ERROR ID", MessageBoxButton.OK, MessageBoxImage.Error);
                    textDoc.Text = "";
                }
                else
                {
                    conectar.Cerrar();
                    conectar.Abrir();
                    string comando2 = "INSERT INTO CLIENTES (ID_CLIENTE,DOC_CLIENTE,NOMBRE,APELLIDO,TELEFONO,DIRECCION,FECHA_INGRESO,GENERO,ESTATURA_IN) VALUES(@DOC,@TIPO_DOC,@NOMBRE,@APELLIDO,@TELEFONO,@DIRECCION,@FECHA_INGRE,@GENERO,@ESTATURA)";
                    SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                    cmd2.Parameters.AddWithValue("@DOC", Convert.ToInt64(textDoc.Text));
                    cmd2.Parameters.AddWithValue("@TIPO_DOC", comboTipoDoc.Text);
                    cmd2.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                    cmd2.Parameters.AddWithValue("@APELLIDO", textApellido.Text);
                    cmd2.Parameters.AddWithValue("@TELEFONO", textTelefono.Text);
                    cmd2.Parameters.AddWithValue("@DIRECCION", textDireccion.Text);
                    cmd2.Parameters.AddWithValue("@FECHA_INGRE",Convert.ToDateTime(DateFechaUltimoPago.Text));
                    cmd2.Parameters.AddWithValue("@GENERO", comboGenero.Text);
                    cmd2.Parameters.AddWithValue("@ESTATURA", Convert.ToInt32(textEstatura.Text));

                    int ok = cmd2.ExecuteNonQuery();
                    if (ok == 1)
                    {
                        MessageBox.Show("Cliente creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                        //textDoc.Text = "";
                        textDireccion.Text = "";
                        textNombre.Text = "";
                        textTelefono.Text = "";
                        textApellido.Text = "";
                        textEstatura.Text = "";
                        comboTipoDoc.SelectedItem = -1;
                        comboGenero.SelectedItem = -1;
                        conectar.Cerrar();
                        conectar.Abrir();
                        int total = 0;
                        string comando3 = "INSERT INTO INGRESOS (DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL,ID_USUARIO,FECHA_INICIO) VALUES(@DOC,@TIPO_INGRE,@FECHA_INGRE,@TOTAL,@US_ACTIVO,@FECHA_INGRE)";
                        SqlCommand cmd3 = new SqlCommand(comando3, conectar.Conectarbd);
                        cmd3.Parameters.AddWithValue("@DOC", Convert.ToInt64(textDoc.Text));
                        cmd3.Parameters.AddWithValue("@TIPO_INGRE", comboTipoPago.Text);
                        cmd3.Parameters.AddWithValue("@FECHA_INGRE", Convert.ToDateTime(DateFechaUltimoPago.Text));
                        cmd3.Parameters.AddWithValue("@TOTAL", total);
                        cmd3.Parameters.AddWithValue("@US_ACTIVO", UsActivo);

                        int ok2 = cmd3.ExecuteNonQuery();
                        if (ok2 == 1)
                        {
                            MessageBox.Show("Ingreso creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                            textDoc.Text = "";
                            comboTipoPago.SelectedIndex = -1;

                        }
                        else
                        {
                            MessageBox.Show("No se pudo ingresar el nuevo cliente", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo ingresar el nuevo cliente", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    conectar.Cerrar();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
