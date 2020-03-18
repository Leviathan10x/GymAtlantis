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
    /// Lógica de interacción para CerrarTurno.xaml
    /// </summary>
    public partial class CerrarTurno : Window
    {
        public Int64 UsActivo;
        public string Nombre;
        public DateTime FechaHoy;
        
        
        public CerrarTurno(Int64 pUsActivo,string pNombre)
        {
            InitializeComponent();
            this.UsActivo = pUsActivo;
            this.Nombre = pNombre;
            labelNombre.Content = pNombre;
            labelDocumento.Content = Convert.ToString(pUsActivo);
            DateTime pFechaHoy = Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
            this.FechaHoy = pFechaHoy;
            CargarMes();
            CargarSemana();
            CargarDia();
            CargarQuincena();
            Cargar2Meses();
            Cargar3Meses();
            Cargar6Meses();
            CargarAño();
            
            cargarliquidos();
            CargarOtros();
            Totalizar();
            
        }
        public void CargarMes()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='Mensual                  ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadMensual.Content = read["CANTIDAD"].ToString();
                        labelTotalMensual.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadMensual.Content = "0";
                        labelTotalMensual.Content = "0";
                    }
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarSemana()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='Semanal                  ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadSemanal.Content = read["CANTIDAD"].ToString();
                        labelTotalSemanales.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadSemanal.Content = "0";
                        labelTotalSemanales.Content = "0";
                    }
                }
                
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CargarDia()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='Diario                   ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadDiarios.Content = read["CANTIDAD"].ToString();
                        labelTotalDiarios.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadDiarios.Content = "0";
                        labelTotalDiarios.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void fecha()
        {
            label1.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        public void CargarQuincena()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='Quincenal                ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadQuinceban.Content = read["CANTIDAD"].ToString();
                        labelTotalQuincenal.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadQuinceban.Content = "0";
                        labelTotalQuincenal.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Cargar2Meses()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='2 Meses                  ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidad2Meses.Content = read["CANTIDAD"].ToString();
                        labelTotal2Meses.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidad2Meses.Content = "0";
                        labelTotal2Meses.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Cargar3Meses()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='3 Meses                  ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidad3Meses.Content = read["CANTIDAD"].ToString();
                        labelTotal3Meses.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidad3Meses.Content = "0";
                        labelTotal3Meses.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Cargar6Meses()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='6 Meses                  ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidad6Meses.Content = read["CANTIDAD"].ToString();
                        labelTotal6Meses.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidad6Meses.Content = "0";
                        labelTotal6Meses.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarAño()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL) as TOTAL,COUNT(*) AS CANTIDAD FROM INGRESOS WHERE (ID_USUARIO=@ID AND FECHA_INGRE=@FECHA_HOY AND TIPO_INGRE='Anual                    ')";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadAños.Content = read["CANTIDAD"].ToString();
                        labelTotalAños.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadAños.Content = "0";
                        labelTotalAños.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void cargarliquidos()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL_VENTA) as TOTAL,COUNT(UNIDADES) AS CANTIDAD FROM VENTAS INNER JOIN PRODUCTOS ON PRODUCTOS.ID_PRODUCTO=Ventas.ID_PRODUCTO WHERE (ID_USUARIO=@ID AND FECHA_VENTA=@FECHA_HOY ) AND PRODUCTOS.CATEGORIA=2 AND PRODUCTOS.CATEGORIA<>4 AND PRODUCTOS.CATEGORIA<>5 AND PRODUCTOS.CATEGORIA<>6";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    if (read["CANTIDAD"] != null)
                    { 
                    int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                    if (Cantidad != 0)
                    {
                        labelCantidadBebidas.Content = read["CANTIDAD"].ToString();
                        labelTotalBebidas.Content = read["TOTAL"].ToString();
                        //Console.WriteLine("Entro");
                    }
                    else
                    {
                        labelCantidadBebidas.Content = "0";
                        labelTotalBebidas.Content = "0";
                    } 

                }
                    else
                    {
                        labelCantidadBebidas.Content = "0";
                        labelTotalBebidas.Content = "0";
                    }
                }

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarOtros()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT SUM(TOTAL_VENTA) as TOTAL,COUNT(UNIDADES) AS CANTIDAD FROM VENTAS INNER JOIN PRODUCTOS ON PRODUCTOS.ID_PRODUCTO=Ventas.ID_PRODUCTO WHERE (ID_USUARIO=@ID AND FECHA_VENTA=@FECHA_HOY ) AND PRODUCTOS.CATEGORIA>2 AND PRODUCTOS.CATEGORIA<>4 AND PRODUCTOS.CATEGORIA<>5 AND PRODUCTOS.CATEGORIA<>6";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", UsActivo);
                cmd.Parameters.AddWithValue("@FECHA_HOY", FechaHoy);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    if (read["CANTIDAD"] != null)
                    {
                        int Cantidad = Convert.ToInt32(read["CANTIDAD"].ToString());
                        if (Cantidad != 0)
                        {
                            labelCantidadOtros.Content = read["CANTIDAD"].ToString();
                            labelTotalOtros.Content = read["TOTAL"].ToString();
                            //Console.WriteLine("Entro");
                        }
                        else
                        {
                            labelTotalOtros.Content = "0";
                            labelCantidadOtros.Content = "0";
                        }
                    }
                    else
                    {
                        labelTotalOtros.Content = "0";
                        labelCantidadOtros.Content = "0";
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
            int t1, t2, t3, t4, t5, t6, t7, t8, t9, t10;
            t1 = Convert.ToInt32(labelTotalMensual.Content);
            t2 = Convert.ToInt32(labelTotalDiarios.Content);
            t3=  Convert.ToInt32(labelTotalQuincenal.Content);
            t4=  Convert.ToInt32(labelTotalSemanales.Content);
            t5= Convert.ToInt32(labelTotal2Meses.Content);
            t6= Convert.ToInt32(labelTotal3Meses.Content);
            t7= Convert.ToInt32(labelTotal6Meses.Content);
            t8= Convert.ToInt32(labelTotalAños.Content);
            t9 = Convert.ToInt32(labelTotalBebidas.Content);
            t10 = Convert.ToInt32(labelTotalOtros.Content);
            int TOTAL = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10;
            labelTotalGeneral.Content = Convert.ToString(TOTAL);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(UsActivo);
            window1.Show();
            this.Close();
        }

        private void load1(object sender, RoutedEventArgs e)
        {
            CargarInventarios CargarI = new CargarInventarios();
            CargarI.Show();
            fecha();
        }
    }
}
