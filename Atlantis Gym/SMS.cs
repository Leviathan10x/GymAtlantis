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
    public partial class SMS : Form
    {
        public Boolean confir;
        public SMS()
        {

            
            InitializeComponent();
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void Editar()
        {
            try
            {

                ConexionSMS conexionSMS = new ConexionSMS();
                conexionSMS.Abrir();
                string comando = "UPDATE CONFISMS SET NroCLIENTE=@NC,CLIENTE=@C,HEADER=@H,KY=@KY,CODIGOPAIS=@CP";

                SqlCommand cmd = new SqlCommand(comando, conexionSMS.Conectarbd);
                cmd.Parameters.AddWithValue("@NC", textNroCliente.Text);
                cmd.Parameters.AddWithValue("@C", textCliente.Text);
                cmd.Parameters.AddWithValue("@H", textHeader.Text);
                cmd.Parameters.AddWithValue("@KY", textKey.Text);
                cmd.Parameters.AddWithValue("@CP", textCodP.Text);
                int ok = cmd.ExecuteNonQuery();

                if (ok == 1)
                {
                    MessageBox.Show("Configuracion Actulualizada con exito", "OK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    MessageBox.Show("Configuracion Fallo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conexionSMS.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void Guardar()
        {
            try
            {

                ConexionSMS conexionSMS = new ConexionSMS();
                conexionSMS.Abrir();
                string comando = "INSERT INTO CONFISMS (NroCLIENTE,CLIENTE,HEADER,KY,CODIGOPAIS) VALUES (@NC,@C,@H,@KY,@CP)";

                SqlCommand cmd = new SqlCommand(comando, conexionSMS.Conectarbd);
                cmd.Parameters.AddWithValue("@NC", textNroCliente.Text);
                cmd.Parameters.AddWithValue("@C", textCliente.Text);
                cmd.Parameters.AddWithValue("@H", textHeader.Text);
                cmd.Parameters.AddWithValue("@KY", textKey.Text);
                cmd.Parameters.AddWithValue("@CP", textCodP.Text);
                int ok = cmd.ExecuteNonQuery();

                if (ok == 1)
                {
                    MessageBox.Show("Configuracion realizada con exito", "OK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    MessageBox.Show("Configuracion Fallo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conexionSMS.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static int comprobar()
        {
            int x = 0;
            try
            {
                ConexionSMS conexionSMS = new ConexionSMS();
                conexionSMS.Abrir();
                string comando = "SELECT COUNT(*) AS CUENTA FROM CONFISMS";
                SqlCommand cmd = new SqlCommand(comando, conexionSMS.Conectarbd);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    x=Convert.ToInt32( reader["CUENTA"].ToString());
                }

                conexionSMS.Cerrar();
                return x;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public void cargar()
        {
            try
            {
                ConexionSMS conexionSMS = new ConexionSMS();
                conexionSMS.Abrir();

                string comando = "SELECT * FROM CONFISMS";

                SqlCommand cmd = new SqlCommand(comando, conexionSMS.Conectarbd);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textCliente.Text = reader["CLIENTE"].ToString();
                    textNroCliente.Text = reader["NroCLIENTE"].ToString();
                    textKey.Text = reader["KY"].ToString();
                    textHeader.Text = reader["HEADER"].ToString();
                    textCodP.Text = reader["CODIGOPAIS"].ToString();
                }

                conexionSMS.Cerrar();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (confir)
            {
                Editar();
            }
            else
            {
                Guardar();
            }
        }

        private void SMS_Load(object sender, EventArgs e)
        {
            int a = comprobar();
            if (a == 1)
            {
                cargar();
                confir = true;
            }
            else
            {
                MessageBox.Show("No se han creado configuraciones", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                confir = false;
            }
        }
    }
}
