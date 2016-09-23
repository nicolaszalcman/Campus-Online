using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Noticias
    {
        public int IdNotica { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Nota { get; set; }

        public string Foto { get; set; }

        public DateTime Fecha { get; set; }


        public void CargarNoticia()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "INSERT INTO noticias (Titulo, Descripcion, Nota, Foto, Fecha) VALUES (@value2,@value3,@value4, @value5, @value6)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@value2", Titulo);
            cmd.Parameters.AddWithValue("@value3", Descripcion);
            cmd.Parameters.AddWithValue("@value4", Nota);
            cmd.Parameters.AddWithValue("@value5", Foto);
            cmd.Parameters.AddWithValue("@value6", Fecha);

            cmd.ExecuteNonQuery();
        }

        public void EliminarNoticia(int IdNoticia)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "delete from noticias where IdNoticia = @IdNoticia";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdNoticia", IdNotica);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Noticias> ListarNoticias()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Noticias> listaSanciones = new List<Noticias>();
            string sql = "SELECT * FROM `noticias` ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Noticias unaSancion = new Noticias();
                unaSancion.IdNotica = Convert.ToInt32(rdr[0]);
                unaSancion.Titulo = rdr[1].ToString();
                unaSancion.Descripcion = rdr[2].ToString();
                unaSancion.Nota = rdr[3].ToString();
                unaSancion.Foto = rdr[4].ToString();
                unaSancion.Fecha = Convert.ToDateTime(rdr[5]);
                listaSanciones.Add(unaSancion);
            }
            rdr.Close();
            return listaSanciones;
        }

        public void AgregarFoto()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "INSERT INTO noticias ( Foto ) VALUES(@pFoto) ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pFoto", Foto);
           
            cmd.ExecuteNonQuery();
        }
    }
}