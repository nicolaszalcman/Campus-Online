using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Sancion
    {
        public int IdSancion { get; set; }
        public DateTime fecha { get; set; }
        public string motivo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idAlumno { get; set; }
        public int IdDivision { get; set; }

        public List<Sancion> ListarSanciones(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Sancion> listaSanciones = new List<Sancion>();
            string sql = "SELECT * FROM `sancion` INNER JOIN alumno ON sancion.idAlumno = alumno.IdAlumno INNER JOIN division ON sancion.IdDivision = division.IdDivision WHERE  division.Año = @año AND division.Division = @division  ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@año", año);
            cmd.Parameters.Add("@division", division);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Sancion unaSancion = new Sancion();
                unaSancion.IdSancion = Convert.ToInt32(rdr[0]);
                unaSancion.fecha = Convert.ToDateTime(rdr[1]);
                unaSancion.motivo = rdr[2].ToString();
                unaSancion.nombre = rdr[5].ToString();
                unaSancion.apellido = rdr[6].ToString();
                listaSanciones.Add(unaSancion);
            }
            rdr.Close();
            return listaSanciones;

        }

        public void Cargar_Sancion(int idDivi)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "INSERT INTO sancion (Fecha, Motivo, idAlumno, IdDivision) VALUES (@value2,@value3,@value4, @value5)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@value2", fecha);
            cmd.Parameters.AddWithValue("@value3", motivo);
            cmd.Parameters.AddWithValue("@value4", idAlumno);
            cmd.Parameters.AddWithValue("@value5", idDivi);

            cmd.ExecuteNonQuery();

        }
        public Sancion traeruno ( int idSanciontr)
        {

            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT * FROM `sancion` INNER JOIN alumno ON sancion.idAlumno = alumno.IdAlumno WHERE sancion.IdSancion =@ingreseSancion";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ingreseSancion", idSanciontr);
            Sancion unaSancion = new Sancion();


            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                unaSancion.IdSancion = Convert.ToInt32(rdr[0]);
                unaSancion.fecha = Convert.ToDateTime(rdr[1]);
                unaSancion.motivo = rdr[2].ToString();
                unaSancion.nombre = rdr[5].ToString();
                unaSancion.apellido = rdr[6].ToString();
            }
            rdr.Close();
            return unaSancion;
        }
        //esta funcion no esta bien
        public void EliminarSacion(int IdSacion)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "delete from sancion where IdSancion = @IdSancion";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdSancion", IdSacion);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Sancion> ListarTodasSanciones()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Sancion> listaSanciones = new List<Sancion>();
            string sql = "SELECT * FROM `sancion` ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
         
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Sancion unaSancion = new Sancion();
                unaSancion.IdSancion = Convert.ToInt32(rdr[0]);
                unaSancion.fecha = Convert.ToDateTime(rdr[1]);
                unaSancion.motivo = rdr[2].ToString();
                unaSancion.nombre = rdr[5].ToString();
                unaSancion.apellido = rdr[6].ToString();
                listaSanciones.Add(unaSancion);
            }
            rdr.Close();
            return listaSanciones;
        }
    }
}