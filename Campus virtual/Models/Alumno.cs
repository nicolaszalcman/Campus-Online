using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Campus_virtual.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }



        public List<Alumno> ListarAlumnos()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT *  FROM alumno order by Apellido asc ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            
           
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                Alumno unAlumno = new Alumno();
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                listaAlumnos.Add(unAlumno);
            }
            rdr.Close();
            return listaAlumnos;
            conn.Close();
        }

        public List<Alumno> Listar_Alumnos_Falta(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT *  FROM alumno where IdDivision = @division ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@division", division);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Alumno unAlumno = new Alumno();
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                listaAlumnos.Add(unAlumno);
            }
            rdr.Close();
            return listaAlumnos;
            conn.Close();

        }

    }
}