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
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {

        int pactivo;
        public Productos()
        {
            InitializeComponent();
            CargarCategorias();
            
            labelStock.Visibility = Visibility.Collapsed;
            textStock.Visibility = Visibility.Collapsed;
            Id_Producto();
            checkBox.Content = "Ativar Producto";
        }
        public int ArticuloSelec;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Categorias categorias = new Categorias();
            categorias.Show();
            this.Close();
        }

        public void checkin()
        {
            if (checkBox.IsChecked == true)
            {
                pactivo = 1;
            }
            else
            {
                pactivo = 0;
            }
            

        }
        public void checkout()
        {
            if (pactivo == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void CargarCategorias()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT ID_CATEGORIA FROM CATEGORIAS_ART";
                List<Int32> ID = new List<int>();
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ID.Add(Convert.ToInt32(read["ID_CATEGORIA"].ToString()));
                }
                conectar.Cerrar();
                comboIdCat.ItemsSource = ID;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void NombreCat()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE FROM CATEGORIAS_ART WHERE ID_CATEGORIA=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", comboIdCat.SelectedItem);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    labelNombreCat.Content = read["NOMBRE"].ToString();
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ComboIdCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NombreCat();
        }

        public void Id_Producto()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT ID_PRODUCTO FROM PRODUCTOS WHERE ID_PRODUCTO=(SELECT MAX(ID_PRODUCTO)FROM PRODUCTOS)";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int id = Convert.ToInt32(read["ID_PRODUCTO"].ToString());
                    int ID = id + 1;
                    textId.Text = Convert.ToString(ID);
                }
                else
                {
                    textId.Text = "1";
                }
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CrearProducto()
        {
            try
            {
                checkin();
                Conexion conectar = new Conexion();
                string comando = "SELECT ID_PRODUCTO FROM PRODUCTOS WHERE ID_PRODUCTO=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    MessageBox.Show("El Id de producto ya existe", "ERROR ID", MessageBoxButton.OK, MessageBoxImage.Error);
                    Id_Producto();
                }
                else
                {
                    string comando2 = "INSERT INTO PRODUCTOS(ID_PRODUCTO,NOMBRE,PRECIO,DESCRIPCION,CATEGORIA,PACTIVO) VALUES(@ID,@NOMBRE,@PRECIO,@DESCRIPCION,@CATEGORIA,@PACTIVO)";
                    conectar.Cerrar();
                    conectar.Abrir();
                    SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                    cmd2.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                    cmd2.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                    cmd2.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(textPrecio.Text));
                    cmd2.Parameters.AddWithValue("@DESCRIPCION", textDescripcion.Text);
                    cmd2.Parameters.AddWithValue("@CATEGORIA", comboIdCat.SelectedItem);
                    cmd2.Parameters.AddWithValue("@PACTIVO", pactivo);
                    int ok = cmd2.ExecuteNonQuery();
                    if (ok == 1)
                    {
                        MessageBox.Show("Producto creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                        textDescripcion.Text = "";
                        textNombre.Text = "";
                        //textId.Text = "";
                        textPrecio.Text = "";
                        if (textStock.Text != "")
                        {
                            conectar.Cerrar();
                            conectar.Abrir();
                            string comando3 = "INSERT INTO INVENTARIO (ID_PRODUCTO,STOCK) VALUES(@ID,@STOCK)";
                            SqlCommand cmd3 = new SqlCommand(comando3, conectar.Conectarbd);
                            cmd3.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                            cmd3.Parameters.AddWithValue("@STOCK", Convert.ToInt32(textStock.Text));
                            int ok2 = cmd3.ExecuteNonQuery();
                            if (ok2 == 1)
                            {
                                MessageBox.Show("Inventario cargado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else
                            {
                                MessageBox.Show("El inventario no pudo ser creado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            conectar.Cerrar();
                            conectar.Abrir();
                            string comando3 = "INSERT INTO INVENTARIO (ID_PRODUCTO,STOCK) VALUES(@ID,@STOCK)";
                            SqlCommand cmd3 = new SqlCommand(comando3, conectar.Conectarbd);
                            cmd3.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                            cmd3.Parameters.AddWithValue("@STOCK", 0);
                            int ok2 = cmd3.ExecuteNonQuery();
                            if (ok2 == 1)
                            {
                                MessageBox.Show("Inventario cargado en cero", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else
                            {
                                MessageBox.Show("El inventario no pudo ser creado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al crear el producto", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                conectar.Cerrar();
                textId.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CrearProducto();
            Id_Producto();
        }

        public void Buscar()
        {
            try
            {

                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM PRODUCTOS WHERE ID_PRODUCTO=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    textNombre.Text = read["NOMBRE"].ToString();
                    textDescripcion.Text = read["DESCRIPCION"].ToString();
                    textPrecio.Text = read["PRECIO"].ToString();
                    int i = Convert.ToInt32(read["CATEGORIA"].ToString());
                    pactivo = Convert.ToInt32(read["PACTIVO"].ToString());
                    comboIdCat.SelectedItem = i ;

                    string comando2 = "SELECT STOCK FROM INVENTARIO WHERE ID_PRODUCTO=@ID_P";
                    conectar.Cerrar();
                    conectar.Abrir();
                    SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                    cmd2.Parameters.AddWithValue("@ID_P", Convert.ToInt32(textId.Text));
                    SqlDataReader read2 = cmd2.ExecuteReader();
                    if (read2.Read())
                    {
                        textStock.Text = read2["STOCK"].ToString();
                    }
                    else
                    {
                        textStock.Text = "";
                    }
                    
                }
                else
                {
                    MessageBox.Show("Este producto no existe", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    textId.Text = "";
                }
                conectar.Cerrar();
                checkout();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }
        public void EditarP()
        {
            try
            {
                checkin();
                Conexion conectar = new Conexion();
                string comando = "UPDATE PRODUCTOS SET NOMBRE=@NOMBRE, DESCRIPCION=@DESCRIPCION, PRECIO=@PRECIO, PACTIVO=@PACTIVO WHERE ID_PRODUCTO=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                cmd.Parameters.AddWithValue("@DESCRIPCION", textDescripcion.Text);
                cmd.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(textPrecio.Text));
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@PACTIVO", pactivo);
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Producto editado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (textStock.Text != "")
                    {
                        conectar.Cerrar();
                        conectar.Abrir();
                        string comando2 = "SELECT * FROM INVENTARIO WHERE ID_PRODUCTO=@ID";
                        SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                        cmd2.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                        string comando3;
                        SqlDataReader read = cmd2.ExecuteReader();
                        if (read.Read())
                        {
                            comando3 = "UPDATE INVENTARIO SET STOCK=@STOCK WHERE ID_PRODUCTO=@ID";
                        }
                        else
                        {
                            comando3 = "INSERT INTO INVENTARIO (ID_PRODUCTO,STOCK) VALUES(@ID,@STOCK)";
                        }
                        conectar.Cerrar();
                        conectar.Abrir();
                        SqlCommand cmd3 = new SqlCommand(comando3, conectar.Conectarbd);
                        cmd3.Parameters.AddWithValue("@ID", Convert.ToInt32(textId.Text));
                        cmd3.Parameters.AddWithValue("@STOCK", Convert.ToInt32(textStock.Text));
                        int ok3 = cmd3.ExecuteNonQuery();
                        if (ok3 == 1)
                        {
                            MessageBox.Show("Inventario Actualizado", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar inventario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Error al editar producto", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                conectar.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            EditarP();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextId_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textId.Text != "")
                {
                    if ((Convert.ToInt64(textId.Text)) > 8)
                    {
                        labelStock.Visibility = Visibility.Visible;
                        textStock.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        labelStock.Visibility = Visibility.Collapsed;
                        textStock.Visibility = Visibility.Collapsed;

                    }
                }
                else
                {
                    labelStock.Visibility = Visibility.Collapsed;
                    textStock.Visibility = Visibility.Collapsed;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
