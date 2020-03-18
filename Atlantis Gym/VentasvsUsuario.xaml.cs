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
    /// Lógica de interacción para VentasvsUsuario.xaml
    /// </summary>
    public partial class VentasvsUsuario : Window
    {
        public Int32 Nombre;
        public VentasvsUsuario()
        {
            InitializeComponent();
            Buscar();
            
        }

        public void Buscar()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE,ID_USUARIO FROM USUARIOS ";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                List<Int32> DOC = new List<Int32>();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    DOC.Add(Convert.ToInt32(read["ID_USUARIO"].ToString()));
                }
                comboUs.ItemsSource = DOC;
                conectar.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ComboUs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selnombre();
        }
        public void selnombre()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE FROM USUARIOS WHERE ID_USUARIO=@DOC ";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt32(comboUs.SelectedItem.ToString()));
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    labelNombre.Content = read["NOMBRE"].ToString();
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static List<Ingresos> Consulta(string pFechaIni, string pFechaFin,Int32 pID_Usuario)
        {
            try
            {
                List<Ingresos> Ventas = new List<Ingresos>();
                Conexion conectar = new Conexion();
                string comando = "SELECT DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL FROM INGRESOS WHERE FECHA_INGRE>=@FECHA_INI AND FECHA_INGRE<=@FECHA_FIN AND ID_USUARIO=@DOC";
                conectar.Abrir();

                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@FECHA_INI", Convert.ToDateTime(pFechaIni));
                cmd.Parameters.AddWithValue("@FECHA_FIN", Convert.ToDateTime(pFechaFin));
                cmd.Parameters.AddWithValue("@DOC", pID_Usuario);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Ingresos pingresos = new Ingresos();
                    pingresos.DOC_CLIENTE = Convert.ToInt32(read["DOC_CLIENTE"].ToString());
                    pingresos.TIPO_INGRE = (read["TIPO_INGRE"].ToString());
                    pingresos.FECHA_INGRE = Convert.ToDateTime(read["FECHA_INGRE"].ToString());
                    pingresos.TOTAL = (Convert.ToInt32(read["TOTAL"].ToString()));
                    Console.WriteLine(read["DOC_CLIENTE"].ToString());
                    Ventas.Add(pingresos);
                }

                conectar.Cerrar();
                return Ventas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource= Consulta(FechaIni.DisplayDate.Date.ToString("yyyy-MM-dd"), FechaFin.DisplayDate.Date.ToString("yyyy-MM-dd"),Convert.ToInt32(comboUs.SelectedItem.ToString()));
        }
    }
}
