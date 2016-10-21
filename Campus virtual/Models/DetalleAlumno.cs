using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class DetalleAlumno
    {
        public int IdDetalleAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set;}
        public int Cantidad_Inasistencias { get; set; }
        public int Cantidad_Sanciones { get; set; }


        public int CantidadFaltas(int IdAlumno)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT Count(*) FROM falta where IdAlumno =@IdAlumno";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public int CantidadSancion(int IdAlumno)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT Count(*) FROM sancion where idAlumno =@IdAlumno";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public DetalleAlumno TraerAlumno(int Id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();

            string sql = "SELECT *  FROM alumno where IdAlumno=@Id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            DetalleAlumno unAlumno = new DetalleAlumno();
            while (rdr.Read())
            {

               
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                
                unAlumno.NombreUsuario = rdr[4].ToString();
              


            }
            rdr.Close();

            conn.Close();
            return unAlumno;
        }
    }
}