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

        public List<Division> listardivisiones()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Division> listadivision = new List<Division>();
            string sql = "SELECT *  FROM division";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Division unaDivision = new Division();
                unaDivision.idDivision = Convert.ToInt32(rdr[0]);
                unaDivision.Año = Convert.ToInt32(rdr[1]);
                unaDivision.Nombre = rdr[2].ToString();
                listadivision.Add(unaDivision);
            }
            rdr.Close();
            return listadivision;
        }
    }
}