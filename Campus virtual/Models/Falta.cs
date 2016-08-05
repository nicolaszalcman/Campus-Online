﻿using System;
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

        public int IdMateria { get; set; }


        public void Cargar_Falta(List<Falta> listaTraida, int materia)
        {
          
            for (int i =0; i< listaTraida.Count; i++)
            {
                if(listaTraida[i].tipo == null)
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

        
    }
}