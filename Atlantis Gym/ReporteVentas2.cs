using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Atlantis_Gym
{
    public partial class ReporteVentas2 : Form
    {
        public string TOTAL;
        public DataGridView DATA;
        public ReporteVentas2(string pTOTAL,string pFechain, string pfechafin)
        {
            InitializeComponent();
            this.TOTAL = pTOTAL;
            
            cargar();
            dataGridView1.DataSource = Consulta(pFechain, pfechafin);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ExportarExcel exportarExcel = new ExportarExcel();
            exportarExcel.ExportarDate(dataGridView1);

        }

        public void cargar()
        {
            
            labelTOTAL.Text = TOTAL;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        public static List<Ingresos> Consulta(string pFechaIni, string pFechaFin)
        {
            try
            {
                List<Ingresos> Ventas = new List<Ingresos>();
                Conexion conectar = new Conexion();
                string comando = "SELECT DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL FROM INGRESOS WHERE FECHA_INGRE>=@FECHA_INI AND FECHA_INGRE<=@FECHA_FIN";
                conectar.Abrir();

                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                DateTime fini = Convert.ToDateTime(pFechaIni);
                DateTime ffin = Convert.ToDateTime(pFechaFin);
                cmd.Parameters.AddWithValue("@FECHA_INI", Convert.ToDateTime(fini.Date.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@FECHA_FIN", Convert.ToDateTime(ffin.Date.ToString("yyyy-MM-dd")));
                Console.WriteLine(pFechaIni);
                Console.WriteLine(pFechaFin);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Ingresos pingresos = new Ingresos();
                    pingresos.DOC_CLIENTE = Convert.ToInt32(read["DOC_CLIENTE"].ToString());
                    pingresos.TIPO_INGRE = (read["TIPO_INGRE"].ToString());
                    pingresos.FECHA_INGRE = Convert.ToDateTime(read["FECHA_INGRE"].ToString());
                    pingresos.TOTAL = (Convert.ToInt32(read["TOTAL"].ToString()));

                    //Console.WriteLine(read["DOC_CLIENTE"].ToString());
                    Ventas.Add(pingresos);
                }

                conectar.Cerrar();
                return Ventas;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }

        }
    }
}
