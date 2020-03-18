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
    public partial class BuscarIngresos : Form
    {
        public BuscarIngresos()
        {
            InitializeComponent();
            Datos_Reporte datos_Reporte = new Datos_Reporte();
            comboBox1.DataSource = Nombre();
        }
        private static List<string> Nombre()
        {
            try
            {
                List<string> nom = new List<string>();
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT NOMBRE FROM USUARIOS";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    nom.Add(read["NOMBRE"].ToString());
                }
                conectar.Cerrar();
                return nom;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private static List<Ingresos2> Buscar(DateTime fecha,string usuario)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                List<Ingresos2> buscar = new List<Ingresos2>();
                string comando = "SELECT ID_INGRESO,DOC_CLIENTE,RTRIM (TIPO_INGRE) AS TIPO_INGRE,FECHA_INGRE,INGRESOS.ID_USUARIO,FECHA_INICIO,TOTAL FROM INGRESOS INNER JOIN USUARIOS ON NOMBRE=@NOMBRE WHERE FECHA_INGRE=@FECHA";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@NOMBRE", usuario);
                cmd.Parameters.AddWithValue("@FECHA", fecha);
                Console.WriteLine(usuario);
                Console.WriteLine(fecha);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Ingresos2 ingresos = new Ingresos2();
                    ingresos.ID_INGRESO = Convert.ToInt32(read["ID_INGRESO"].ToString());
                    ingresos.DOC_CLIENTE = Convert.ToInt32(read["DOC_CLIENTE"].ToString());
                    ingresos.TIPO_INGRE = (read["TIPO_INGRE"].ToString());
                    ingresos.TOTAL = Convert.ToInt32(read["TOTAL"].ToString());
                    ingresos.ID_USUARIO = Convert.ToInt32(read["ID_USUARIO"].ToString());
                    ingresos.FECHA_INGRE = Convert.ToDateTime(read["FECHA_INGRE"].ToString());
                    ingresos.FECHA_INI = Convert.ToDateTime(read["FECHA_INICIO"].ToString());
                    buscar.Add(ingresos);
                }
                conectar.Cerrar();
                return buscar;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                DateTime fecha1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                dataGridView1.DataSource = Buscar( fecha1, comboBox1.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Seleccione un usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Int32 a1 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                string a2 = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                DateTime a3 = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
                Int32 a4 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
                Int32 a5 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
                DateTime a6 = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value);
                Int32 a7 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
                SuperEdicion superEdicion = new SuperEdicion(a7,a1,a2,a4,a5,a3,a6);
                

                this.Close();
                
                superEdicion.Show();

            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun Cliente");
            }
        }
    }
}
