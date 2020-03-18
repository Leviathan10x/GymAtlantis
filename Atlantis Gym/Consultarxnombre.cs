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
    public partial class Consultarxnombre : Form
    {
        public Int64 UsActivo;
        
        public Consultarxnombre(Int64 pUsActivo)
        {
            InitializeComponent();
            this.UsActivo = pUsActivo;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        public string Idselec="";
        private static List<Buascarnombre> Buscar(string lNombre, string lApellido)
        {
            
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                List<Buascarnombre> Consulta = new List<Buascarnombre>();
                //string comando = "SELECT NOMBRE,APELLIDO,ID_CLIENTE AS DOCUMENTO FROM CLIENTES WHERE NOMBRE LIKE '%{0}%' AND APELLIDO LIKE '%{1}%'";
                SqlCommand cmd = new SqlCommand(string.Format("SELECT NOMBRE, APELLIDO, ID_CLIENTE AS DOCUMENTO FROM CLIENTES WHERE NOMBRE LIKE '%{0}%' AND APELLIDO LIKE '%{1}%'", lNombre, lApellido),conectar.Conectarbd);
                //cmd.Parameters.AddWithValue("@NOMBRE", lNombre);
                //cmd.Parameters.AddWithValue("@APELLIDO", lApellido);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Buascarnombre pbuascarnombre = new Buascarnombre();
                    pbuascarnombre.Nombre = read["NOMBRE"].ToString();
                    pbuascarnombre.Apellido = read["APELLIDO"].ToString();
                    pbuascarnombre.Documento =Convert.ToInt64( read["DOCUMENTO"].ToString());
                    Consulta.Add(pbuascarnombre);

                }
                conectar.Cerrar();
                return Consulta;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Buscar(textNombre.Text, textApellido.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Idselec =Convert.ToString( dataGridView1.CurrentRow.Cells[2].Value);
                this.Close();
                Buscarcliente buscarcliente = new Buscarcliente(UsActivo, Idselec);
                buscarcliente.Show();

            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun Cliente");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Consultarxnombre_Load(object sender, EventArgs e)
        {

        }
    }
}
