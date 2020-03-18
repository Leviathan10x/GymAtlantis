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
using System.Configuration;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Buscarcliente.xaml
    /// </summary>
    public partial class Buscarcliente : Window
    {
        public string pTPago="";
        public Int64 pUsActivo;
        public int TOTAL=0;
        public DateTime pFechaVen;
        public Buscarcliente(Int64 UsActivo,string doc)
        {
            
            InitializeComponent();
            this.pUsActivo = UsActivo;
            NroDocumento.Text = doc;
            button6.Visibility = Visibility.Collapsed;
            button6_Copy.Visibility = Visibility.Collapsed;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexion Conectar = new Conexion();
                Conectar.Abrir();
                string comando = "SELECT * FROM CLIENTES WHERE ID_CLIENTE=@DOC";


                SqlCommand cmd = new SqlCommand(comando, Conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt64(NroDocumento.Text));
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    string Nombre = read["NOMBRE"].ToString();
                    string Apellido = read["APELLIDO"].ToString();
                    string Telefono = read["TELEFONO"].ToString();
                    string Direccion = read["DIRECCION"].ToString();
                    string Genero = read["GENERO"].ToString();
                    string TipoDoc = read["DOC_CLIENTE"].ToString();
                    string Estatura = read["ESTATURA_IN"].ToString();
                    textNombre.Text = Nombre;
                    textApellido.Text = Apellido;
                    textDireccion.Text = Direccion;
                    textTelefono.Text = Telefono;
                    textGenero.Text = Genero;
                    textTipoDoc.Text = TipoDoc;
                    textEstatura.Text = Estatura;
                }
                Conectar.Cerrar();
                string FechaHoy = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime FechaH = Convert.ToDateTime(FechaHoy);
                string comando2 = "SELECT * FROM INGRESOS WHERE FECHA_INGRE = (SELECT MAX(FECHA_INGRE) FROM INGRESOS WHERE DOC_CLIENTE = @DOC) AND DOC_CLIENTE = @DOC";
                Conectar.Abrir();
                SqlCommand cmd2 = new SqlCommand(comando2,Conectar.Conectarbd);
                cmd2.Parameters.AddWithValue("@DOC", Convert.ToInt64(NroDocumento.Text));
                SqlDataReader LFecha = cmd2.ExecuteReader();
                while (LFecha.Read())
                {
                    string FechaUlt = LFecha["FECHA_INICIO"].ToString();
                    string FechaUlti2 = LFecha["FECHA_INGRE"].ToString();
                    string TipoPago = LFecha["TIPO_INGRE"].ToString();
                    //textUlyimoPago.Text = LFecha["FECHA_INGRE"].ToString();
                    DateTime fecha = Convert.ToDateTime(FechaUlt);
                    textTipoPago.Text = TipoPago;
                    textUlyimoPago.Text = FechaUlti2;
                    DateTime FechaVen;
                    Console.WriteLine(TipoPago);
                    if (TipoPago == "Mensual                  ")
                    {
                        FechaVen = fecha.AddMonths(1);
                        TOTAL = 1;

                    }
                    else
                    {
                        if (TipoPago == "Quincenal                ")
                        {
                            FechaVen = fecha.AddDays(15);
                            TOTAL = 2;
                        }
                        else
                        {
                            if (TipoPago == "Semanal                  ")
                            {
                                FechaVen = fecha.AddDays(7);
                                TOTAL = 3;
                            }
                            else
                            {
                                if (TipoPago == "Diario                   ")
                                {
                                    FechaVen = fecha.AddDays(1);
                                    TOTAL = 4;
                                }
                                else
                                {
                                    if(TipoPago== "2 Meses                  ")
                                    {
                                        FechaVen = fecha.AddMonths(2);
                                        TOTAL = 5;
                                    }
                                    else
                                    {
                                        if(TipoPago== "3 Meses                  ")
                                        {
                                            FechaVen = fecha.AddMonths(3);
                                            TOTAL = 6;
                                        }
                                        else
                                        {
                                            if(TipoPago== "6 Meses                  ")
                                            {
                                                FechaVen = fecha.AddMonths(3);
                                                TOTAL = 7;
                                            }
                                            else
                                            {
                                                if(TipoPago== "Anual                    ")
                                                {
                                                    FechaVen = fecha.AddYears(1);
                                                    TOTAL = 8;
                                                }
                                                else
                                                {
                                                    FechaVen = fecha;

                                                }
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                    pTPago = FechaVen.ToString("yyyy-MM-dd");

                    if (FechaVen > FechaH)
                    {
                        textEstado.Text = "ACTIVO";
                    }
                    else
                    {
                        textEstado.Text = "VENCIDO";
                    }
                    Console.WriteLine(FechaVen);
                    Console.WriteLine(FechaH);
                }
                Conectar.Cerrar();

            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.ToString());
            }

            try
            {
                ConexionHuella conexionHuella = new ConexionHuella();
                conexionHuella.Abrir();
                string comando = "SELECT * FROM HUELLASCLIENTES WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(comando, conexionHuella.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt64(NroDocumento.Text));
                SqlDataReader reader = cmd.ExecuteReader();
                int ok = 0;
                while (reader.Read())
                {
                    ok = ok + 1;

                }
                if (ok == 1)
                {
                    button6.Visibility = Visibility.Visible;
                    button6_Copy.Visibility=Visibility.Collapsed;
                }
                else
                {
                    button6_Copy.Visibility=Visibility.Visible;
                    button6.Visibility=Visibility.Collapsed;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "UPDATE CLIENTES SET NOMBRE=@NOMBRE, APELLIDO=@APELLIDO, DIRECCION=@DIRECCION, TELEFONO=@TELEFONO WHERE ID_CLIENTE=@DOC ";
                SqlCommand cmd = new SqlCommand(comando,conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@NOMBRE", textNombre.Text);
                cmd.Parameters.AddWithValue("@APELLIDO", textApellido.Text);
                cmd.Parameters.AddWithValue("@TELEFONO", textTelefono.Text);
                cmd.Parameters.AddWithValue("@DIRECCION", textDireccion.Text);
                cmd.Parameters.AddWithValue("@DOC", Convert.ToInt64(NroDocumento.Text));

                int ok = cmd.ExecuteNonQuery();

                if (ok == 1)
                {
                    MessageBox.Show("OPERACION EXITOSA");
                }
                else
                {
                    MessageBox.Show("ERROR");
                }

                conectar.Cerrar();

            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textEstado.Text == "VENCIDO")
            {
                MessageBoxResult opc =MessageBox.Show("¿El Cliente retoma Servicio?", "Servicio Retomado o en mora", MessageBoxButton.YesNo, MessageBoxImage.Question,MessageBoxResult.No);
                if (opc == MessageBoxResult.No)
                {
                    MessageBoxResult op = MessageBox.Show("La fecha de la suscripcion arranca \ndesde el dia del vencimiento anterior", "Confirmar proceso", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (op == MessageBoxResult.Yes)
                    {
                        Pagos p = new Pagos(Convert.ToInt64(NroDocumento.Text), pUsActivo, textNombre.Text, textApellido.Text, pTPago, TOTAL);
                        p.Show();
                    }
                    else
                    {
                        MessageBox.Show("El pago no continua", "Pago interrumpido", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else
                {
                    MessageBoxResult op = MessageBox.Show("La fecha de la suscripcion arranca \ndesde el dia de HOY", "Confirmar proceso", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (op == MessageBoxResult.Yes)
                    {
                        Pagos p = new Pagos(Convert.ToInt64(NroDocumento.Text), pUsActivo, textNombre.Text, textApellido.Text, DateTime.Now.ToString("yyyy-MM-dd"), TOTAL);
                        p.Show();
                    }
                    else
                    {
                        MessageBox.Show("El pago no continua", "Pago interrumpido", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
            }
            else
            {

                MessageBoxResult op = MessageBox.Show("No esta Vencido \n¿Desea pagar?", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                if (op == MessageBoxResult.OK)
                {
                    Pagos p = new Pagos(Convert.ToInt64(NroDocumento.Text), pUsActivo, textNombre.Text, textApellido.Text, pTPago,TOTAL);
                    p.Show();
                }
            }

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textNombre.Text != null)
                {
                    Medidas medidas = new Medidas(textNombre.Text, Convert.ToInt64(NroDocumento.Text), Convert.ToInt64(textEstatura.Text));
                    medidas.Show();
                }
                else
                {
                    MessageBox.Show("Primero debe seleccionar el cliente", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Consultarxnombre consultarxnombre = new Consultarxnombre(pUsActivo);
            consultarxnombre.Show();
            this.Close();
            
            
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("El usuario ya tiene una huella registrada...\nSi desea cambiarla contacte con el administrador...","Informacion",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Button6_Copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vamos a crear la huella para el usuario...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("Recuerde terner la piel de la huella limpia e hidratada...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            CapturarHuella capturarHuella = new CapturarHuella(Convert.ToInt64(NroDocumento.Text));
            capturarHuella.Show();
        }
    }
}
