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





        public List<Alumno> ListarAlumnos(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT *  FROM alumno inner join division on alumno.IdDivision = division.IdDivision where division.Año = @año AND division.Division = @division order by Apellido asc ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@año", año);
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

        public List<Alumno> Listar_Alumnos_Falta(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT * FROM alumno INNER JOIN division on alumno.IdDivision = division.IdDivision WHERE division.Año = @anio AND division.Division = @letra";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@anio", año);
            cmd.Parameters.Add("@letra", division);
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

        public Boolean IniciarSesion(string NombreUsu, string contraseña)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT * FROM alumno Where NombreUsuario = @NomUsu and Contraseña = @Contra";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@NomUsu", NombreUsu);
            cmd.Parameters.Add("@Contra", contraseña);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int BuscarId(string NombreUsuario)
        {
            int id = 0;
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT * FROM alumno Where NombreUsuario = @NomUsu ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@NomUsu", NombreUsuario);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                id = Convert.ToInt32(rdr[0]);
            }
            rdr.Close();
            conn.Close();
            return id;
        }
    }
}

