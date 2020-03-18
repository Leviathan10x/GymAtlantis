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
    public partial class NombresUsuarios : Form
    {
        public Int32 Id;
        public string Nombre;
        public NombresUsuarios()
        {
            InitializeComponent();
            dataGridView1.DataSource = Lusuario();
        }

        private void NombresUsuarios_Load(object sender, EventArgs e)
        {

        }

        private static List<Usuarios> Lusuario()
        {
            try
            {
                List<Usuarios> usuarios = new List<Usuarios>();
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT NOMBRE, APELLIDO, ID_USUARIO FROM USUARIOS";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Usuarios pUsuario = new Usuarios();
                    pUsuario.Nombre = (read["NOMBRE"].ToString()) +" "+ (read["APELLIDO"].ToString());
                    pUsuario.Id = read["ID_USUARIO"].ToString();
                    usuarios.Add(pUsuario);
                }
                conectar.Cerrar();
                return usuarios;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
                Nombre =Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                ReportesN reportesN = new ReportesN(Id,Nombre,false);
                reportesN.Show();
                this.Close();
            }
        }
    }
}
