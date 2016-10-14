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
        public string DivisionCompleta { get; set; }


        public List<string> ListarDivisiones_X_Anio(int anio, string letra)
        {

            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<string> ListaDivisiones_X_año = new List<string>();
            string sql = "SELECT * FROM alumno INNER JOIN division on alumno.IdDivision = division.IdDivision WHERE division.Año = @anio AND division.Division = @letra";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@anio", anio);
            cmd.Parameters.Add("@letra", letra);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string Letra;
                Letra = rdr[0].ToString();
                ListaDivisiones_X_año.Add(Letra);
            }
            rdr.Close();
            return ListaDivisiones_X_año;

        }
        public int TraerIdDivision(int anio, string letra)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            int idDivision = 1000;
            string sql = "SELECT * FROM `division` WHERE division.Año = @anio AND division.Division= @letra";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@anio", anio);
            cmd.Parameters.Add("@letra", letra);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                idDivision = Convert.ToInt32(rdr[0]);
            }
            rdr.Close();
            return idDivision;
        } 

        public List<Division> ListarDivisiones()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Division> Lista = new List<Division>();
            string sql = "SELECT * FROM division";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Division midivi = new Division();
                midivi.idDivision =Convert.ToInt32( rdr[0]);
                midivi.Año = Convert.ToInt32(rdr[1]);
                midivi.Nombre = rdr[2].ToString();
                midivi.DivisionCompleta= rdr[3].ToString();

                Lista.Add(midivi);
            }
            rdr.Close();
            return Lista;
        }

        


    }
}