using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class AlumnoDivision
    {
        public int idAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int IdDivision { get; set; }

        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public int Año { get; set; }
        public string NombreDivision { get; set; }

        public string DivisionCompleta { get; set; }

        public AlumnoDivision TraerUnAlumno(int Id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();

            string sql = "SELECT *  FROM alumno where IdAlumno=@Id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            AlumnoDivision unAlumno = new AlumnoDivision();
            while (rdr.Read())
            {


                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                unAlumno.IdDivision = Convert.ToInt32(rdr[3]);
                unAlumno.NombreUsuario = rdr[4].ToString();
                unAlumno.Contrasenia = rdr[5].ToString();



            }
            rdr.Close();

            conn.Close();
            return unAlumno;
        }

    }
}