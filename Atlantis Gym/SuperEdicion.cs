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
    public partial class SuperEdicion : Form
    {
        public Int32 DOC_CLIENTE;
        public string TIPO_INGRE;
        public DateTime FECHA_INGRE;
        public Int32 TOTAL;

        public Int32 ID_USUARIO;

        public DateTime FECHA_INI;

        public Int32 ID_INGRESO;
        public SuperEdicion(Int32 pId_ingreso, Int32 pDOC_CLIENTE, string pTIPO_INGRE, Int32 pTOTAL, Int32 pId_Usuario, DateTime pFECHA_INGRE, DateTime pFecha_inicio)
        {
            InitializeComponent();
            this.DOC_CLIENTE = pDOC_CLIENTE;
            this.TIPO_INGRE = pTIPO_INGRE;
            this.FECHA_INGRE = pFECHA_INGRE;
            this.TOTAL = pTOTAL;
            this.ID_USUARIO = pId_Usuario;
            this.FECHA_INI = pFecha_inicio;
            this.ID_INGRESO = pId_ingreso;

            textDocCliente.Text =Convert.ToString( pDOC_CLIENTE);
            textTipoIngreso.Text = pTIPO_INGRE;
            dateTimeFechaIngre.Value = pFECHA_INGRE;
            textTotal.Text =Convert.ToString(pTOTAL);
            textDocUsuario.Text =Convert.ToString( pId_Usuario);
            dateTimeFechaInicio.Value = pFecha_inicio;
            labelID.Text =Convert.ToString( pId_ingreso);
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextDocCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void SuperEdicion_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'datos_Reporte.INGRESOS' Puede moverla o quitarla según sea necesario.
            this.iNGRESOSTableAdapter.Fill(this.datos_Reporte.INGRESOS);

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BuscarIngresos buscarIngresos = new BuscarIngresos();
            buscarIngresos.Show();
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (UsuarioOK(Convert.ToInt32(textDocUsuario.Text)))
            {
                if (ClienteOK(Convert.ToInt32(textDocCliente.Text)))
                {
                    if (TipoPagoOK(textTipoIngreso.Text))
                    {
                        if (dateTimeFechaIngre.Value <= DateTime.Now)
                        {
                            Guardar();

                        }
                        else
                        {
                            MessageBox.Show("La fecha del ingreso no puede ser mayor a la fecha de hoy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El ingreso es errado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
                    }
                }
                else
                {
                    MessageBox.Show("El cliente no existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El Usuario No existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Guardar()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "UPDATE INGRESOS SET DOC_CLIENTE=@DOC_CLIENTE, TIPO_INGRE=@TIPO_INGRESO, FECHA_INGRE=@fECHA_INGRESO, TOTAL=@TOTAL, ID_USUARIO=@ID_USUARIO, FECHA_INICIO=@FECHA_INICIO WHERE ID_INGRESO=@ID_INGRESO";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC_CLIENTE", Convert.ToInt32(textDocCliente.Text));
                cmd.Parameters.AddWithValue("@TIPO_INGRESO", textTipoIngreso.Text);
                cmd.Parameters.AddWithValue("@FECHA_INGRESO", dateTimeFechaIngre.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@TOTAL", Convert.ToInt32(textTotal.Text));
                cmd.Parameters.AddWithValue("@ID_USUARIO", Convert.ToInt32(textDocUsuario.Text));
                cmd.Parameters.AddWithValue("@FECHA_INICIO", dateTimeFechaInicio.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ID_INGRESO", Convert.ToInt32(labelID.Text));
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Ingreso editado con exito", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al editar este ingreso", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static bool TipoPagoOK(string TipoPago)
        {
            bool ok;
            switch (TipoPago)
            {
                case "Mensual":
                    ok = true;
                    break;
                case "Quincenal":
                    ok = true;
                    break;
                case "Semanal":
                    ok = true;
                    break;
                case "Diario":
                    ok = true;
                    break;
                case "2 Meses":
                    ok = true;
                    break;
                case "3 Meses":
                    ok = true;
                    break;
                case "6 Meses":
                    ok = true;
                    break;
                case "Anual":
                    ok = true;
                    break;
                default:
                    ok = false;
                    break;
            }
            return ok;
        }

        private static bool ClienteOK(Int32 IdCliente)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT * FROM CLIENTES WHERE ID_CLIENTE=@ID";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", IdCliente);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    conectar.Cerrar();
                    return true;

                }
                else
                {
                    conectar.Cerrar();
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool UsuarioOK(Int32 IdUsuario)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT * FROM USUARIOS WHERE ID_USUARIO=@ID";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", IdUsuario);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    conectar.Cerrar();
                    return true;
                }
                else
                {
                    conectar.Cerrar();
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
