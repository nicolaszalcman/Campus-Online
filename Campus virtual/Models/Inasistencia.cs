using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Campus_virtual.Models
{
    public class Inasistencia
    {
        int idFalta { get; set; }
        DateTime Fecha { get; set; }
        int idAlumno { get; set; }
        string tipoFalta { get; set; }


    }
   
   
}