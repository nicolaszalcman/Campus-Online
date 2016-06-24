using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Division
    {
        public int idDivision { get; set; }
        public int Año { get; set; }
        public string Nombre { get; set; }


        public List<string> ListarDivisiones_X_Anio(int anio)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<string> ListaDivisiones_X_año = new List<string>();
            string sql = "SELECT Division FROM division WHERE Año = @inganio order by Division asc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@inganio", anio);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ListaDivisiones_X_año.Add()
            }
            rdr.Close();
            return ListaDivisiones_X_año;

        }

    }
}