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

        public void Cargar_Nota(List<Nota> listaTraida, int Materia, int division)
        {

            for (int i = 0; i < listaTraida.Count; i++)
            {


                AbrirConexion conecxion = new AbrirConexion();
                MySqlConnection conn = new MySqlConnection();
                conn = conecxion.Conexion();
                MySqlCommand con = conn.CreateCommand();
                con.CommandText = "INSERT INTO notas(Trimestre,,IdAlumno, IdMateria, IdDivision) VALUES(@fech,@tip, @Id, @Mat, @iddiv)";
                con.Parameters.Add("@tip", listaTraida[i].tipo);
                con.Parameters.Add("@Id", listaTraida[i].idAlumno);
                con.Parameters.Add("@Mat", unaFalta.IdMateria);
                con.Parameters.Add("@iddiv", divi);
                con.ExecuteNonQuery();
                conn.Close();



            }




        }
    }
}