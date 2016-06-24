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

        
        public void Cargar_Falta(List<Falta> listaTraida)
        {
            AbrirConexion conecxion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = conecxion.Conexion();
            MySqlCommand con = conn.CreateCommand();
            for (int i =0; i< listaTraida.Count; i++)
            {
                if(listaTraida[i].tipo == "Ausente" || listaTraida[i].tipo == "Tarde")
                {
                    
                    
                    con.CommandText = "INSERT INTO Falta(Tipo,IdAlumno) VALUES(@tip, @Id)";
                    con.Parameters.Add("@tip", listaTraida[i].tipo);
                    con.Parameters.Add("@Id", listaTraida[i].idAlumno);
                    
                    
                }

            }
            con.ExecuteNonQuery();
            conn.Close();


        }
    }
}