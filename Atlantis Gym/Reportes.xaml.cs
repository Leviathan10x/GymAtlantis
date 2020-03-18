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
using System.Windows.Forms;

namespace Atlantis_Gym
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReporVentas reporVentas = new ReporVentas();
            reporVentas.Show();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            VentasvsUsuario ventasvsUsuario = new VentasvsUsuario();
            ventasvsUsuario.Show();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            VentasvsUsuario ventasvsUsuario = new VentasvsUsuario();
            ventasvsUsuario.Show();

        }

        private void Button2_Click_1(object sender, RoutedEventArgs e)
        {
            ReportesN reportesN = new ReportesN(0,"Todos",true);
            reportesN.Show();
        }
    }
}
