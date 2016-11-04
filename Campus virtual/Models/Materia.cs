using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Materia
    {
        public int idMateria { get; set; }
        public string Nombre { get; set; }

        public List<Materia> listarmateria()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Materia> listamaterias = new List<Materia>();
            string sql = "SELECT *  FROM materia";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Materia unaMateria = new Materia();
                unaMateria.idMateria = Convert.ToInt32(rdr[0]);
                unaMateria.Nombre = rdr[1].ToString();
                listamaterias.Add(unaMateria);
            }
            rdr.Close();
            return listamaterias;

        }

        public string TraerMateria(int id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();

            string sql = "SELECT *  FROM materia where IdMateria = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                
                Nombre = rdr[1].ToString();
            }
            rdr.Close();
            return Nombre ;
        }
    }
}