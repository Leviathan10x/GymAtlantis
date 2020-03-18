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
    public partial class BuscarUsuario : Form
    {
        public BuscarUsuario()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        private static List<Buascarnombre> Busqueda(string lNombre,String lApellido)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                List<Buascarnombre> Consulta = new List<Buascarnombre>();
                //string comando = "SELECT NOMBRE,APELLIDO,ID_CLIENTE AS DOCUMENTO FROM CLIENTES WHERE NOMBRE LIKE '%{0}%' AND APELLIDO LIKE '%{1}%'";
                SqlCommand cmd = new SqlCommand(string.Format("SELECT NOMBRE, APELLIDO, ID_USUARIO AS DOCUMENTO FROM USUARIOS WHERE NOMBRE LIKE '%{0}%' AND APELLIDO LIKE '%{1}%'", lNombre, lApellido), conectar.Conectarbd);
                //cmd.Parameters.AddWithValue("@NOMBRE", lNombre);
                //cmd.Parameters.AddWithValue("@APELLIDO", lApellido);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Buascarnombre pbuascarnombre = new Buascarnombre();
                    pbuascarnombre.Nombre = read["NOMBRE"].ToString();
                    pbuascarnombre.Apellido = read["APELLIDO"].ToString();
                    pbuascarnombre.Documento = Convert.ToInt64(read["DOCUMENTO"].ToString());
                    Consulta.Add(pbuascarnombre);

                }
                conectar.Cerrar();
                return Consulta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Busqueda(textNombre.Text, textApellido.Text);
        }
        public string Idselec;

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Idselec = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                this.Close();
                EditarUsuario editarUsuario = new EditarUsuario(Idselec);
                editarUsuario.Show();

            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun Cliente");
            }
        }
    }
}
