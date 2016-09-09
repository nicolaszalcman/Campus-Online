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

        public int IdDivision { get; set; }

        public void Cargar_Falta( List<Falta> listaTraida, Falta unaFalta)
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
                con.CommandText = "INSERT INTO Falta(Fecha,Tipo,IdAlumno, IdMateria, IdDivision) VALUES(@fech,@tip, @Id, @Mat, @iddiv)";
                con.Parameters.Add("@fech", unaFalta.fecha.ToString("yyyyMMdd"));
                con.Parameters.Add("@tip", listaTraida[i].tipo);
                con.Parameters.Add("@Id", listaTraida[i].idAlumno);
                con.Parameters.Add("@Mat", unaFalta.IdMateria);
                con.Parameters.Add("@iddiv", unaFalta.IdDivision);
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
            cmd.Parameters.Add("@ingfecha", Fecha);
            cmd.Parameters.Add("@ingmat", idMateria);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Falta unafalta = new Falta();
                unafalta.idFalta = Convert.ToInt32(rdr[0]);
                unafalta.fecha = Convert.ToDateTime(rdr[1]);
                unafalta.tipo = rdr[2].ToString();
                unafalta.IdMateria = Convert.ToInt32(rdr[4]);
                unafalta.IdDivision = Convert.ToInt32(rdr[5]);
                unafalta.idAlumno = Convert.ToInt32(rdr[6]);
                unafalta.nombre = rdr[7].ToString();
                unafalta.apellido = rdr[8].ToString();
                listaFalta.Add(unafalta);
            }
            rdr.Close();
            return listaFalta;
            conn.Close();
        }
        public void Modificar_Falta(List<Falta> listafaltas, DateTime fecha)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "UPDATE `falta` SET `Tipo`= @ingtipo WHERE IdAlumno = @ingalumno AND falta.Fecha = @ingfecha AND falta.IdMateria = @ingmat";
            for (int i = 0; i < listafaltas.Count(); i++)
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ingtipo", listafaltas[i].tipo);
                cmd.Parameters.AddWithValue("@ingalumno", listafaltas[i].idAlumno);
                cmd.Parameters.AddWithValue("@ingfecha", fecha);
                cmd.Parameters.AddWithValue("@ingmat", listafaltas[i].IdMateria);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public Boolean HayUnaFalta(Falta fecha, int IdDivision, int IdMateria, List<Falta> listaFalta)
        {
            for (int i =0; i < listaFalta.Count(); i++)
            {
                if(listaFalta[i].fecha == fecha.fecha)
                {
                    if(listaFalta[i].IdDivision == IdDivision)
                    {
                        if(listaFalta[i].IdMateria == IdMateria)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;

            
        }

        public List<Falta> ListarFaltas()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Falta> listaFalta = new List<Falta>();
            string sql = "SELECT * FROM `falta`  ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Falta unafalta = new Falta();
                unafalta.idFalta = Convert.ToInt32(rdr[0]);
                unafalta.fecha = Convert.ToDateTime(rdr[1]);
                unafalta.tipo = rdr[2].ToString();
                unafalta.IdMateria = Convert.ToInt32(rdr[4]);
                unafalta.IdDivision = Convert.ToInt32(rdr[5]);
                listaFalta.Add(unafalta);
            }
            rdr.Close();
            return listaFalta;
            conn.Close();
        }
        

       
    }
}