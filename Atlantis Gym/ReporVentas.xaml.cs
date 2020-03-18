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
using System.Windows.Forms;


namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para ReporVentas.xaml
    /// </summary>
    public partial class ReporVentas : Window
    {
        public ReporVentas()
        {
            InitializeComponent();
            
            //Console.WriteLine(Fecha_fin.DisplayDate.Date.ToString("yyyy-MM-dd"));
        }
        public string TOTAL;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DataGridView Tabla=new DataGridView();
            //Tabla.DataSource =Consulta(Fecha_ini.Text, Fecha_fin.Text);
            Totalizar();
            ReporteVentas2 reporteVentas2 = new ReporteVentas2(TOTAL, Fecha_ini.Text,Fecha_fin.Text);
            reporteVentas2.Show();

        }

        public static List<Ingresos> Consulta(string pFechaIni,string pFechaFin)
        {
            try
            {
                List<Ingresos> Ventas = new List<Ingresos>();
                Conexion conectar = new Conexion();
                string comando = "SELECT DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL FROM INGRESOS WHERE FECHA_INGRE>=@FECHA_INI AND FECHA_INGRE<=@FECHA_FIN";
                conectar.Abrir();
                
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                DateTime fini=Convert.ToDateTime(pFechaIni);
                DateTime ffin=Convert.ToDateTime(pFechaFin);
                cmd.Parameters.AddWithValue("@FECHA_INI",Convert.ToDateTime(fini.Date.ToString("yyyy-MM-dd") ));
                cmd.Parameters.AddWithValue("@FECHA_FIN",Convert.ToDateTime(ffin.Date.ToString("yyyy-MM-dd")));
                Console.WriteLine(pFechaIni);
                Console.WriteLine(pFechaFin);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Ingresos pingresos = new Ingresos();
                    pingresos.DOC_CLIENTE=Convert.ToInt32(read["DOC_CLIENTE"].ToString());
                    pingresos.TIPO_INGRE=(read["TIPO_INGRE"].ToString());
                    pingresos.FECHA_INGRE=Convert.ToDateTime(read["FECHA_INGRE"].ToString());
                    pingresos.TOTAL=(Convert.ToInt32(read["TOTAL"].ToString()));
                    
                    //Console.WriteLine(read["DOC_CLIENTE"].ToString());
                    Ventas.Add(pingresos);
                }
                
                conectar.Cerrar();
                return Ventas;
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
            
        }

        public void Totalizar()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT SUM (TOTAL) AS TOTALG FROM INGRESOS WHERE FECHA_INGRE>=@FECHA_INI AND FECHA_INGRE<=@FECHA_FIN";
                conectar.Abrir();

                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                DateTime fini = Convert.ToDateTime(Fecha_ini.Text);
                DateTime ffin = Convert.ToDateTime(Fecha_fin.Text);
                cmd.Parameters.AddWithValue("@FECHA_INI", Convert.ToDateTime(fini.Date.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@FECHA_FIN", Convert.ToDateTime(ffin.Date.ToString("yyyy-MM-dd")));
                //Console.WriteLine(pFechaIni);
                //Console.WriteLine(pFechaFin);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    TOTAL = read["TOTALG"].ToString();
                }

                conectar.Cerrar();


            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
