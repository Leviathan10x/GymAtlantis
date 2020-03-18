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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
//using System.Windows.Forms;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Int64 Us;
        public string Nom;
        public string Tip;
        bool Ac=false;
        public MainWindow()
        {
            InitializeComponent();
            
            

        }
        
        public void Logins()
        {
            try
            {
                string cnn = ConfigurationManager.ConnectionStrings["AtlantisGymBDConnectionString"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cnn))
                {
                    conexion.Open();
                    using(SqlCommand cmd = new SqlCommand("SELECT DOC_USUARIO, CONTRASEÑA FROM USUARIOS WHERE DOC_USUARIO='"+ textBox.Text +"'AND CONTRASEÑA ='"+ passwordBox.Password +"'",conexion))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        Ac = dr.Read();

                        if (Ac)
                        {
                            MessageBox.Show("BIENVENIDO","Login OK",MessageBoxButton.OK,MessageBoxImage.Information);
                            usuarion(Convert.ToInt64(textBox.Text));
                            
                        }
                        else
                        {
                            MessageBox.Show("Datos Incorrectos","ERROR LOGIN",MessageBoxButton.OK,MessageBoxImage.Stop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Logins();

            if (Ac)
            {
                
                Window1 h = new Window1(Convert.ToInt64(textBox.Text));
                h.Show();
                this.Close();
                
            }
            
        }

        public static UsserOn UsuarioSel(Int32 pID)
        {

            try
            {
                Int32 conten;
                conten = pID;
                string cnn = ConfigurationManager.ConnectionStrings["AtlantisGymBDConnectionString"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cnn))
                {
                    UsserOn pID_USUARIO = new UsserOn();
                    string comando = "SELECT * FROM USUARIOS  WHERE ID_USUARIO=@DOC";

                    SqlCommand cmd = new SqlCommand(comando, conexion);
                    cmd.Parameters.AddWithValue("@DOC", conten);
                    conexion.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    pID_USUARIO.ID_USUARIO = Convert.ToInt64(read["ID_USUARIO"].ToString());
                    pID_USUARIO.NOMBRE = read["NOMBRE"].ToString();
                    pID_USUARIO.TIPO_USUARIO = read["TIPO_USUARIO"].ToString();
                    
                    conexion.Close();
                    return pID_USUARIO;
                }
                

            }
            catch (Exception ex)
            {
                UsserOn pID_USUARIO = new UsserOn();
                MessageBox.Show(ex.ToString());
                MessageBox.Show(pID_USUARIO.ID_USUARIO.ToString());

                return null;
            }

        }
        
        public static UsserOn usuarion(Int64 pID)
        {
            UsserOn pUsuarioSele = new UsserOn();
            pUsuarioSele.ID_USUARIO = pID;
            //MessageBox.Show(pUsuarioSele.ID_USUARIO.ToString());
            return pUsuarioSele;
        }
        
    }
}
