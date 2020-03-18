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
using Microsoft.VisualBasic;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        public Int64 UsActivo;
        public string Nombre;
        public Ventas(Int64 pUsActivo,string pNombre)
        {
            InitializeComponent();
            this.Nombre = pNombre;
            this.UsActivo = pUsActivo;
            labelDoc.Content = pUsActivo;
            labelNombre.Content = pNombre;
            CargarP();
        }
        
        public void CargarP()
        {
            try
            {
                List<string> Nom_Producto = new List<string>();
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT NOMBRE FROM PRODUCTOS WHERE CATEGORIA<>1 AND PACTIVO=1 AND CATEGORIA<>4 AND CATEGORIA<>5 AND CATEGORIA<>6";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Nom_Producto.Add(read["NOMBRE"].ToString());
                }
                conectar.Cerrar();
                comboProductos.ItemsSource = Nom_Producto;

                
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
        public Int32 pID;
        public void CargarDatos()
        {
            try
            {
                
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT dbo.PRODUCTOS.ID_PRODUCTO,PRECIO,DESCRIPCION,STOCK FROM PRODUCTOS,INVENTARIO WHERE dbo.PRODUCTOS.ID_PRODUCTO=dbo.INVENTARIO.ID_PRODUCTO AND NOMBRE=@NOMBRE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@NOMBRE", comboProductos.SelectedItem);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Int32 ID =Convert.ToInt32( read["ID_PRODUCTO"].ToString());
                    pID = ID;
                    labelPrecioUni.Content = read["PRECIO"];
                    labelDescripcion.Content = read["DESCRIPCION"];
                    //labelStock.Content = read["STOCK"];
                    if( read["STOCK"].ToString() == "0")
                    {
                        int Stock =Convert.ToInt32( Interaction.InputBox("Ingrese la cantidad total en stock del producto:", "STOCK"));
                        conectar.Cerrar();
                        string comando2;
                        conectar.Abrir();
                        
                        
                        comando2 = "UPDATE INVENTARIO SET STOCK=@STOCK WHERE ID_PRODUCTO=@ID";
                        
                        SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                        cmd2.Parameters.AddWithValue("@STOCK", Stock);
                        cmd2.Parameters.AddWithValue("@ID", ID);
                        int ok = cmd2.ExecuteNonQuery();
                        if (ok == 1)
                        {
                            MessageBox.Show("Stock actualizado", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar Stock", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    conectar.Cerrar();
                    conectar.Abrir();
                    string comando3 = "SELECT STOCK FROM INVENTARIO WHERE ID_PRODUCTO=@ID";
                    SqlCommand cmd3 = new SqlCommand(comando3, conectar.Conectarbd);
                    cmd3.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader read2 = cmd3.ExecuteReader();
                    if (read2.Read())
                    {
                        labelStock.Content = read2["STOCK"];
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Error al cargar el producto", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
                

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void ComboProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboProductos.SelectedIndex != -1)
            {
                CargarDatos();
                //Totalizar();
            }
            else
            {
                labelDescripcion.Content = "";
                labelPrecioUni.Content = "";
                labelStock.Content = "";
                labelTotal.Content = "";
                textCantidad.Text = "";
            }
        }

        public void Totalizar()
        {
            labelTotal.Content =(Convert.ToInt32(labelPrecioUni.Content)) * (Convert.ToInt32(textCantidad.Text));

        }

        private void TextCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textCantidad.Text != "")
                {
                    Totalizar();
                }
                else
                {
                    labelTotal.Content = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Totalizar();
            GuardaVenta();
        }
        public void GuardaVenta()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "INSERT INTO VENTAS (ID_PRODUCTO,UNIDADES,VALOR_UNI,TOTAL_VENTA,ID_USUARIO,FECHA_VENTA) VALUES(@ID,@UNIDADES,@VALOR_UNI,@TOTAL,@DOC,@FECHA)";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", pID);
                cmd.Parameters.AddWithValue("@VALOR_UNI", Convert.ToInt32(labelPrecioUni.Content));
                cmd.Parameters.AddWithValue("@UNIDADES", Convert.ToInt32(textCantidad.Text));
                cmd.Parameters.AddWithValue("@TOTAL", Convert.ToInt32(labelTotal.Content));
                cmd.Parameters.AddWithValue("@DOC", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    conectar.Cerrar();
                    conectar.Abrir();
                    string comando2 = "UPDATE INVENTARIO SET STOCK=STOCK-@CANTIDAD WHERE ID_PRODUCTO=@ID ";
                    SqlCommand cmd2 = new SqlCommand(comando2, conectar.Conectarbd);
                    cmd2.Parameters.AddWithValue("@CANTIDAD", Convert.ToInt32(textCantidad.Text));
                    cmd2.Parameters.AddWithValue("@ID", pID);
                    int ok2 = cmd2.ExecuteNonQuery();
                    if (ok2 == 1)
                    {
                        
                        MessageBox.Show("Venta completada con exito","OK",MessageBoxButton.OK,MessageBoxImage.Information);

                        comboProductos.SelectedIndex = -1;

                    }
                    else
                    {
                        MessageBox.Show("Problema al reducir del inventario", "Error inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Problema al crear la venta", "error venta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (labelStock.Content.ToString() != "")
            {
                
                int NStock = Convert.ToInt32(Interaction.InputBox("Digite la cantidad de producto para cargar al stock"));
                if (NStock > 0)
                {
                    NStock = NStock + Convert.ToInt32(labelStock.Content);
                    labelStock.Content = CargarInventario(NStock, pID);
                }
                else
                {
                    MessageBox.Show("Solo se puede incrementar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Primero debe seleccionar un producto","ERROR",MessageBoxButton.OK,MessageBoxImage.Stop);

            }
        }
        private static int CargarInventario(int Inventario,int Id)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "UPDATE INVENTARIO SET STOCK=@STOCK WHERE ID_PRODUCTO=@ID ";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@STOCK", Inventario);
                cmd.Parameters.AddWithValue("@ID", Id);
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Inventario actualizado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al cargar el inventario", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
                return Inventario;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
    }
}
