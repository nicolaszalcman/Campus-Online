﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Campus_virtual.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int IdDivision { get; set; }

        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string nomYAp { get { return Nombre + " " + Apellido; } }





        public List<Alumno> ListarAlumnos(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT *  FROM alumno inner join division on alumno.IdDivision = division.IdDivision where division.Año = @año AND division.Division = @division order by Apellido asc ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@año", año);
            cmd.Parameters.Add("@division", division);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Alumno unAlumno = new Alumno();
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                

                listaAlumnos.Add(unAlumno);
            }
            rdr.Close();
            return listaAlumnos;
            conn.Close();
        }

        public List<Alumno> Listar_Alumnos_Falta(int año, string division)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT * FROM alumno INNER JOIN division on alumno.IdDivision = division.IdDivision WHERE division.Año = @anio AND division.Division = @letra ORDER BY Apellido ASC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@anio", año);
            cmd.Parameters.Add("@letra", division);
            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                Alumno unAlumno = new Alumno();
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                listaAlumnos.Add(unAlumno);
            }
            rdr.Close();
            return listaAlumnos;
            conn.Close();

        }

        public Boolean IniciarSesion(string NombreUsu, string contraseña)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT * FROM alumno Where NombreUsuario = @NomUsu and Contrasenia = @Contra";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@NomUsu", NombreUsu);
            cmd.Parameters.Add("@Contra", contraseña);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Alumno BuscarId(string NombreUsuario)
        {
            
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "SELECT * FROM alumno Where NombreUsuario = @NomUsu ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@NomUsu", NombreUsuario);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Alumno unAlumno = new Alumno();

            while (rdr.Read())
            {
                
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
            }
            rdr.Close();
            conn.Close();
            return unAlumno;
        }

        public void AgregarAlumno(int Divi)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();

            string sql = "INSERT INTO alumno (Nombre, Apellido, IdDivision, NombreUsuario, Contrasenia) VALUES (@pNombre, @pApellido, @pIdDivision, @pNombreUsu, @pContra)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pNombre", Nombre);
            cmd.Parameters.AddWithValue("@pApellido", Apellido);
            cmd.Parameters.AddWithValue("@pIdDivision",Divi);
            cmd.Parameters.AddWithValue("@pNombreUsu", NombreUsuario);
            cmd.Parameters.AddWithValue("@pContra", Contrasenia);

            cmd.ExecuteNonQuery();
        }

        public List<Alumno> ListarAlumnosConId(int IdDivision)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            List<Alumno> listaAlumnos = new List<Alumno>();
            string sql = "SELECT *  FROM alumno  where IdDivision= @Id ORDER BY  Apellido ASC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            
            cmd.Parameters.Add("@Id", IdDivision);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Alumno unAlumno = new Alumno();
                unAlumno.idAlumno = Convert.ToInt32(rdr[0]);
                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                unAlumno.IdDivision= Convert.ToInt32( rdr[3]);
                unAlumno.NombreUsuario= rdr[4].ToString();
                unAlumno.Contrasenia= rdr[5].ToString();
                listaAlumnos.Add(unAlumno);
            }
            rdr.Close();
            return listaAlumnos;
            conn.Close();
        }

        public void EliminarAlumno(int id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "delete from alumno where IdAlumno = @IdAlumno";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdAlumno", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void ModificarAlumno(int id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();
            string sql = "UPDATE alumno SET Nombre = @pTitu, Apellido = @pDesc,  IdDivision = @pFuente, NombreUsuario = @pFoto ,Contrasenia = @pFecha WHERE IdAlumno = @pIdNoti";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pTitu", Nombre);
            cmd.Parameters.AddWithValue("@pDesc", Apellido);
            cmd.Parameters.AddWithValue("@pFuente", id);
            cmd.Parameters.AddWithValue("@pFoto", NombreUsuario);
            cmd.Parameters.AddWithValue("@pFecha", Contrasenia);
            cmd.Parameters.AddWithValue("@pIdNoti", idAlumno);

            cmd.ExecuteNonQuery();
        }


        public Alumno TraerUnAlumnoo(int Id)
        {
            AbrirConexion abrirconexion = new AbrirConexion();
            MySqlConnection conn = new MySqlConnection();
            conn = abrirconexion.Conexion();

            string sql = "SELECT *  FROM alumno where IdAlumno=@Id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Alumno unAlumno = new Alumno();
            while (rdr.Read())
            {


                unAlumno.Nombre = rdr[1].ToString();
                unAlumno.Apellido = rdr[2].ToString();
                unAlumno.IdDivision = Convert.ToInt32(rdr[3]);
                unAlumno.NombreUsuario = rdr[4].ToString();
                unAlumno.Contrasenia = rdr[5].ToString();



            }
            rdr.Close();

            conn.Close();
            return unAlumno;
        }

    }
}

