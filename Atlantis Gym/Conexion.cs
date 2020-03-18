using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Atlantis_Gym
{
    class Conexion
    {
        string cadena = "Data Source=(LocalDB)\\GYM;Initial Catalog=AtlantisGYM;Integrated Security=True";
        public SqlConnection Conectarbd = new SqlConnection();

        public Conexion()
        {
            Conectarbd.ConnectionString = cadena;
        }
        public void Abrir()
        {
            try
            {
                Conectarbd.Open();
                Console.WriteLine("Conexion Abierta");

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error base de datos" + ex.Message);
            }
        }
        public void Cerrar()
        {
            Conectarbd.Close();
            Console.WriteLine("Conexion Cerrada");
        }

    }
}
