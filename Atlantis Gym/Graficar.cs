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
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
namespace Atlantis_Gym
{
    public partial class Graficar : Form
    {
        public Int64 Doc;
        public DateTime FechaHoy,FechaRefe;
        
        public Graficar(Int64 pDoc,string pNombre)
        {
            InitializeComponent();
            this.Doc = pDoc;
            LabelDoc.Text = Convert.ToString(pDoc);
            LabelNombre.Text = pNombre;
        }

        public void CargarBrazos()
        {
            try
            {
                ArrayList BrazoD = new ArrayList();
                ArrayList BrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_BRAZO_D,D_BRAZO_I,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    BrazoD.Add(Convert.ToDouble(read["D_BRAZO_D"].ToString()));
                    BrazoI.Add(Convert.ToDouble(read["D_BRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                chart1.Series[0].Points.DataBindXY(FechaM, BrazoD);
                chart1.Series[0].Name = "Brazo Derecho";
                chart1.Series[1].Points.DataBindXY(FechaM, BrazoI);
                chart1.Series[1].Name = "Brazo Izquierdo";
                chart1.Titles[0].Text="Diametro de Brazos (cm)";
                chart1.Series[1].IsVisibleInLegend = true;
                chart2.Visible = false;
                chart1.Visible = true;




                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CargarAntebrazos()
        {
            try
            {
                ArrayList AnteBrazoD = new ArrayList();
                ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_ANTEBRAZO_D,D_ANTEBRAZO_I,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    AnteBrazoD.Add(Convert.ToDouble(read["D_ANTEBRAZO_D"].ToString()));
                    AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                chart1.Series[0].Points.DataBindXY(FechaM, AnteBrazoD);
                chart1.Series[0].Name = "Antebrazo Derecho";
                chart1.Series[1].Points.DataBindXY(FechaM, AnteBrazoI);
                chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart1.Titles[0].Text = "Diametro de Antebrazos (cm)";
                chart1.Series[1].IsVisibleInLegend = true;
                chart2.Visible = false;
                chart1.Visible = true;

                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarPeso()
        {
            try
            {
                ArrayList Peso = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT PESO,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Peso.Add(Convert.ToDouble(read["PESO"].ToString())/1000);
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Peso);
                chart2.Series[0].Points.DataBindXY(FechaM, Peso);
                chart2.Series[0].Name = "Peso";
                
                
                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Peso Kg";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart1.Visible = false;
                chart2.Visible = true;
                
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void IdiMasaCorp()
        {
            try
            {
                ArrayList Idice = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT INDI_MASA_CORP,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Idice.Add(Convert.ToDouble(read["INDI_MASA_CORP"].ToString()));
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Name = "Indice de masa corporal";


                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Indice de masa corporal";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart2.Visible = true;
                chart1.Visible = false;
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Hombros()
        {
            try
            {
                ArrayList Hombros = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_HOMBRO,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Hombros.Add(Convert.ToDouble(read["D_HOMBRO"].ToString()));
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Points.DataBindXY(FechaM, Hombros);
                chart2.Series[0].Name = "Diametro de Hombros";


                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Diametro de Hombros (cm)";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart2.Visible = true;
                chart1.Visible = false;
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarPecho()
        {
            try
            {
                ArrayList Pecho = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_PECHO,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Pecho.Add(Convert.ToDouble(read["D_PECHO"].ToString()));
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Points.DataBindXY(FechaM, Pecho);
                chart2.Series[0].Name = "Diametro Pecho";


                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Diametro Pecho (cm)";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart2.Visible = true;
                chart1.Visible = false;
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarGluteo()
        {
            try
            {
                ArrayList Gluteo = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_GLUTEO,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Gluteo.Add(Convert.ToDouble(read["D_GLUTEO"].ToString()));
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Points.DataBindXY(FechaM, Gluteo);
                chart2.Series[0].Name = "Diametro Gluteos";


                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Diametro Gluteos (cm)";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart2.Visible = true;
                chart1.Visible = false;
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarAbds()
        {
            try
            {
                ArrayList Adbs = new ArrayList();
                //ArrayList AnteBrazoI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_ABDOMEN,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Adbs.Add(Convert.ToDouble(read["D_ABDOMEN"].ToString()));
                    //AnteBrazoI.Add(Convert.ToDouble(read["D_ANTEBRAZO_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                //chart1.Series[1].Points.DataBindXY(FechaM, Idice);
                chart2.Series[0].Points.DataBindXY(FechaM, Adbs);
                chart2.Series[0].Name = "Diametro Abdomen";


                //chart1.Series[1].Name = "Antebrazo Izquierdo";
                chart2.Titles[0].Text = "Diametro Abdomen (cm)";
                //chart1.Series[1].IsVisibleInLegend = false;
                chart2.Visible = true;
                chart1.Visible = false;
                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CargarPiernas()
        {
            try
            {
                ArrayList PiernaD = new ArrayList();
                ArrayList PiernaI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_PIERNA_D,D_PIERNA_I,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    PiernaD.Add(Convert.ToDouble(read["D_PIERNA_D"].ToString()));
                    PiernaI.Add(Convert.ToDouble(read["D_PIERNA_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                chart1.Series[0].Points.DataBindXY(FechaM, PiernaD);
                chart1.Series[0].Name = "Pierna Derecha";
                chart1.Series[1].Points.DataBindXY(FechaM, PiernaI);
                chart1.Series[1].Name = "Pierna Izquierda";
                chart1.Titles[0].Text = "Diametro de piernas (cm)";
                chart1.Series[1].IsVisibleInLegend = true;
                chart2.Visible = false;
                chart1.Visible = true;




                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CargarPantorrillas()
        {
            try
            {
                ArrayList PantD = new ArrayList();
                ArrayList PantI = new ArrayList();
                ArrayList FechaM = new ArrayList();
                Conexion conectar = new Conexion();
                DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                conectar.Abrir();
                string comando = "SELECT D_PANTORRILLA_D,D_PANTORRILLA_I,FECHA FROM MEDIDASC WHERE DOC_CLIENTE=@DOC AND FECHA>=@FECHA_REFE";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Doc);
                cmd.Parameters.AddWithValue("@FECHA_REFE", FechaRefe);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    PantD.Add(Convert.ToDouble(read["D_PANTORRILLA_D"].ToString()));
                    PantI.Add(Convert.ToDouble(read["D_PANTORRILLA_I"].ToString()));
                    DateTime F = Convert.ToDateTime(read["FECHA"].ToString());
                    FechaM.Add(F.ToString("dd-MM-yyyy"));
                }
                chart1.Series[0].Points.DataBindXY(FechaM, PantD);
                chart1.Series[0].Name = "Pantorrilla Derecha";
                chart1.Series[1].Points.DataBindXY(FechaM, PantI);
                chart1.Series[1].Name = "Pantorrilla Izquierda";
                chart1.Titles[0].Text = "Diametro de Pantorrillas (cm)";
                chart1.Series[1].IsVisibleInLegend = true;
                chart2.Visible = false;
                chart1.Visible = true;




                conectar.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Graficar_Load(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            if (comboPeriodo.SelectedIndex == 0)
            {
                FechaRefe = FechaHoy.AddMonths(-3);
                
            }
            else
            {
                if (comboPeriodo.SelectedIndex == 1)
                {
                    FechaRefe = FechaHoy.AddMonths(-6);
                }
                else
                {
                    if (comboPeriodo.SelectedIndex == 2)
                    {
                        FechaRefe = FechaHoy.AddYears(-1);
                    }

                    else
                    {
                        if (comboPeriodo.SelectedIndex == 3)
                        {
                            FechaRefe = FechaHoy.AddYears(-5);
                        }
                        else
                        {
                            MessageBox.Show("Primero debe seleccionar un periodo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FechaRefe = FechaHoy;
                        }
                    }
                }
            }
            switch (combotipo.SelectedIndex)
            {
                case 0:
                    CargarPeso();
                    break;
                case 1:
                    IdiMasaCorp();
                    break;
                case 2:
                    CargarBrazos();
                    break;
                case 3:
                    CargarAntebrazos();
                    break;
                case 4:
                    Hombros();
                    break;
                case 5:
                    CargarPecho();
                    break;
                case 6:
                    CargarAbds();
                    break;
                case 7:
                    CargarGluteo();
                    break;
                case 8:
                    CargarPiernas();
                    break;
                case 9:
                    CargarPantorrillas();
                    break;
                default:
                    MessageBox.Show("No ha seleccionado un item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                    

            }
        }
    }
}
