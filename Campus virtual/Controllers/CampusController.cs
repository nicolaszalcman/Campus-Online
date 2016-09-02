using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus_virtual.Models;

namespace Campus_virtual.Controllers
{
    public class CampusController : Controller
    {
        //
        // GET: /Campus/     
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModificarInasistencias(List<Falta> unaFalta)
        {
            Falta unaFaltastatic = new Falta();
            unaFaltastatic.Modificar_Falta(unaFalta);
            return View();
        }
        public ActionResult ModificarFalta()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            return View();
        }
        [HttpPost]
        public ActionResult ModificarFaltaCurso(Falta unaFalta, int anio, string letra, int idMateria)
        {
            Falta objFalta = new Falta();
            List<Falta> listaFaltas = objFalta.TraerFaltas_X_Fecha(unaFalta.fecha, anio,letra, idMateria);
            return View(listaFaltas);
        }
        public ActionResult Inasistencias()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
            //ViewBag.Listar_Alumnos_Falta = unAlumno.Listar_Alumnos_Falta();
            return View();
        }
        public ActionResult ActualizarAnio(Falta unaFalta, int anio, string Letra, int IdMateria )
        {
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.Listar_Alumnos_Falta(anio, Letra);
            Falta Unafalta = new Falta();
            Boolean falta;
            Division unaDivision = new Division();
            int divi;
            divi = unaDivision.TraerIdDivision( anio,  Letra);
            List<Falta> lista;
            lista = Unafalta.ListraFaltas();
            falta = Unafalta.HayUnaFalta(unaFalta, divi, IdMateria, lista );
            TempData.Add("IdDivision", divi);

            if(falta == true)
            {
                TempData.Add("Fecha", unaFalta);
                TempData.Add("IdMateria", IdMateria);
                TempData.Keep();
                return View("ModificarFaltaCurso");
            }
            else
            {
                TempData.Add("Fecha", unaFalta);
                TempData.Add("IdMateria", IdMateria);
                TempData.Keep();
                return View();
            }
            //
            
        }
        [HttpPost]
        public ActionResult CargarInasistencias(List<Falta> faltas)
        {

            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();

           
           

            Falta falta = new Falta();
            falta.Cargar_Falta((DateTime)TempData["Fecha"]),(int)TempData["IdDivision"] , (int)TempData["IdMateria"],faltas );
      
            return View("Inasistencias");
        }

        
        public ActionResult VerSancion(int idSancion)
        {
            Sancion unaSancion = new Sancion();
            unaSancion = unaSancion.traeruno(idSancion);
            return View(unaSancion);
        }


        public ActionResult Sanciones()
        {
            
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            return View();
        }
        [HttpPost]
        public ActionResult ListaSanciones(int anio, string Letra)
        {
            Sancion unaSancion = new Sancion();
            ViewBag.listasanciones = unaSancion.ListarSanciones(anio, Letra);
            TempData.Add("anio", anio);
            TempData.Add("letra", Letra);
            TempData.Keep();
            return View();
        }

        public ActionResult Altasancion()
        {
            
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos((int) TempData["anio"], (string)TempData["letra"]);
            

            return View();
        }
        [HttpPost]
        public ActionResult Altasancion(Sancion unasancion)
        {
            unasancion.Cargar_Sancion(); 
            return RedirectToAction("Sanciones");
        }

        public ActionResult EliminarSancion(int IdSancion)
        {
            Sancion san = new Sancion();
            san.EliminarSacion(IdSancion);
           
            return View("Sanciones");
        }
        [HttpPost]
        public ActionResult EliminarSancion ()
        {
            return RedirectToAction("Sanciones");
        }

    

    }

}
