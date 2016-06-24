using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace Campus_virtual.Models
{
    public class AbrirConexion
    {
        public  MySqlConnection Conexion()
        {
            MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=;database=campus;"; 

            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            return conn;

        }
    }
}