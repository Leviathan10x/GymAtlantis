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
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : Window
    {
        public Categorias()
        {
            InitializeComponent();
            Id_categoria();
        }
        public void Id_categoria()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT ID_CATEGORIA FROM CATEGORIAS_ART WHERE ID_CATEGORIA=(SELECT MAX(ID_CATEGORIA)FROM CATEGORIAS_ART)";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int id =Convert.ToInt32(read["ID_CATEGORIA"].ToString());
                    int ID = id + 1;
                    textId.Text = Convert.ToString(ID);
                }
                else
                {
                    textId.Text = "1";
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool ok = false;
        public void comparacion()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT ID_CATEGORIA FROM CATEGORIAS_ART WHERE ID_CATEGORIA=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    MessageBox.Show("El Id ya existe para otro producto", "ERROR ID", MessageBoxButton.OK, MessageBoxImage.Stop);
                    ok = false;
                }
                else
                {
                    ok = true;
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CrearCategoria()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "INSERT INTO CATEGORIAS_ART (ID_CATEGORIA,NOMBRE,DESCRIPCION) VALUES(@ID,@NOMBRE,@DESCRIPCION)";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                cmd.Parameters.AddWithValue("@DESCRIPCION", textDescripcion.Text);
                int b = cmd.ExecuteNonQuery();
                if (b == 1)
                {
                    MessageBox.Show("Categoria creada con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    textId.Text = "";
                    textNombre.Text = "";
                    textDescripcion.Text = "";
                }
                else
                {
                    MessageBox.Show("Error al crear la categoria", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    textId.Text = "";

                }
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            comparacion();
            if (ok)
            {
                CrearCategoria();
                Id_categoria();
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Productos productos = new Productos();
            productos.Show();
            this.Close();
        }
    }
}
