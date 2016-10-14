using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Nota
    {
        public int idNota { get; set; }
        public string trimestre { get; set; }
        public int nota { get; set; }
        public int IdAlumno { get; set; }
        public int IdMateria { get; set; }
        public int IdDivision { get; set; }

        public void Cargar_Nota(List<Nota> listaTraida, string trim,int Materia, int division)
        {

            for (int i = 0; i < listaTraida.Count; i++)
            {


                AbrirConexion conecxion = new AbrirConexion();
                MySqlConnection conn = new MySqlConnection();
                conn = conecxion.Conexion();
                MySqlCommand con = conn.CreateCommand();
                con.CommandText = "INSERT INTO nota(Trimestre,Nota, IdAlumno, IdMateria,IdDivision) VALUES(@trim,@nota, @Id, @Mat, @iddiv)";
                con.Parameters.Add("@trim", trim);
                con.Parameters.Add("@nota", listaTraida[i].nota);
                con.Parameters.Add("@Id", listaTraida[i].IdAlumno);
                con.Parameters.Add("@Mat", Materia);
                con.Parameters.Add("@iddiv", division);
                con.ExecuteNonQuery();
                conn.Close();



            }

        }
        public List<Nota> ListarNotas()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Falta> listaFalta = new List<Falta>();
            string sql = "SELECT * FROM `nota`  ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);


            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Nota unaNota = new Nota();
                unaNota.idNota = Convert.ToInt32(rdr[0]);
                unaNota.trimestre = Convert.ToDateTime(rdr[1]);
                unaNota.nota = rdr[2].ToString();
                unaNota.IdAlumno = Convert.ToInt32(rdr[3]);
                unaNota.IdMateria = Convert.ToInt32(rdr[4]);
                unafalta.IdDivision = Convert.ToInt32(rdr[5]);
                listaFalta.Add(unafalta);
            }
            rdr.Close();
            return listaFalta;
            conn.Close();
        }
    }
}