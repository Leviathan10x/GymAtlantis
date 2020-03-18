using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GriauleFingerprintLibrary;
using GriauleFingerprintLibrary.Exceptions;
using System.Data.SqlClient;
using GriauleFingerprintLibrary.DataTypes;
using DPFP;
using DPFP.Capture;

namespace Atlantis_Gym
{
    public partial class CapturarHuella : Form
    {
        public Int64 pId;
        private DPFP.Template Template;
        public CapturarHuella(Int64 Id)
        {
            this.pId = Id;
            InitializeComponent();
            BotonGuardar.Enabled = true;
            //LeerHuella = new DPFP.Capture.();
            //Fingerprint = new FingerprintCore();
            //Fingerprint.onStatus += new StatusEventHandler(Fingerprint_onStatus);
            //Fingerprint.onFinger += new FingerEventHandler(Fingerprint_onFinger);
            //Fingerprint.onImage += new ImageEventHandler(Fingerprint_onImage);
        }
        

       // public bool InvokeRequired { get; }
        //private FingerprintCore Fingerprint;
        //private GriauleFingerprintLibrary.DataTypes.FingerprintRawImage rawImage;
        //GriauleFingerprintLibrary.DataTypes.FingerprintTemplate Template;

        /*void Fingerprint_onStatus(object source, GriauleFingerprintLibrary.Events.StatusEventArgs se)
        {
            if (se.StatusEventType == GriauleFingerprintLibrary.Events.StatusEventType.SENSOR_PLUG)
            {
                Fingerprint.StartCapture(source.ToString());
            }
            else
            {
                Fingerprint.StopCapture(source);
            }
        }

        private void Fingerprint_onImage(object source, GriauleFingerprintLibrary.Events.ImageEventArgs ie)
        {
            rawImage = ie.RawImage;
            SetImage(ie.RawImage.Image);

            ExtractTemplate();
        }

        private delegate void delSetImage(Image img);
        void SetImage(Image img)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delSetImage(SetImage), new object[] { img });
            }
            else
            {
                Bitmap bmp = new Bitmap(img, pictureBoxHuella.Width, pictureBoxHuella.Height);
                pictureBoxHuella.Image = bmp;
            }
        }

        private void ExtractTemplate()
        {
            if (rawImage != null)
            {
                try
                {
                    Template = null;
                    Fingerprint.Extract(rawImage, ref Template);

                    SetQualityBar(Template.Quality);
                    DisplayImage(Template, false);
                }
                catch
                {
                    SetQualityBar(-1);

                }
            }

        }

        delegate void delSetQualityBar(int Quality);
        private void SetQualityBar(int Quality)
        {
            if (pgrbQuality.InvokeRequired == true)
            {
                this.Invoke(new delSetQualityBar(SetQualityBar), new object[] { Quality });
            }
            else
            {
                switch(Quality)
                {
                    case 0:
                        {
                            pgrbQuality.ForeColor = System.Drawing.Color.LightCoral;
                            pgrbQuality.Value = pgrbQuality.Maximum / 3;
                        }break;

                    case 1:
                        {
                            pgrbQuality.ForeColor = System.Drawing.Color.GreenYellow;
                            pgrbQuality.Value = pgrbQuality.Maximum / 3 * 2;
                        }break;

                    case 2:
                        {
                            pgrbQuality.ForeColor = System.Drawing.Color.MediumSeaGreen;
                            pgrbQuality.Value = pgrbQuality.Maximum;
                        }break;
                    default:
                        {
                            pgrbQuality.ForeColor = System.Drawing.Color.Gray;
                            pgrbQuality.Value = 0 ;
                        }break;
                }
            }
        }

        private void DisplayImage(GriauleFingerprintLibrary.DataTypes.FingerprintTemplate template,bool identify)
        {
            IntPtr hdc = FingerprintCore.GetDC();
            IntPtr Image = new IntPtr();
            if (identify)
            {
                Fingerprint.GetBiometricDisplay(template, rawImage, hdc, ref Image, FingerprintConstants.GR_DEFAULT_CONTEXT);
                BotonGuardar.Enabled = true;
            }
            else
            {
                Fingerprint.GetBiometricDisplay(template, rawImage, hdc, ref Image, FingerprintConstants.GR_NO_CONTEXT);
                BotonGuardar.Enabled = false;
            }
            SetImage(Bitmap.FromHbitmap(Image));
            FingerprintCore.ReleaseDC(hdc);
        }

        public void guardar(FingerprintTemplate fingerprintTemplate,Int64 id,int qa)
        {
            try
            {
                ConexionHuella conexionHuella = new ConexionHuella();
                conexionHuella.Abrir();
                string COMANDO = "INSERT INTO HuellasClientes(ID, HUELLA, QUALITY) VALUES(@ID, @HUELLA, @QUALITY)";
                SqlCommand cmd = new SqlCommand(COMANDO, conexionHuella.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", pId);
                //cmd.Parameters.AddWithValue("@HUELLA",fingerprintTemplate.Size,ParameterDirection.Input);
                //cmd.Parameters.AddWithValue("@QUALITY",);

            }
            catch
            {

            }
        }
        */
        private void BotonGuardar_Click(object sender, EventArgs e)
        {
            CHuella cHuella = new CHuella();
            cHuella.OnTemplate += this.OnTemplate;
            cHuella.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                btnGuardarBD.Enabled = (Template != null);
                if (Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                    txtHuella.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
                }
            }));
        }

        private void CapturarHuella_Load(object sender, EventArgs e)
        {
                   
        }

        private void BotonCerrar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void BtnGuardarBD_Click(object sender, EventArgs e)
        {
            GuardarBD();
            
        }

        public void GuardarBD()
        {
            try
            {
                byte[] streamHuella = Template.Bytes;
                ConexionHuella conexionHuella = new ConexionHuella();
                conexionHuella.Abrir();
                String Comando = "INSERT INTO HUELLASCLIENTES(ID,HUELLA) VALUES(@ID,@HUELLA)";
                SqlCommand cmd = new SqlCommand(Comando, conexionHuella.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", pId);
                cmd.Parameters.AddWithValue("@HUELLA", streamHuella);
                int ok = cmd.ExecuteNonQuery();

                if (ok == 1)
                {
                    MessageBox.Show("Huella guardada con Exito...", "OK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar la huella...", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conexionHuella.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        

    }
}

