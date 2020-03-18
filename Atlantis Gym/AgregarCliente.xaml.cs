using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;
//using System.Windows.Forms;
using GriauleFingerprintLibrary;
using GriauleFingerprintLibrary.Exceptions;
using RestSharp;


namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para AgregarCliente.xaml
    /// </summary>
    public partial class AgregarCliente : Window
    {
        

        public AgregarCliente(Int64 UsActivo)
        {
            InitializeComponent();
            this.pUsActivo = UsActivo;
            
            //Fingerprint = new FingerprintCore();
            //Fingerprint.onStatus += new StatusEventHandler(Fingerprint_onStatus);
            //Fingerprint.onFinger += new FingerEventHandler(Fingerprint_onFinger);
            //Fingerprint.onImage += new ImageEventHandler(Fingerprint_onImage);
        }
       // public bool InvokeRequired { get; }
        private FingerprintCore Fingerprint;
        private GriauleFingerprintLibrary.DataTypes.FingerprintRawImage rawImage;
        GriauleFingerprintLibrary.DataTypes.FingerprintTemplate Template;

        void Fingerprint_onStatus(object source, GriauleFingerprintLibrary.Events.StatusEventArgs se)
        {
            if(se.StatusEventType==GriauleFingerprintLibrary.Events.StatusEventType.SENSOR_PLUG)
            {
                Fingerprint.StartCapture(source.ToString());
            }
            else
            {
                Fingerprint.StopCapture(source);
            }
        }

        

        public UsserOn IdActivo { get; set; }
        public Int64 pUsActivo;
        public Int32 pPrecio;
        int ok;
        public bool Des = false;
        public Int32 ValDes = 0;
        public DateTime fechaven1;
        public void CrearCliente()
        {
            try
            {
                string FechaHoy = DateTime.Now.ToString("yyyy-MM-dd");
                string cnn = ConfigurationManager.ConnectionStrings["AtlantisGymBDConnectionString"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cnn))
                {
                    UsserOn pID_USUARIO = new UsserOn();
                    
                    string comando = "INSERT INTO CLIENTES (ID_CLIENTE, DOC_CLIENTE, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_INGRESO, GENERO, ESTATURA_IN) VALUES(@ID, @DOC, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHA, @GENERO, @ESTATURA) ";
                    string comando2 ="Insert into INGRESOS (DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL,ID_USUARIO,FECHA_INICIO) VALUES (@ID,@TIPO_INGRE,@FECHA,@TOTAL,@ID_USUARIO,@FECHA)";
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(comando, conexion);
                    SqlCommand cmd2 = new SqlCommand(comando2, conexion);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(textDocumento.Text));
                    cmd.Parameters.AddWithValue("@DOC", comboTipoDoc.Text);
                    cmd.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                    cmd.Parameters.AddWithValue("@APELLIDO", textApellido.Text);
                    cmd.Parameters.AddWithValue("@TELEFONO", textTelefono.Text);
                    cmd.Parameters.AddWithValue("@DIRECCION", textDireccion.Text);
                    cmd.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(FechaHoy));
                    cmd.Parameters.AddWithValue("@GENERO", comboGenero.Text);
                    cmd.Parameters.AddWithValue("@ESTATURA", Convert.ToInt32(textEstatura.Text));

                    cmd2.Parameters.AddWithValue("@ID", Convert.ToInt32(textDocumento.Text));
                    cmd2.Parameters.AddWithValue("@TIPO_INGRE", comboPago.Text);
                    cmd2.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(FechaHoy));
                    cmd2.Parameters.AddWithValue("@TOTAL", Convert.ToInt32(labelTotal.Content));
                    cmd2.Parameters.AddWithValue("@ID_USUARIO", pUsActivo);

                    
                    
                    int FA1 = cmd.ExecuteNonQuery();
                    int FA2 = cmd2.ExecuteNonQuery();
                    if (FA1==1 & FA2 == 1)
                    {
                        DateTime fechaven = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        System.Windows.MessageBox.Show("CLIENTE CREADO CORRECTAMENTE");
                        ok = 1;
                        int alf = 1;
                        if (textTelefono.Text != "")
                        {
                            switch (comboPago.SelectedIndex)
                            {
                                case 0:
                                    fechaven=fechaven.AddMonths(1);
                                    break;
                                case 1:
                                    fechaven=fechaven.AddDays(15);
                                    Console.WriteLine(fechaven);
                                    break;
                                case 2:
                                    fechaven = fechaven.AddDays(7);
                                    break;
                                case 3:
                                    fechaven = fechaven.AddDays(1);
                                    alf = 4;
                                    break;
                                case 4:
                                    fechaven = fechaven.AddMonths(2);
                                    break;
                                case 5:
                                    fechaven = fechaven.AddMonths(3);
                                    break;
                                case 6:
                                    fechaven = fechaven.AddMonths(6);

                                    break;
                                case 7:
                                    fechaven = fechaven.AddYears(1); 
                                    break;
                                default:
                                    alf = 4;
                                    break;

                            }
                            fechaven1 = fechaven;
                            Console.WriteLine(fechaven1);
                            enviarSMS(Convert.ToInt32(labelTotal.Content),fechaven,alf);
                        }
                        else
                        {
                            MessageBox.Show("Para el envio de mensajes tiene que registrar un numero de celular.", "STOP", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Error al ingresar datos");
                        ok = 0;
                    }
                    
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void enviarSMS(Int32 total, DateTime fecha, int palfa)
        {
            try
            {

                string Cliente, Header, Cpais, ky, Ncliente;
                ConexionSMS conexionSMS = new ConexionSMS();
                conexionSMS.Abrir();

                string comando = "SELECT * FROM CONFISMS";
                SqlCommand cmd = new SqlCommand(comando, conexionSMS.Conectarbd);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    Ncliente = reader["NroCLIENTE"].ToString();
                    Cliente = reader["CLIENTE"].ToString();
                    Header = reader["HEADER"].ToString();
                    ky = reader["KY"].ToString();
                    Cpais = reader["CODIGOPAIS"].ToString();
                    Console.WriteLine(Cliente);

                    System.Uri Url = new System.Uri("http://www.google.com/");
                    System.Net.WebRequest WebRequest;
                    WebRequest = System.Net.WebRequest.Create(Url);
                    System.Net.WebResponse objResp;
                    Boolean ONline;
                    try
                    {
                        objResp = WebRequest.GetResponse();
                        //result = "Su dispositivo está correctamente conectado a internet";
                        Console.WriteLine("Online");
                        objResp.Close();
                        WebRequest = null;
                        ONline = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Offline");
                        //result = "Error al intentar conectarse a internet " + ex.Message;
                        WebRequest = null;
                        ONline = false;
                    }
                    string mensaje = string.Format("Estimado cliente se ha registrado su pago por ${0}, Su suscripcion vence {1}. Le agradecemos su fidelidad.\nAtlantis GYM", total,fechaven1.ToString("dd/MM/yyyy"));
                    if (palfa != 4)
                    {
                        if (ONline)
                        {

                            var client = new RestClient("https://www.onurix.com/api/v1/send-sms");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("content-type", "application/x-www-form-urlencoded");
                            request.AddParameter("key", ky);
                            request.AddParameter("client", Ncliente);
                            request.AddParameter("phone", textTelefono.Text);
                            request.AddParameter("sms", mensaje);
                            request.AddParameter("country-code", "CO");
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);

                            Console.WriteLine(response.StatusCode);
                            if (response.StatusCode.ToString() == "OK")
                            {
                                MessageBox.Show("SMS enviado exitosamente", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                Console.WriteLine("Error");
                                MessageBox.Show("El mensaje no se pudo enviar... \nVerifica el numero de telefono o su saldo...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                logsms(mensaje,Convert.ToInt64( textDocumento.Text));
                            }

                        }
                        else
                        {
                            MessageBox.Show("No tiene conexion a Internet para enviar mensajes.\nLos mensajes se enviaran Cuando se reestablezca el servicio.", "Error conexion", MessageBoxButton.OK, MessageBoxImage.Error);
                            logsms(mensaje, Convert.ToInt64(textDocumento.Text));
                        }
                    }
                }


                conexionSMS.Cerrar();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void logsms(string sms, Int64 id)
        {
            try
            {
                ConexionSMS conexionSMS1 = new ConexionSMS();
                conexionSMS1.Cerrar();
                conexionSMS1.Abrir();

                string comando1 = "INSERT INTO SMSLOG (Mensaje,ID) VALUES(@SMS,@ID)";
                SqlCommand cmd1 = new SqlCommand(comando1, conexionSMS1.Conectarbd);
                cmd1.Parameters.AddWithValue("@SMS", sms);
                cmd1.Parameters.AddWithValue("@ID", id);
                int x = cmd1.ExecuteNonQuery();
                if (x == 1)
                {
                    MessageBox.Show("El mensaje se enviara cuando se solucionen los problemas...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El mensaje no sera enviado...", "Error log", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                conexionSMS1.Cerrar();
                conexionSMS1.Abrir();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (textDocumento.Text != "")
            {
                CapturarHuella capturarHuella = new CapturarHuella(Convert.ToInt64(textDocumento.Text));
                capturarHuella.Show();
            }
            else
            {
                MessageBox.Show("El campo Documento no puede esta vacio...", "ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

            CrearCliente();
            if (Des)
            {
                GuardarDes();
            }
            if (ok == 1)
            {
                textDocumento.Text = "";
                textNombre.Text = "";
                textApellido.Text = "";
                textTelefono.Text = "";
                textDireccion.Text = "";
                labelTotal.ContentStringFormat = "";
                textEstatura.Text = "";
            }
        }
        public void VPrecio()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT PRECIO, NOMBRE FROM PRODUCTOS WHERE ID_PRODUCTO=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", pPrecio);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    labelTotal.Content = read["PRECIO"].ToString();
                    Console.WriteLine(read["PRECIO"].ToString());
                }
                conectar.Cerrar();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void ComboPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sel = comboPago.SelectedItem.ToString();
            Console.WriteLine(sel);
            if (sel == "System.Windows.Controls.ListBoxItem: Mensual")
            {
                
                pPrecio = 1;
                //fechaven.AddMonths(1);

            }
            else
            {
                if (sel == "System.Windows.Controls.ListBoxItem: Quincenal")
                {
                    
                    pPrecio = 2;
                    //fechaven.AddDays(15);
                }
                else
                {
                    if (sel == "System.Windows.Controls.ListBoxItem: Semanal")
                    {
                        
                        pPrecio = 3;
                        //fechaven.AddDays(7);
                    }
                    else
                    {
                        if (sel == "System.Windows.Controls.ListBoxItem: Diario")
                        {
                            
                            pPrecio = 4;
                           // fechaven.AddDays(1);
                        }
                        else
                        {
                            if (sel == "System.Windows.Controls.ListBoxItem: 2 Meses")
                            {
                                
                                pPrecio = 5;
                                //fechaven.AddMonths(2);
                            }
                            else
                            {
                                if (sel == "System.Windows.Controls.ListBoxItem: 3 Meses")
                                {
                                    
                                    pPrecio = 6;
                                    //fechaven.AddMonths(3);
                                }
                                else
                                {
                                    if (sel == "System.Windows.Controls.ListBoxItem: 6 Meses")
                                    {
                                        
                                        pPrecio = 7;
                                       // fechaven.AddMonths(6);
                                    }
                                    else
                                    {
                                        if (sel == "System.Windows.Controls.ListBoxItem: Anual")
                                        {
                                            
                                            pPrecio = 8;
                                            //fechaven.AddYears(1);
                                        }
                                        else
                                        {
                                            pPrecio = 0;
                                           // fechaven = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            VPrecio();
        }

        public static bool CodigoOk(string pCodigo)
        {
            try
            {
                bool ok;
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT CODIGO FROM CODIGOS_DES WHERE CODIGO=@CODIGO";
                SqlCommand cmd = new SqlCommand(comando,conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", pCodigo);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                }
                conectar.Cerrar();
                return ok;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public void Descuento()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT DESCRIPCION,VALOR FROM CODIGOS_DES WHERE CODIGO=@CODIGO";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", textDescuento.Text);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    labelDescrip.Content = read["DESCRIPCION"].ToString();
                    labelTotal.Content = (Convert.ToInt32(labelTotal.Content) - (Convert.ToInt32(read["VALOR"].ToString())));
                    ValDes = Convert.ToInt32(read["VALOR"].ToString());

                }
                conectar.Cerrar();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static bool EstadoOk(string pCodigo)
        {
            try
            {
                bool a;
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "SELECT ESTADO FROM CODIGOS_DES WHERE CODIGO=@CODIGO";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@CODIGO", pCodigo);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    int ok =Convert.ToInt32( read["ESTADO"].ToString());
                    if (ok == 1)
                    {
                        a = true;
                    }
                    else
                    {
                        a = false;
                    }
                }
                else
                {
                    a = false;
                }
                conectar.Cerrar();
                return a;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public void GuardarDes()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "INSERT INTO Descuentos (Valor,Codigo,ID_USUARIO,FECHA,ID_CLIENTE) VALUES(@VALOR,@CODIGO,@ID_USUARIO,@FECHA,@ID_CLIENTE)";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@VALOR", ValDes);
                cmd.Parameters.AddWithValue("@CODIGO", textDescuento.Text);
                cmd.Parameters.AddWithValue("@ID_USUARIO", pUsActivo);
                cmd.Parameters.AddWithValue("@FECHA", DateTime.Now);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", textDocumento.Text);

                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Descuento creado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al crear el descuento", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool com = CodigoOk(textDescuento.Text);
            if (com)
            {
                bool com1 = EstadoOk(textDescuento.Text);
                if (com1)
                {
                    if (labelTotal.Content.ToString() == "")
                    {
                        MessageBox.Show("Tiene que seleccionar primero un tipo de pago", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        Descuento();
                        Des = true;
                        MessageBox.Show("Descuento aplicado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Este codigo esta inactivo", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                MessageBox.Show("Este codigo no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Button1Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
