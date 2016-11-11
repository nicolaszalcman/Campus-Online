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
        public string Materia { get; set; }
        public int notatrimestre1 { get; set; }
        public int notatrimestre2 { get; set; }
        public int notatrimestre3 { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }

        public void Modificar_Nota(List<Nota> listaNotas, string trimestre, int Materia)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "UPDATE `nota` SET `Nota`= @nota WHERE Trimestre = @trim AND nota.IdAlumno = @ingalum AND nota.IdMateria = @ingmat";
            for (int i = 0; i < listaNotas.Count(); i++)
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nota", listaNotas[i].nota);
                cmd.Parameters.AddWithValue("@trim", trimestre);
                cmd.Parameters.AddWithValue("@ingalum", listaNotas[i].IdAlumno);
                cmd.Parameters.AddWithValue("@ingmat", Materia);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void Cargar_Nota(List<Nota> listaTraida, string trim, int Materia, int division)
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
        public List<Nota> ListarNotasXAlumno(int IdAlumno)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Nota> ListaNotaAlu = new List<Nota>();
            string sql = "select DISTINCT m.Nombre AS Materia, (select n2.nota from nota AS n2 where n2.IdMateria=n.IdMateria and n2.Trimestre='Primer' and n2.IdAlumno=n.IdAlumno)as Trimestre1, (select n3.nota from nota n3 where n3.IdMateria=n.IdMateria and n3.Trimestre='Segundo' and n3.IdAlumno=n.IdAlumno)as Trimestre2 ,(select n4.nota from nota n4 where n4.IdMateria=n.IdMateria and n4.Trimestre='Tercer' and n4.IdAlumno=n.IdAlumno)as Trimestre3 from nota n inner join materia m on m.IdMateria=n.IdMateria where n.IdAlumno=@ingid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@ingid", IdAlumno);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                
                Nota unaNota = new Nota();
                unaNota.Materia = rdr[0].ToString();
                try
                {
                    unaNota.notatrimestre1 = Convert.ToInt32(rdr[1]);
                }
                catch (InvalidCastException e)
                {
                    unaNota.notatrimestre1 = 0;
                }
                try
                {
                    unaNota.notatrimestre2 = Convert.ToInt32(rdr[2]);
                }
                catch (InvalidCastException e)
                {
                    unaNota.notatrimestre2 = 0;
                }
                try
                {
                    unaNota.notatrimestre3 = Convert.ToInt32(rdr[3]);
                }
                catch (InvalidCastException e)
                {
                    unaNota.notatrimestre3 = 0;
                }

                ListaNotaAlu.Add(unaNota);
            }
            rdr.Close();
            return ListaNotaAlu;
            conn.Close();

        }
        public List<Nota> ListarNotas()
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Nota> ListaNota = new List<Nota>();
            string sql = "SELECT * FROM `nota`  ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);


            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Nota unaNota = new Nota();
                unaNota.idNota = Convert.ToInt32(rdr[0]);
                unaNota.trimestre = (rdr[1]).ToString();
                unaNota.nota = Convert.ToInt32(rdr[2]);
                unaNota.IdAlumno = Convert.ToInt32(rdr[3]);
                unaNota.IdMateria = Convert.ToInt32(rdr[4]);
                unaNota.IdDivision = Convert.ToInt32(rdr[5]);
                ListaNota.Add(unaNota);
            }
            rdr.Close();
            return ListaNota;
            conn.Close();
        }
        public Boolean HayUnaNota(string trimestre, int IdDivision, int IdMateria, List<Nota> listaNota)
        {
            for (int i = 0; i < listaNota.Count(); i++)
            {
                if (listaNota[i].trimestre == trimestre)
                {
                    if (listaNota[i].IdDivision == IdDivision)
                    {
                        if (listaNota[i].IdMateria == IdMateria)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;


        }
        public List<Nota> TraerFaltas_X_todo(string trimestre, int IdDivision, int IdMateria)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Nota> listaNota = new List<Nota>();
            string sql = "SELECT * FROM `nota` INNER JOIN alumno ON nota.IdAlumno = alumno.IdAlumno INNER JOIN division ON alumno.IdDivision = division.IdDivision WHERE division.IdDivision= @ingdiv AND nota.Trimestre= @trim AND nota.IdMateria = @mat";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@ingdiv", IdDivision);
            cmd.Parameters.Add("@trim", trimestre);
            cmd.Parameters.Add("@mat", IdMateria);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Nota unaNota = new Nota();
                unaNota.idNota = Convert.ToInt32(rdr[0]);
                unaNota.trimestre = (rdr[1]).ToString();
                unaNota.nota = Convert.ToInt32( rdr[2]);
                unaNota.IdAlumno = Convert.ToInt32(rdr[3]);
                unaNota.IdMateria = Convert.ToInt32(rdr[4]);
                unaNota.IdDivision = Convert.ToInt32(rdr[5]);
                unaNota.nombre = rdr[7].ToString();
                unaNota.apellido = rdr[8].ToString();
                listaNota.Add(unaNota);
            }
            rdr.Close();
            return listaNota;
            conn.Close();
        }


    }
}