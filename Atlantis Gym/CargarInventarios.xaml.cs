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
    /// Lógica de interacción para CargarInventarios.xaml
    /// </summary>
    public partial class CargarInventarios : Window
    {
        public CargarInventarios()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CargarP();
        }

        private static List<ProductosI> CargarP()
        {
            try
            {
                List<ProductosI> Productos = new List<ProductosI>();
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT NOMBRE, STOCK FROM PRODUCTOS INNER JOIN INVENTARIO ON PRODUCTOS.ID_PRODUCTO=INVENTARIO.ID_PRODUCTO WHERE PRODUCTOS.PACTIVO=1 AND PRODUCTOS.CATEGORIA<>4 AND PRODUCTOS.CATEGORIA<>5 AND PRODUCTOS.CATEGORIA<>6";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ProductosI pproductos = new ProductosI();
                    pproductos.Nombre = read["NOMBRE"].ToString();
                    pproductos.Stock =Convert.ToInt32( read["STOCK"].ToString());
                    Productos.Add(pproductos);
                }
                conectar.Cerrar();
                return Productos;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

    }
}
