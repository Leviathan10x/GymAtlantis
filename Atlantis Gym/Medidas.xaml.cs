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

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Medidas.xaml
    /// </summary>
    public partial class Medidas : Window
    {
        public string Nombre;
        public Int64 CliSelect;
        public Int64 Estatura;
        public double IndiceMasa;
        public Medidas(string pNombre,Int64 pClieSelect,Int64 pEstatura)
        {
            InitializeComponent();

            this.Nombre = pNombre;
            this.CliSelect = pClieSelect;
            this.Estatura = pEstatura;
            labelNombre.Content = pNombre;
            labelDoc.Content = Convert.ToString(pClieSelect);

        }

        public void cargar()
        {
            try
            {
                Conexion conectar = new Conexion();
                conectar.Abrir();
                string comando = "INSERT INTO MEDIDASC (DOC_CLIENTE,ESTATURA,PESO,EDAD,INDI_MASA_CORP,D_BRAZO_D,D_BRAZO_I,D_ANTEBRAZO_D,D_ANTEBRAZO_I,D_HOMBRO,D_PECHO,D_ABDOMEN,D_GLUTEO,D_PIERNA_D,D_PIERNA_I,D_PANTORRILLA_D,D_PANTORRILLA_I,FECHA) VALUES (@DOC,@ESTATURA,@PESO,@EDAD,@INDI,@BRAZO_D,@BRAZO_I,@ANTEBR_D,@ANTEBR_I,@HOMBRO,@PECHO,@ABD,@GLUTEO,@PIERNA_D,@PIERNA_I,@PANT_D,@PANT_I,@FECHA)";
                SqlCommand cmd = new SqlCommand(comando, conectar.Conectarbd);
                cmd.Parameters.AddWithValue("@DOC", CliSelect);
                cmd.Parameters.AddWithValue("@ESTATURA", Estatura);
                cmd.Parameters.AddWithValue("@PESO", Convert.ToInt64(textPeso.Text));
                cmd.Parameters.AddWithValue("@EDAD", Convert.ToInt64(textEdad.Text));
                cmd.Parameters.AddWithValue("@INDI", Convert.ToDouble(labelIndmasa.Content.ToString()));
                cmd.Parameters.AddWithValue("@BRAZO_D", Convert.ToDouble(textBarzoD.Text));
                cmd.Parameters.AddWithValue("@BRAZO_I", Convert.ToDouble(textBarzoI.Text));
                cmd.Parameters.AddWithValue("@ANTEBR_D", Convert.ToDouble(textAntebarzoD.Text));
                cmd.Parameters.AddWithValue("@ANTEBR_I", Convert.ToDouble(textAntebarzoI.Text));
                cmd.Parameters.AddWithValue("@HOMBRO", Convert.ToDouble(textHombro.Text));
                cmd.Parameters.AddWithValue("@PECHO", Convert.ToDouble(textPecho.Text));
                cmd.Parameters.AddWithValue("@ABD", Convert.ToDouble(textAbdomen.Text));
                cmd.Parameters.AddWithValue("@GLUTEO", Convert.ToDouble(textGluteo.Text));
                cmd.Parameters.AddWithValue("@PIERNA_D", Convert.ToDouble(textPiernaD.Text));
                cmd.Parameters.AddWithValue("@PIERNA_I", Convert.ToDouble(textPiernaI.Text));
                cmd.Parameters.AddWithValue("@PANT_D", Convert.ToDouble(textPantorrillaD.Text));
                cmd.Parameters.AddWithValue("@PANT_I", Convert.ToDouble(textPantorrillaI.Text));
                cmd.Parameters.AddWithValue("@FECHA", DateTime.Now.ToString("yyyy-MM-dd"));
                int ok = cmd.ExecuteNonQuery();
                if (ok == 1)
                {
                    MessageBox.Show("Datos Guardados con Exito", "ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conectar.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double pestatura = (Convert.ToInt64(textEstatura.Text));
                double ppeso = (Convert.ToInt64(textPeso.Text));
                Console.WriteLine(Convert.ToString(pestatura));
                Console.WriteLine(Convert.ToString(ppeso));
                Int64 pedad = (Convert.ToInt64(textEdad.Text));
                IndiceMasa = (ppeso/1000) / (Math.Pow((pestatura/100),2));
                
                labelIndmasa.Content = Convert.ToString(IndiceMasa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cargar();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Graficar graficar = new Graficar(CliSelect,Nombre);
            graficar.Show();
        }
    }
}
