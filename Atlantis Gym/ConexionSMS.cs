using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Atlantis_Gym
{
    class ConexionSMS
    {
        string cadena = "Data Source=(LocalDB)\\GYM;Initial Catalog=MensajesTXT;Integrated Security=True";
        public SqlConnection Conectarbd = new SqlConnection();

        public ConexionSMS()
        {
            Conectarbd.ConnectionString = cadena;
        }
        public void Abrir()
        {
            try
            {
                Conectarbd.Open();
                Console.WriteLine("Conexion Abierta SMS");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error base de datos" + ex.Message);
            }
        }
        public void Cerrar()
        {
            Conectarbd.Close();
            Console.WriteLine("Conexion Cerrada SMS");
        }

    }
}
