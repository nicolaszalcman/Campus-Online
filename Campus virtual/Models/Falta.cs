using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Campus_virtual.Models
{
    public class Falta
    {
        public int idFalta { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public int idAlumno { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public int IdMateria { get; set; }


        public void Cargar_Falta(List<Falta> listaTraida, int materia)
        {

            for (int i = 0; i < listaTraida.Count; i++)
            {
                if (listaTraida[i].tipo == null)
                {
                    listaTraida[i].tipo = "Presente";

                }

                AbrirConexion conecxion = new AbrirConexion();
                MySqlConnection conn = new MySqlConnection();
                conn = conecxion.Conexion();
                MySqlCommand con = conn.CreateCommand();
                con.CommandText = "INSERT INTO Falta(Fecha,Tipo,IdAlumno, IdMateria) VALUES(@fech,@tip, @Id, @Mat)";
                con.Parameters.Add("@fech", listaTraida[i].fecha);
                con.Parameters.Add("@tip", listaTraida[i].tipo);
                con.Parameters.Add("@Id", listaTraida[i].idAlumno);
                con.Parameters.Add("@Mat", materia);
                con.ExecuteNonQuery();
                conn.Close();



            }




        }

        public void EliminarFalta(int IdFalta)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "delete from sancion where IdFalta = @IdFalta";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdSancion", IdFalta);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Falta> TraerFaltas_X_Fecha(DateTime Fecha, int division, string letra, int idMateria)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Falta> listaFalta = new List<Falta>();
            string sql = "SELECT * FROM `falta` INNER JOIN alumno ON falta.IdAlumno = alumno.IdAlumno INNER JOIN division ON alumno.IdDivision = division.IdDivision WHERE division.Division = @ingletra AND division.Año = @inganio AND falta.fecha = @ingfecha AND falta.IdMateria = @ingmat ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@ingletra", letra);
            cmd.Parameters.Add("@inganio", division);
            cmd.Parameters.Add("@ingfecha", Fecha.ToString("yyyyddMM"));
            cmd.Parameters.Add("@ingmat", idMateria);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Falta unafalta = new Falta();
                unafalta.idFalta = Convert.ToInt32(rdr[0]);
                unafalta.fecha = Convert.ToDateTime(rdr[1]);
                unafalta.tipo = rdr[2].ToString();
                unafalta.IdMateria = Convert.ToInt32(rdr[4]);
                unafalta.nombre = rdr[6].ToString();
                unafalta.apellido = rdr[7].ToString();
                listaFalta.Add(unafalta);
            }
            rdr.Close();
            return listaFalta;
            conn.Close();
        }
        public void Modificar_Falta(List<Falta> listafaltas)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "UPDATE `falta` SET `Tipo`= @ingtipo WHERE IdAlumno = @ingalumno AND falta.Fecha = @ingfecha AND falta.IdMateria = @ingmat";
            for (int i = 0; i < listafaltas.Count(); i++)
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ingtipo", listafaltas[i].tipo );
                cmd.Parameters.AddWithValue("@ingalumno", listafaltas[i].idAlumno);
                cmd.Parameters.AddWithValue("@ingfecha", listafaltas[i].fecha.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@ingmat", listafaltas[i].IdMateria);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
        
    }
}