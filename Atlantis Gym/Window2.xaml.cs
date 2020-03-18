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
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        DateTime FechaHoy;
        public Window2()
        {
            InitializeComponent();
            FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            CargarZN();
            CargarPV();
            CargarBP();
            Totalizar();
        }

        public void CargarZN()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL_VENTA) as TOTAL,COUNT(UNIDADES) AS CANTIDAD FROM VENTAS INNER JOIN PRODUCTOS ON PRODUCTOS.ID_PRODUCTO=Ventas.ID_PRODUCTO WHERE  FECHA_VENTA=@FECHA_HOY  AND PRODUCTOS.CATEGORIA=4";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                //cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        LCantZN.Content = read["CANTIDAD"].ToString();
                        lTotalZN.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        LCantZN.Content = "0";
                        lTotalZN.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarPV()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL_VENTA) as TOTAL,COUNT(UNIDADES) AS CANTIDAD FROM VENTAS INNER JOIN PRODUCTOS ON PRODUCTOS.ID_PRODUCTO=Ventas.ID_PRODUCTO WHERE  FECHA_VENTA=@FECHA_HOY  AND PRODUCTOS.CATEGORIA=5";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                //cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        LCantPV.Content = read["CANTIDAD"].ToString();
                        lTotalPV.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        LCantPV.Content = "0";
                        lTotalPV.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarBP()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL_VENTA) as TOTAL,COUNT(UNIDADES) AS CANTIDAD FROM VENTAS INNER JOIN PRODUCTOS ON PRODUCTOS.ID_PRODUCTO=Ventas.ID_PRODUCTO WHERE  FECHA_VENTA=@FECHA_HOY  AND PRODUCTOS.CATEGORIA=6";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                //cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        LCantBP.Content = read["CANTIDAD"].ToString();
                        lTotalBP.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        LCantBP.Content = "0";
                        lTotalBP.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Totalizar()
        {

            int t1, t2, t3;
            t1 = Convert.ToInt32(lTotalZN.Content);
            t2= Convert.ToInt32(lTotalPV.Content);
            t3 = Convert.ToInt32(lTotalBP.Content);
            int total = t1 + t2 + t3;
            lTotalG.Content = Convert.ToString(total);


        }

        public void fecha()
        {
            lFecha.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fecha();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
