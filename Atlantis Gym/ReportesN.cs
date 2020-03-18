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
    public partial class ReportesN : Form
    {
        public Int32 ID;
        public string Nombre;
        public bool Check;
        public ReportesN(Int32 pID,string pNombre,bool pCheck)
        {
            InitializeComponent();
            checkPorUsuario.Checked = pCheck;
            
            
            
            this.ID = pID;
            this.Nombre = pNombre;
            this.Check = pCheck;
            if (Check)
            {
                LabelNombreUsuario.Visible = false;
                //LabelNombreUsuario.Text = Nombre;
            }
            else
            {
                LabelNombreUsuario.Visible = true;
                LabelNombreUsuario.Text = Nombre;
            }
        }

        private void FlowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPorUsuario.Checked)
            {
                LabelNombreUsuario.Visible = false;
                
            }
            else
            {
                this.Close();
                NombresUsuarios nombresUsuarios = new NombresUsuarios();
                nombresUsuarios.Show();
                
            }
        }

        private void ReportesN_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public void CargarReporte()
        {
            
        }
    }
}
