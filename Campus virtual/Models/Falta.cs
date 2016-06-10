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
        public void Cargar_Falta()
        {



        }
    }
}