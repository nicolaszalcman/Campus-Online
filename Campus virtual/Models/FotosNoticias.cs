using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class FotosNoticias
    {
        public int IdFoto { get; set; }

        public string Nombre { get; set; }


        public void AgregarFoto()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "INSERT INTO fotoNoticia ( Nombre ) VALUES(@pFoto) ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pFoto", Nombre);

            cmd.ExecuteNonQuery();
        }
    }
}