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
    /// Lógica de interacción para Descuentos.xaml
    /// </summary>
    public partial class Descuentos : Window
    {
        public Descuentos()
        {
            InitializeComponent();
            comboCodigos.ItemsSource = Cargar();
        }

        private static List<string> Cargar()
        {
            try
            {
                List<string> Codigos = new List<string>();
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT RTRIM(CODIGO) AS CODIGO FROM CODIGOS_DES";

                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Codigos.Add(read["CODIGO"].ToString());
                }
                conectar.Cerrar();
                return Codigos;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void Buscar()
        {
            try
            {
                
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT RTRIM(CODIGO) AS CODIGO, VALOR, DESCRIPCION,ESTADO FROM CODIGOS_DES WHERE CODIGO=@CODIGO";

                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", comboCodigos.SelectedItem.ToString());
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    textCodigo.Text = read["CODIGO"].ToString();
                    textValor.Text = read["VALOR"].ToString();
                    textDescrip.Text = read["DESCRIPCION"].ToString();
                    comboEstado.SelectedIndex = Convert.ToInt32(read["ESTADO"].ToString());

                }
                conectar.Cerrar();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        public void Crear()
        {
        
           try
             {

               Conexion conectar = new Conexion();
               conectar.Abrir();
               string comando = "INSERT INTO CODIGOS_DES (CODIGO,VALOR,DESCRIPCION,ESTADO) VALUES(@CODIGO,@VALOR,@DESCRIP,@ESTADO)";

               SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", textCodigo.Text);
                cmd.Parameters.AddWithValue("@VALOR", Convert.ToInt32(textValor.Text));
                cmd.Parameters.AddWithValue("@DESCRIP", textDescrip.Text);
                cmd.Parameters.AddWithValue("@ESTADO", comboEstado.SelectedIndex);
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Codigo creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al crear este codigo", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                    conectar.Cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }

        private static bool Comprobar(string pcodigo)
        {
            try
            {
                bool a;
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT CODIGO FROM CODIGOS_DES WHERE CODIGO=@CODIGO";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", pcodigo);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    a = false;
                }
                else
                {
                    a = true;
                }
                conectar.Cerrar();
                return a;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void Button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (comboCodigos.SelectedIndex != -1)
            {
                Buscar();
            }
            else
            {
                MessageBox.Show("Tiene que seleccionar un codigo primero", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public void Editar()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "UPDATE CODIGOS_DES SET CODIGO=@CODIGO, VALOR=@VALOR, DESCRIPCION=@DESCRIP, ESTADO=@ESTADO WHERE CODIGO=@CODIGO";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", textCodigo.Text);
                cmd.Parameters.AddWithValue("@VALOR", Convert.ToInt32(textValor.Text));
                cmd.Parameters.AddWithValue("@DESCRIP", textDescrip.Text);
                cmd.Parameters.AddWithValue("@ESTADO", comboEstado.SelectedIndex);
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Codigo editado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al editar este codigo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ComboEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool comp = Comprobar(textCodigo.Text);
            if (comp)
            {
                Crear();
            }
            else
            {
                MessageBox.Show("El codigo ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            bool com = Comprobar(textCodigo.Text);
            if (com)
            {
                MessageBox.Show("El Codigo no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Editar();

            }
        }
    }
}
