using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using RestSharp;
using Microsoft.VisualBasic;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Pagos.xaml
    /// </summary>
    public partial class Pagos : Window
    {
        public Int64 NroDoc,ID_producto;
        public Int64 pUsActivo;
        public string pNombre, pApellido,pFechaVen,TipoIngreso;
        public int pPrecio;
        public Int32 ValDes = 0;
        public bool Des = false;
        public Int64 TEL;
        public DateTime fV;

        private void ComboTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sel = comboTipoPago.SelectedItem.ToString();
            Console.WriteLine(sel);
            if (sel == "System.Windows.Controls.ListBoxItem: Mensual")
            {

                pPrecio = 1;
                fV = fV.AddMonths(1);
            }
            else
            {
                if (sel == "System.Windows.Controls.ListBoxItem: Quincenal")
                {

                    pPrecio = 2;
                    fV = fV.AddDays(15);
                }
                else
                {
                    if (sel == "System.Windows.Controls.ListBoxItem: Semanal")
                    {

                        pPrecio = 3;
                        fV = fV.AddDays(7);
                    }
                    else
                    {
                        if (sel == "System.Windows.Controls.ListBoxItem: Diario")
                        {

                            pPrecio = 4;
                            fV = fV.AddDays(1);
                        }
                        else
                        {
                            if (sel == "System.Windows.Controls.ListBoxItem: 2 Meses")
                            {

                                pPrecio = 5;
                                fV = fV.AddMonths(2);
                            }
                            else
                            {
                                if (sel == "System.Windows.Controls.ListBoxItem: 3 Meses")
                                {

                                    pPrecio = 6;
                                    fV = fV.AddMonths(3);
                                }
                                else
                                {
                                    if (sel == "System.Windows.Controls.ListBoxItem: 6 Meses")
                                    {

                                        pPrecio = 7;
                                        fV = fV.AddMonths(6);
                                    }
                                    else
                                    {
                                        if (sel == "System.Windows.Controls.ListBoxItem: Anual")
                                        {

                                            pPrecio = 8;
                                            fV = fV.AddYears(1);
                                        }
                                        else
                                        {
                                            pPrecio = 0;

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
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
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
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
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
                    int ok = Convert.ToInt32(read["ESTADO"].ToString());
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TextDescuento_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            bool com = CodigoOk(textDescuento.Text);
            if (com)
            {
                bool com1 = EstadoOk(textDescuento.Text);
                if (com1)
                {
                    if (comboTipoPago.SelectedIndex <0 )
                    {
                        System.Windows.MessageBox.Show("Tiene que seleccionar primero un tipo de pago", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        Descuento();
                        Des = true;
                        System.Windows.MessageBox.Show("Descuento aplicado con exito", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Este codigo esta inactivo", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Este codigo no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public Pagos(Int64 Documento,Int64 UsActivo,string Nombre,string Apellido,string FechaVen,int Precio)
        {
            InitializeComponent();
            this.NroDoc = Documento;
            this.pUsActivo = UsActivo;
            this.pNombre = Nombre;
            this.pApellido = Apellido;
            this.pFechaVen = FechaVen;
            this.pPrecio = Precio;
            this.fV = Convert.ToDateTime(FechaVen);
            textDocumento.Text = Convert.ToString(Documento);
            textNombre.Text = Nombre;
            textApellido.Text = Apellido;
            LabelFechaVen.Content = FechaVen;
            VPrecio();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Atlantis_GYM_Loaded(object sender, RoutedEventArgs e)
        {
            TEL = CargarTel(Convert.ToInt64( NroDoc));


            MessageBoxResult rest = MessageBox.Show(string.Format("¿Desea Cambiar el numero actual: {0}?", TEL), "¿?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rest == MessageBoxResult.Yes)
            {
                TEL = ActuTel(NroDoc);
            }
            
        }

        private static Int64 ActuTel(Int64 id)
        {
            try
            {
                Int64 Ntel = Convert.ToInt64(Interaction.InputBox("Digite El nuevo numero:","Nuevo telefono"));
                Conexion conexion = new Conexion();
                conexion.Abrir();
                string comando = "UPDATE CLIENTES SET TELEFONO=@TEL WHERE ID_CLIENTE=@ID";
                SqlCommand cmd = new SqlCommand(comando, conexion.Conectarbd);
                cmd.Parameters.AddWithValue("@TEL", Ntel);
                cmd.Parameters.AddWithValue("@ID", id);
                int x = cmd.ExecuteNonQuery();

                if (x == 1)
                {
                    MessageBox.Show("Su numero fue actualizado correctamente", "OK", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
                else
                {
                    MessageBox.Show("Erro al actualizar el numero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                conexion.Cerrar();
                return Ntel;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboTipoPago.SelectedIndex != -1)
            {
                System.Windows.Forms.DialogResult Op;
                Op = System.Windows.Forms.MessageBox.Show("Cofirme Pago", "Confirmar", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);
                if (Op == System.Windows.Forms.DialogResult.OK)
                {

                    try
                    {

                        DateTime fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        Conexion Conectar = new Conexion();
                        Conectar.Abrir();
                        string comando = "INSERT INTO INGRESOS (DOC_CLIENTE,TIPO_INGRE,FECHA_INGRE,TOTAL,ID_USUARIO,FECHA_INICIO) VALUES(@DOC,@TIPO_INGRESO,@FECHA_INGRESO,@TOTAL,@ID_USUARIO,@FECHA_INICIO)";
                        SqlCommand cmd = new SqlCommand(comando, Conectar.Conectarbd);
                        cmd.Parameters.AddWithValue("@DOC", NroDoc);
                        cmd.Parameters.AddWithValue("@TIPO_INGRESO", comboTipoPago.Text);
                        cmd.Parameters.AddWithValue("@FECHA_INGRESO", fecha);
                        cmd.Parameters.AddWithValue("@TOTAL", Convert.ToInt32(labelTotal.Content));
                        cmd.Parameters.AddWithValue("@ID_USUARIO", pUsActivo);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", Convert.ToDateTime(pFechaVen));
                        int ok = cmd.ExecuteNonQuery();
                        if (ok == 1)
                        {
                            System.Windows.MessageBox.Show("Pago Existoso");

                            if (TEL != 0)
                            {
                                Console.WriteLine(TEL);
                                enviarSMS(Convert.ToInt32(labelTotal.Content),fV);
                            }

                        }
                        else
                        {
                            System.Windows.MessageBox.Show("ERROR");
                        }
                        Conectar.Cerrar();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.ToString());
                    }

                    if (Des)
                    {
                        GuardarDes();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No se pudo completar el pago", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Tiene que seleccionar el tipo de pago", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public void enviarSMS(Int32 total,DateTime fecha)
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
                    Cliente= reader["CLIENTE"].ToString();
                    Header= reader["HEADER"].ToString();
                    ky= reader["KY"].ToString();
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
                    string mensaje = string.Format("Estimado cliente se ha registrado su pago por ${0}, Su suscripcion vence {1}. Le agradecemos su fidelidad.\nAtlantis GYM", total, fecha.ToString("dd/MM/yyyy"));
                    if (pPrecio != 4 && pPrecio !=0)
                    {
                        if (ONline)
                        {

                            var client = new RestClient("https://www.onurix.com/api/v1/send-sms");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("content-type", "application/x-www-form-urlencoded");
                            request.AddParameter("key", ky);
                            request.AddParameter("client", Ncliente);
                            request.AddParameter("phone", TEL);
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
                                logsms(mensaje, NroDoc);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No tiene conexion a Internet para enviar mensajes.\nLos mensajes se enviaran Cuando se reestablezca el servicio.", "Error conexion", MessageBoxButton.OK, MessageBoxImage.Error);
                            logsms(mensaje, NroDoc);
                        }
                    }
                }
                

                conexionSMS.Cerrar();



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void logsms(string sms,Int64 id)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static Int64 CargarTel(Int64 id)
        {
            try
            {
                Conexion conexion = new Conexion();
                conexion.Abrir();
                string comando = "SELECT TELEFONO FROM CLIENTES WHERE ID_CLIENTE=@ID";
                SqlCommand cmd = new SqlCommand(comando, conexion.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                Int64 tel=0;
                string t=null;
                if (reader.Read())
                {
                    t=reader["TELEFONO"].ToString();
                    Console.WriteLine(t);
                }

                if (t != "                    ")
                {
                    tel = Convert.ToInt64(t);
                }
                else
                {
                    MessageBox.Show("Este usuario no tiene un numero registrado", "STOP", MessageBoxButton.OK, MessageBoxImage.Stop);
                    
                }

                Console.WriteLine(tel);

                conexion.Cerrar();
                return tel;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
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
                    
                }
                conectar.Cerrar();

            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
