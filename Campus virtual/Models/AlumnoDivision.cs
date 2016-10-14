using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Campus_virtual.Models
{
    public class AlumnoDivision
    {
        public int idAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int IdDivision { get; set; }

        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public int Año { get; set; }
        public string NombreDivision { get; set; }

        public string DivisionCompleta { get; set; }

    }
}