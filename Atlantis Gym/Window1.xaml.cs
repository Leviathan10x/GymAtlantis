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
using System.Threading;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(Int64 UsActivo)
        {
            InitializeComponent();
            
            
            this.pUsActivo = UsActivo;
            button3.Visibility = Visibility.Collapsed;
            TipoUsuario();
            Usuario();
            Fechas();
            BotonVisible();
            dataGridRojo.ItemsSource=AlertaRoja(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            dataGridAmarillo.ItemsSource = AlertaAmarilla(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            dataGridVerde.ItemsSource = AlertaVerde(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            
            
            
        }
        public Int64 pUsActivo;
        public string pNombre;
        public DateTime FechaVmes;
        public DateTime FechaVsemana;
        public DateTime FechaVquincena;
        public DateTime FechaV2meses;
        public DateTime FechaV3meses;
        public DateTime FechaV6meses;
        public DateTime FechaVaño;
        public DateTime FechaHoy;

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            AgregarCliente agregarCliente = new AgregarCliente(pUsActivo);
            agregarCliente.Show();
            
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Buscarcliente buscarcliente = new Buscarcliente(pUsActivo,"");
            buscarcliente.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigUs configUs = new ConfigUs(pUsActivo);
            configUs.Show();
        }
        public void TipoUsuario()
        {
            try
            {
                string TipoUs = "";
                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM USUARIOS WHERE ID_USUARIO=@DOC";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", pUsActivo);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    TipoUs = read["TIPO_USUARIO"].ToString();

                }
                conectar.Cerrar();
                if(TipoUs== "ADMINISTRADOR            ")
                {
                    button3.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Administrar administrar = new Administrar();
            administrar.Show();
        }

        public void Usuario()
        {
            try
            {
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE FROM USUARIOS WHERE ID_USUARIO=@ID";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@ID", pUsActivo);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    pNombre = read["NOMBRE"].ToString();

                }
                conectar.Cerrar();
                labelNombre.Content = pNombre;
                labelDoc.Content = Convert.ToString(pUsActivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static List<Alerta> AlertaRoja(DateTime pFechaVmes, DateTime pFechaVquincena, DateTime pFechaVsemana, DateTime pFechaV2meses, DateTime pFechaV3meses, DateTime pFechaV6meses, DateTime pFechaVaño)
        {
            try
            {
                List<Alerta> Roja = new List<Alerta>();
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE,APELLIDO,NINGRESOS.FECHA_INICIO,INGRESOS.TIPO_INGRE FROM CLIENTES INNER JOIN  (select max(FECHA_INICIO)AS FECHA_INICIO,DOC_CLIENTE from INGRESOS group by DOC_CLIENTE) AS NINGRESOS  ON NINGRESOS.DOC_CLIENTE = CLIENTES.ID_CLIENTE INNER JOIN INGRESOS ON NINGRESOS.DOC_CLIENTE = INGRESOS.DOC_CLIENTE AND NINGRESOS.FECHA_INICIO=INGRESOS.FECHA_INICIO WHERE((NINGRESOS.FECHA_INICIO > @FECHA_V_MES AND NINGRESOS.FECHA_INICIO <=@FECHA_V_MES2 )AND(TIPO_INGRE = 'Mensual                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_QUIN AND NINGRESOS.FECHA_INICIO <= @FECHA_V_QUIN2 )AND(TIPO_INGRE = 'Quincenal                '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_SEMANA AND NINGRESOS.FECHA_INICIO <= @FECHA_V_SEMANA2 )AND(TIPO_INGRE = 'semanal                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_2MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_2MESES2 )AND(TIPO_INGRE = '2 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_3MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_3MESES2 )AND(TIPO_INGRE = '3 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_6MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_6MESES2 )AND(TIPO_INGRE = '6 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_AÑO AND NINGRESOS.FECHA_INICIO <= @FECHA_V_AÑO2 )AND(TIPO_INGRE = 'Anual                    '))";
                DateTime FechaVmes2 = pFechaVmes.AddDays(-5);
                Console.WriteLine(Convert.ToString(pFechaVmes));
                Console.WriteLine(Convert.ToString(FechaVmes2));
                DateTime FechaVquincena2 = pFechaVquincena.AddDays(-5);
                DateTime FechaVSemana2 = pFechaVsemana.AddDays(-5);
                DateTime FechaV2meses2 = pFechaV2meses.AddDays(-5);
                DateTime FechaV3meses2 = pFechaV3meses.AddDays(-5);
                DateTime FechaV6meses2 = pFechaV6meses.AddDays(-5);
                DateTime FechaVaño2 = pFechaVaño.AddDays(-5);
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@FECHA_V_MES2", pFechaVmes);
                cmd.Parameters.AddWithValue("@FECHA_V_MES", FechaVmes2);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA2", pFechaVsemana);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA", FechaVSemana2);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN2", pFechaVquincena);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN", FechaVquincena2);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES2", pFechaV2meses);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES", FechaV2meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES2", pFechaV3meses);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES", FechaV3meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES2", pFechaV6meses);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES", FechaV6meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO2", pFechaVaño);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO", FechaVaño2);

                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Alerta pAlerta = new Alerta();
                    pAlerta.Nombre = read["NOMBRE"].ToString();
                    pAlerta.Apellido = read["APELLIDO"].ToString();
                    DateTime FechaIni = Convert.ToDateTime(read["FECHA_INICIO"].ToString());
                    DateTime FechaVen;
                    string TipoPago = read["TIPO_INGRE"].ToString();
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
                    pAlerta.Fecha_Vencimiento = FechaVen.ToString("dd-MM-yyyy");
                    pAlerta.Tipo_Pago = TipoPago;
                    Roja.Add(pAlerta);
                }

                conectar.Cerrar();
                return Roja;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public void Fechas()
        {
            FechaHoy =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            FechaVmes = FechaHoy.AddMonths(-1);
            FechaVsemana = FechaHoy.AddDays(-7);
            FechaVquincena = FechaHoy.AddDays(-15);
            FechaV2meses = FechaHoy.AddMonths(-2);
            FechaV3meses = FechaHoy.AddMonths(-3);
            FechaV6meses = FechaHoy.AddMonths(-6);
            FechaVaño = FechaHoy.AddYears(-1);
        }

        public static List<Alerta> AlertaAmarilla(DateTime pFechaVmes, DateTime pFechaVquincena, DateTime pFechaVsemana, DateTime pFechaV2meses, DateTime pFechaV3meses, DateTime pFechaV6meses, DateTime pFechaVaño)
        {
            try
            {
                List<Alerta> Amarilla = new List<Alerta>();
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE,APELLIDO,NINGRESOS.FECHA_INICIO,INGRESOS.TIPO_INGRE FROM CLIENTES INNER JOIN  (select max(FECHA_INICIO)AS FECHA_INICIO,DOC_CLIENTE from INGRESOS group by DOC_CLIENTE) AS NINGRESOS  ON NINGRESOS.DOC_CLIENTE = CLIENTES.ID_CLIENTE INNER JOIN INGRESOS ON NINGRESOS.DOC_CLIENTE = INGRESOS.DOC_CLIENTE AND NINGRESOS.FECHA_INICIO=INGRESOS.FECHA_INICIO WHERE((NINGRESOS.FECHA_INICIO > @FECHA_V_MES AND NINGRESOS.FECHA_INICIO <=@FECHA_V_MES2 )AND(TIPO_INGRE = 'Mensual                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_QUIN AND NINGRESOS.FECHA_INICIO <= @FECHA_V_QUIN2 )AND(TIPO_INGRE = 'Quincenal                '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_SEMANA AND NINGRESOS.FECHA_INICIO <= @FECHA_V_SEMANA2 )AND(TIPO_INGRE = 'semanal                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_2MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_2MESES2 )AND(TIPO_INGRE = '2 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_3MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_3MESES2 )AND(TIPO_INGRE = '3 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_6MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_6MESES2 )AND(TIPO_INGRE = '6 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_AÑO AND NINGRESOS.FECHA_INICIO <= @FECHA_V_AÑO2 )AND(TIPO_INGRE = 'Anual                    '))";
                DateTime FechaVmes2 = pFechaVmes.AddDays(3);
                Console.WriteLine(Convert.ToString(pFechaVmes));
                Console.WriteLine(Convert.ToString(FechaVmes2));
                DateTime FechaVquincena2 = pFechaVquincena.AddDays(3);
                DateTime FechaVSemana2 = pFechaVsemana.AddDays(3);
                DateTime FechaV2meses2 = pFechaV2meses.AddDays(3);
                DateTime FechaV3meses2 = pFechaV3meses.AddDays(3);
                DateTime FechaV6meses2 = pFechaV6meses.AddDays(3);
                DateTime FechaVaño2 = pFechaVaño.AddDays(3);
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@FECHA_V_MES", pFechaVmes);
                cmd.Parameters.AddWithValue("@FECHA_V_MES2", FechaVmes2);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA", pFechaVsemana);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA2", FechaVSemana2);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN", pFechaVquincena);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN2", FechaVquincena2);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES", pFechaV2meses);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES2", FechaV2meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES", pFechaV3meses);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES2", FechaV3meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES", pFechaV6meses);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES2", FechaV6meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO", pFechaVaño);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO2", FechaVaño2);

                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Alerta pAlerta = new Alerta();
                    pAlerta.Nombre = read["NOMBRE"].ToString();
                    pAlerta.Apellido = read["APELLIDO"].ToString();
                    DateTime FechaIni = Convert.ToDateTime(read["FECHA_INICIO"].ToString());
                    DateTime FechaVen;
                    string TipoPago = read["TIPO_INGRE"].ToString();
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
                    pAlerta.Fecha_Vencimiento = FechaVen.ToString("dd-MM-yyyy");
                    pAlerta.Tipo_Pago = TipoPago;
                    Amarilla.Add(pAlerta);
                }

                conectar.Cerrar();
                return Amarilla;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public static List<Alerta> AlertaVerde(DateTime pFechaVmes, DateTime pFechaVquincena, DateTime pFechaVsemana, DateTime pFechaV2meses, DateTime pFechaV3meses, DateTime pFechaV6meses, DateTime pFechaVaño)
        {
            try
            {
                pFechaVmes = pFechaVmes.AddDays(3);
                pFechaVquincena = pFechaVquincena.AddDays(3);
                pFechaVsemana = pFechaVsemana.AddDays(3);
                pFechaV2meses = pFechaV2meses.AddDays(3);
                pFechaV3meses = pFechaV3meses.AddDays(3);
                pFechaV6meses = pFechaV6meses.AddDays(3);
                pFechaVaño = pFechaVaño.AddDays(3);
                List<Alerta> Verde = new List<Alerta>();
                Conexion conectar = new Conexion();
                string comando = "SELECT NOMBRE,APELLIDO,NINGRESOS.FECHA_INICIO,INGRESOS.TIPO_INGRE FROM CLIENTES INNER JOIN  (select max(FECHA_INICIO)AS FECHA_INICIO,DOC_CLIENTE from INGRESOS group by DOC_CLIENTE) AS NINGRESOS  ON NINGRESOS.DOC_CLIENTE = CLIENTES.ID_CLIENTE INNER JOIN INGRESOS ON NINGRESOS.DOC_CLIENTE = INGRESOS.DOC_CLIENTE AND NINGRESOS.FECHA_INICIO=INGRESOS.FECHA_INICIO WHERE((NINGRESOS.FECHA_INICIO > @FECHA_V_MES AND NINGRESOS.FECHA_INICIO <=@FECHA_V_MES2 )AND(TIPO_INGRE = 'Mensual                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_QUIN AND NINGRESOS.FECHA_INICIO <= @FECHA_V_QUIN2 )AND(TIPO_INGRE = 'Quincenal                '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_SEMANA AND NINGRESOS.FECHA_INICIO <= @FECHA_V_SEMANA2 )AND(TIPO_INGRE = 'semanal                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_2MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_2MESES2 )AND(TIPO_INGRE = '2 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_3MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_3MESES2 )AND(TIPO_INGRE = '3 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_6MESES AND NINGRESOS.FECHA_INICIO <= @FECHA_V_6MESES2 )AND(TIPO_INGRE = '6 Meses                  '))or((NINGRESOS.FECHA_INICIO > @FECHA_V_AÑO AND NINGRESOS.FECHA_INICIO <= @FECHA_V_AÑO2 )AND(TIPO_INGRE = 'Anual                    '))";
                DateTime FechaVmes2 = pFechaVmes.AddDays(4);
                Console.WriteLine(Convert.ToString(pFechaVmes));
                Console.WriteLine(Convert.ToString(FechaVmes2));
                DateTime FechaVquincena2 = pFechaVquincena.AddDays(4);
                DateTime FechaVSemana2 = pFechaVsemana.AddDays(4);
                DateTime FechaV2meses2 = pFechaV2meses.AddDays(4);
                DateTime FechaV3meses2 = pFechaV3meses.AddDays(4);
                DateTime FechaV6meses2 = pFechaV6meses.AddDays(4);
                DateTime FechaVaño2 = pFechaVaño.AddDays(4);
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@FECHA_V_MES", pFechaVmes);
                cmd.Parameters.AddWithValue("@FECHA_V_MES2", FechaVmes2);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA", pFechaVsemana);
                cmd.Parameters.AddWithValue("@FECHA_V_SEMANA2", FechaVSemana2);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN", pFechaVquincena);
                cmd.Parameters.AddWithValue("@FECHA_V_QUIN2", FechaVquincena2);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES", pFechaV2meses);
                cmd.Parameters.AddWithValue("@FECHA_V_2MESES2", FechaV2meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES", pFechaV3meses);
                cmd.Parameters.AddWithValue("@FECHA_V_3MESES2", FechaV3meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES", pFechaV6meses);
                cmd.Parameters.AddWithValue("@FECHA_V_6MESES2", FechaV6meses2);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO", pFechaVaño);
                cmd.Parameters.AddWithValue("@FECHA_V_AÑO2", FechaVaño2);

                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Alerta pAlerta = new Alerta();
                    pAlerta.Nombre = read["NOMBRE"].ToString();
                    pAlerta.Apellido = read["APELLIDO"].ToString();
                    DateTime FechaIni = Convert.ToDateTime(read["FECHA_INICIO"].ToString());
                    DateTime FechaVen;
                    string TipoPago = read["TIPO_INGRE"].ToString();
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
                    pAlerta.Fecha_Vencimiento = FechaVen.ToString("dd-MM-yyyy");
                    pAlerta.Tipo_Pago = TipoPago;
                    Verde.Add(pAlerta);
                }

                conectar.Cerrar();
                return Verde;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            CargarDatos cargarDatos = new CargarDatos(pUsActivo);
            cargarDatos.Show();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            dataGridRojo.ItemsSource = AlertaRoja(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            dataGridAmarillo.ItemsSource = AlertaAmarilla(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            dataGridVerde.ItemsSource = AlertaVerde(FechaVmes, FechaVquincena, FechaVsemana, FechaV2meses, FechaV3meses, FechaV6meses, FechaVaño);
            BotonVisible();
        }
        public void BotonVisible()
        {
            try
            {
                int valor=0;
                Conexion conectar = new Conexion();
                string comando = "SELECT * FROM CargarRegistros WHERE ID=1";
                conectar.Abrir();
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    valor =Convert.ToInt32( read["VALOR"].ToString());

                }
                conectar.Cerrar();
                if (valor == 1)
                {
                    button4.Visibility = Visibility.Visible;
                }
                else
                {
                    button4.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void CerrarSesion()
        {
            try
            {
                
                MessageBoxResult op = MessageBox.Show("¿Desea cerrar Sesión?", "CERRA SESION", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (op == MessageBoxResult.Yes)
                {
                    CerrarTurno cerrarTurno = new CerrarTurno(pUsActivo,pNombre);
                    cerrarTurno.Show();
                    this.Close();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            CerrarSesion();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventas = new Ventas(pUsActivo, pNombre);
            ventas.Show();
        }
        private void load (object sender, RoutedEventArgs e)
        {
            CargarInventarios cargarInventarios = new CargarInventarios();
            cargarInventarios.Show();
            RegisIngresos regisIngresos = new RegisIngresos(pUsActivo);
            regisIngresos.Show();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            RegisIngresos regisIngresos = new RegisIngresos(pUsActivo);
            regisIngresos.Show();
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            VentasZN ventasZN = new VentasZN(pUsActivo);
            ventasZN.Show();
        }
    }
}
