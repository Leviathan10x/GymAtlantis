using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPFP;
using DPFP.Capture;
using System.Data.SqlClient;


namespace Atlantis_Gym
{
    delegate void Funcion();

    public partial class RegisIngresos : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        private AtlantisHuellasEntities contexto;
        private Int64 PID;
        public Int64 UsserID;
        public Int64 CedulaPago;
        public string Nombre;
        public string Apellido;
        public string TPago;
        public delegate void Verificador();
        //public event Verificador Verificar;
        public Boolean Veri;

        public RegisIngresos(Int64 IDUSD)
        {
            contexto = new AtlantisHuellasEntities();
            InitializeComponent();
            this.UsserID = IDUSD;
            //Listar();
            dtgLista.DataSource = LisRegist(IDUSD);
            //pintar();
            //pictureBoxVERDE.Visible = false;
            //pictureBoxROJO.Visible = false;
        }
        private DPFP.Capture.Capture Capturer;

        //public Template Template { get; private set; }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                {
                    SetStatus("No se pudo iniciar la operación de captura");
                }
            }
            catch
            {
                MessageBox.Show("No se pudo iniciar la operación de captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            // Draw fingerprint sample image.
            //DrawPicture(ConvertSampleToBitmap(Sample));
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    //SetPrompt("Escanea tu huella usando el lector");
                }
                catch
                {
                    //SetPrompt("No se puede iniciar la captura");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    MessageBox.Show("No se puede terminar la captura");
                    //SetPrompt("No se puede terminar la captura");
                }
            }
        }



        private void RegisIngresos_Load(object sender, EventArgs e)
        {
            //Listar();
            Init2();
            Start();

            pintar();

            


        }

        private void RegisIngresos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            SetStatus("La muestra ha sido capturada");
            //SetPrompt("Escanea tu misma huella otra vez");
            Process2(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            SetStatus("La huella fue removida del lector");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            SetStatus("El lector fue tocado");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            SetStatus("El Lector de huellas ha sido conectado");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            SetStatus("El Lector de huellas ha sido desconectado");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            {
                //MakeReport("La calidad de la muestra es BUENA");
            }
            else
            {
                //MakeReport("La calidad de la muestra es MALA");
            }
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }

        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate ()
            {
                LVisor.Text = status;
            }));
        }

        private void Regis(Int64 ID)
        {
            int ok1;
            try
            {
                DateTime Hoy =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime Manana = Hoy.AddDays(1);
                ConexionHuella conexionHuella1 = new ConexionHuella();
                conexionHuella1.Abrir();
                string com = "SELECT ID FROM REGISTROS WHERE (FECHAYHORA>@FECHAHOY AND FECHAYHORA<@FECHAMANANA) AND IDUSUARIO=@USSERACTIVO AND ID=@IDCLIENTE";
                SqlCommand cmd1 = new SqlCommand(com, conexionHuella1.Conectarbd);
                cmd1.Parameters.AddWithValue("@FECHAHOY", Hoy);
                cmd1.Parameters.AddWithValue("@FECHAMANANA", Manana);
                cmd1.Parameters.AddWithValue("@USSERACTIVO", UsserID);
                cmd1.Parameters.AddWithValue("@IDCLIENTE", ID);
                SqlDataReader reader = cmd1.ExecuteReader();
                ok1 = 0;
                while (reader.Read())
                {
                    ok1 = ok1 + 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ToString());
                ok1 = 2;
            }
            if (ok1 == 0)
            {


                try
                {
                    DateTime dateTime = DateTime.Now;
                    
                    ConexionHuella conexionHuella = new ConexionHuella();
                    conexionHuella.Abrir();
                    string comando = "INSERT INTO REGISTROS (ID,FECHAYHORA,IDUSUARIO,FECHA) VALUES(@ID,@FECHAYHORA,@IDUSUARIO,@FECHA)";
                    SqlCommand cmd = new SqlCommand(comando, conexionHuella.Conectarbd);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@FECHAYHORA", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@IDUSUARIO", UsserID);
                    cmd.Parameters.AddWithValue("@FECHA", dateTime.Date.ToString("yyyy-MM-dd"));
                    
                    int ok = cmd.ExecuteNonQuery();
                    if (ok == 1)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Error al registrar ingreso", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conexionHuella.Cerrar();

                    try
                    {
                        Conexion conexion = new Conexion();
                        conexion.Abrir();
                        string com = "SELECT NINGRESOS.FECHA_INICIO,INGRESOS.TIPO_INGRE FROM CLIENTES INNER JOIN (select max(FECHA_INICIO)AS FECHA_INICIO,DOC_CLIENTE from INGRESOS group by DOC_CLIENTE) AS NINGRESOS  ON NINGRESOS.DOC_CLIENTE = CLIENTES.ID_CLIENTE INNER JOIN INGRESOS ON NINGRESOS.DOC_CLIENTE = INGRESOS.DOC_CLIENTE AND NINGRESOS.FECHA_INICIO=INGRESOS.FECHA_INICIO WHERE CLIENTES.ID_CLIENTE=@ID ";
                        SqlCommand cmd3 = new SqlCommand(com, conexion.Conectarbd);
                        cmd3.Parameters.AddWithValue("@ID", ID);

                        SqlDataReader reader2 = cmd3.ExecuteReader();
                        int ven=0;
                        while (reader2.Read())
                        {
                            string TipoPago = reader2["TIPO_INGRE"].ToString();
                            //listaR.Tipo_Pago = reader2["TIPO_INGRE"].ToString();
                            DateTime FechaIni = Convert.ToDateTime(reader2["FECHA_INICIO"].ToString());
                            DateTime FechaVen;
                            switch (TipoPago)
                            {
                                case "Mensual                  ":
                                    FechaVen = FechaIni.AddMonths(1);
                                    break;
                                case "Semanal                  ":
                                    FechaVen = FechaIni.AddDays(7);
                                    break;
                                case "Quincenal                ":
                                    FechaVen = FechaIni.AddDays(15);
                                    break;
                                case "2 Meses                  ":
                                    FechaVen = FechaIni.AddMonths(2);
                                    break;
                                case "3 Meses                  ":
                                    FechaVen = FechaIni.AddMonths(3);
                                    break;
                                case "6 Meses                  ":
                                    FechaVen = FechaIni.AddMonths(6);
                                    break;
                                case "Anual                    ":
                                    FechaVen = FechaIni.AddYears(1);
                                    break;
                                default:
                                    FechaVen = FechaIni;
                                    break;

                                    
                            }
                            if (FechaVen > DateTime.Now)
                            {
                                Señal(true);
                            }
                            else
                            {
                                Señal(false);
                            }
                        }
                        conexion.Cerrar();
                        
                        if (ven==1)
                        {


                            Veri = true;
                            
                        }
                        else
                        {


                            Veri = false;
                            
                        }
                        //Stop();
                        //Init2();
                        //Start();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    //Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                if (ok1 == 1)
                {
                    //MessageBox.Show("El Usuario ya se registro en este turno...", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Error de captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void Señal(Boolean si)
        {

            this.Invoke(new Verificador(delegate ()
            {
                if (si)
                {
                    pictureBoxROJO.Visible = false;
                    pictureBoxVERDE.Visible = true;
                }
                else
                {
                    pictureBoxVERDE.Visible = false;
                    pictureBoxROJO.Visible = true;
                }
            }));

        }


        private void Listar()
        {
            try
            {
                var registros = from emp in contexto.REGISTROS
                                select new
                                {
                                    Documento = emp.ID,
                                    Fecha_y_Hora = emp.FECHAYHORA
                                };
                if (registros != null)
                {
                    dtgLista.DataSource = registros.ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult Res;
            Res = MessageBox.Show("Recuerde que al cerrar se detiene el ingreso de registros", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
            if (Res == DialogResult.OK)
            {
                //Stop();
                this.Close();
            }

        }



        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected void Init2()
        {
            Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            UpdateStatus(0);
        }
        private void UpdateStatus(int FAR)
        {
            // Show "False accept rate" value
            SetStatus(String.Format("Usiario no registrado (FAR) = {0}", FAR));
        }
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        protected void Process2(DPFP.Sample Sample)
        {
            Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;



                foreach (var emp in contexto.HUELLASCLIENTES)
                {
                    stream = new MemoryStream(emp.HUELLA);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);
                    UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        Int64 ID = emp.ID;
                        this.PID = ID;
                        SetStatus(Convert.ToString(ID));
                        Regis(ID);
                        //dtgLista.DataSource = LisRegist(UsserID);
                        //Listar();
                        //MakeReport("The fingerprint was VERIFIED. " + emp.Nombre);
                        break;
                    }

                }



            }
        }

        private void pintar()
        {
            DateTime Hoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            for (int fila=0; fila < dtgLista.RowCount ; fila++)
            {
                DateTime FV =Convert.ToDateTime( dtgLista.Rows[fila].Cells[3].Value.ToString());
                DateTime nFV =Convert.ToDateTime (FV.ToString("yyyy-MM-dd"));
                if (nFV < Hoy)
                {
                    dtgLista.Rows[fila].DefaultCellStyle.BackColor = Color.IndianRed;
                    dtgLista.Rows[fila].DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
                }
                else
                {
                    dtgLista.Rows[fila].DefaultCellStyle.BackColor = Color.Chartreuse;
                    dtgLista.Rows[fila].DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
                }
            }
        }


        private static List<ListaR> LisRegist(Int64 IdUsser)
        {
            try
            {
                DateTime Hoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime Manana = Hoy.AddDays(1);
                List<ListaR> listas = new List<ListaR>();
                ConexionHuella conexionHuella = new ConexionHuella();
                conexionHuella.Abrir();
                string comando = "SELECT ID,FECHAYHORA FROM REGISTROS WHERE (FECHAYHORA>=@FECHAHOY AND FECHAYHORA<@FECHAMANANA) AND(IDUSUARIO=@USSERACTIVO)";
                SqlCommand cmd = new SqlCommand(comando, conexionHuella.Conectarbd);
                cmd.Parameters.AddWithValue("@FECHAHOY", Hoy);
                cmd.Parameters.AddWithValue("@FECHAMANANA", Manana);
                cmd.Parameters.AddWithValue("@USSERACTIVO", IdUsser);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListaR listaR = new ListaR();
                    listaR.Documento = reader["ID"].ToString();
                    Int64 Pdoc = Convert.ToInt64(listaR.Documento);
                    DateTime FechaHora = Convert.ToDateTime(reader["FECHAYHORA"]);
                    listaR.Fecha_y_Hora = FechaHora.ToString("HH:mm tt");
                    Conexion conexion = new Conexion();
                    conexion.Abrir();
                    string com = "SELECT NOMBRE,APELLIDO,NINGRESOS.FECHA_INICIO,INGRESOS.TIPO_INGRE FROM CLIENTES INNER JOIN  (select max(FECHA_INICIO)AS FECHA_INICIO,DOC_CLIENTE from INGRESOS group by DOC_CLIENTE) AS NINGRESOS  ON NINGRESOS.DOC_CLIENTE = CLIENTES.ID_CLIENTE INNER JOIN INGRESOS ON NINGRESOS.DOC_CLIENTE = INGRESOS.DOC_CLIENTE AND NINGRESOS.FECHA_INICIO=INGRESOS.FECHA_INICIO WHERE CLIENTES.ID_CLIENTE=@IDCLIENTE ";
                    SqlCommand cmd2 = new SqlCommand(com, conexion.Conectarbd);
                    cmd2.Parameters.AddWithValue("@IDCLIENTE", Pdoc);

                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        listaR.Nombre = reader2["NOMBRE"].ToString();
                        listaR.Apellido = reader2["APELLIDO"].ToString();
                        string TipoPago = reader2["TIPO_INGRE"].ToString();
                        listaR.Tipo_Pago = reader2["TIPO_INGRE"].ToString();
                        DateTime FechaIni = Convert.ToDateTime(reader2["FECHA_INICIO"].ToString());
                        DateTime FechaVen;
                        switch (TipoPago)
                        {
                            case "Mensual                  ":
                                FechaVen = FechaIni.AddMonths(1);
                                break;
                            case "Semanal                  ":
                                FechaVen = FechaIni.AddDays(7);
                                break;
                            case "Quincenal                ":
                                FechaVen = FechaIni.AddDays(15);
                                break;
                            case "2 Meses                  ":
                                FechaVen = FechaIni.AddMonths(2);
                                break;
                            case "3 Meses                  ":
                                FechaVen = FechaIni.AddMonths(3);
                                break;
                            case "6 Meses                  ":
                                FechaVen = FechaIni.AddMonths(6);
                                break;
                            case "Anual                    ":
                                FechaVen = FechaIni.AddYears(1);
                                break;
                            default:
                                FechaVen = FechaIni;
                                break;
                        }
                        listaR.Fecha_Vencimiento = FechaVen.ToString("dddd,dd MMMM yyyy");
                    }
                    conexion.Cerrar();
                    listas.Add(listaR);
                }
                conexionHuella.Cerrar();
                return listas;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }

        private void BtnRefres_Click(object sender, EventArgs e)
        {
            dtgLista.DataSource = LisRegist(UsserID);
            pintar();
        }

        private void DtgLista_MouseClick(object sender, MouseEventArgs e)
        {
            int posicionM;
            if (e.Button == MouseButtons.Left)
            {
                posicionM = dtgLista.HitTest(e.X, e.Y).RowIndex;
                ContextMenuStrip menu = new ContextMenuStrip();
                if (posicionM >= 0)
                {
                    int temp = 0;
                    menu.Items.Add("Pagar").Name = "Pagar";

                    if(int.TryParse(dtgLista.CurrentCell.Value.ToString(), out temp))
                    {
                        menu.Show(dtgLista, new Point(e.X, e.Y));
                        menu.ItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
                        CedulaPago = Convert.ToInt64(dtgLista.CurrentCell.Value.ToString());
                        


                    }
                    else
                    {
                        MessageBox.Show("Para pagar tiene que hacer Click sobre el numero de CEDULA...", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                        
                    
                    
                }

            }
        }

        void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DialogResult result;
            result=MessageBox.Show("¿Desea realizar el pago?","Pagar",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (dtgLista.SelectedRows.Count == 1)
                {

                    Nombre = dtgLista.CurrentRow.Cells[1].Value.ToString();
                    Apellido= dtgLista.CurrentRow.Cells[2].Value.ToString();
                    TPago= dtgLista.CurrentRow.Cells[3].Value.ToString();
                    DateTime FeVe = Convert.ToDateTime(TPago);
                    DateTime Fhoy =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
                    if (FeVe >= Fhoy)
                    {
                        MessageBox.Show("El pago Comenzara con la fecha de vencimiento...","INFO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        TPago = FeVe.ToString("yyyy-MM-dd");
                        Pagos pagos = new Pagos(CedulaPago, UsserID, Nombre, Apellido, TPago, 0);
                        pagos.Show();
                        
                    }
                    else
                    {
                        try
                        {
                            ConexionHuella conexionHuella = new ConexionHuella();
                            conexionHuella.Abrir();
                            string com = "SELECT ID,FECHA FROM REGISTROS WHERE (FECHA>@FECHAVEN AND FECHA<@FECHAHOY) AND ID=@CEDULA";
                            SqlCommand cmd = new SqlCommand(com, conexionHuella.Conectarbd);
                            cmd.Parameters.AddWithValue("@FECHAVEN", Convert.ToDateTime( FeVe.Date.ToString("yyyy-MM-dd")));
                            cmd.Parameters.AddWithValue("@FECHAHOY", Convert.ToDateTime( Fhoy.Date.ToString("yyyy-MM-dd"))); 
                            cmd.Parameters.AddWithValue("@CEDULA", CedulaPago);
                            Console.WriteLine(CedulaPago);
                            Console.WriteLine(FeVe.Date.ToString("yyyy-MM-dd"));
                            Console.WriteLine(Fhoy);
                            int ok = 0;
                            int i = 0;
                            Console.WriteLine(ok);
                            SqlDataReader reader = cmd.ExecuteReader();
                            DateTime[] fechas = new DateTime[100];
                            while (reader.Read())
                            {
                                Int64 cd =Convert.ToInt64( reader["ID"].ToString());
                                DateTime f = Convert.ToDateTime(reader["FECHA"].ToString());
                                fechas[i] = f;
                                i++;
                                ok++;
                                Console.WriteLine(cd);
                                Console.WriteLine(ok);
                                
                            }
                            conexionHuella.Cerrar();
                            //int cont = 0;
                            int total = 0;
                            for (int j = 0; j < i; j++)
                            {
                                int cont = 0;
                                //Console.WriteLine(cont);
                                for (int k = 0; k < i; k++)
                                {
                                    
                                    //DateTime fr = fechas[k];
                                    if (fechas[j] == fechas[k])
                                    {
                                        cont++;
                                    }
                                    Console.WriteLine(cont);
                                    if (cont >= 2)
                                    {
                                        total++;
                                        Console.WriteLine("total");
                                        Console.WriteLine(total);
                                        cont = 1;

                                    }
                                    //cont = 0;
                                }

                               // cont = 0;
                                
                            }
                            int ok2 = ok;

                            if (total >= 1)
                            {
                                ok = ok -( total/2);
                                Console.WriteLine(ok);
                            }
                            

                            if (ok2 == 0)
                            {
                                MessageBox.Show("El pago se registrara desde el dia de HOY","INFO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                TPago = Fhoy.ToString();
                                Pagos pagos = new Pagos(CedulaPago, UsserID, Nombre, Apellido, TPago, 0);
                                pagos.Show();
                                
                            }
                            else
                            {
                                string mensaje = string.Format("EL pago se realizara \nDias descontados: {0} \nPor mora...",ok);
                                MessageBox.Show(mensaje,"INFO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                DateTime date = Fhoy.AddDays(-(ok));
                                TPago = date.ToString("yyyy-MM-dd");
                                Pagos pagos = new Pagos(CedulaPago, UsserID, Nombre, Apellido, TPago, 0);
                                pagos.Show();
                            }

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
