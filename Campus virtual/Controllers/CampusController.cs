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
        public ActionResult ModificarFalta()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            return View();
        }
        public ActionResult ModificarFaltaCurso(Falta unaFalta, int anio, string Letra, int IdMateria)
        {
            Falta objFalta = new Falta();

            ViewBag.listaFaltas = objFalta.TraerFaltas_X_Fecha(unaFalta.fecha, IdMateria);
            return View();
        }
        public ActionResult Inasistencias()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
            //ViewBag.Listar_Alumnos_Falta = unAlumno.Listar_Alumnos_Falta();
            return View();
        }
        public ActionResult ActualizarAnio(int anio, string Letra, int IdMateria )
        {
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.Listar_Alumnos_Falta(anio, Letra);
            TempData.Add("IdMateria",IdMateria);
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult CargarInasistencias(List<Falta> faltas)
        {

            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
           

            Falta falta = new Falta();
            falta.Cargar_Falta(faltas, (int)TempData["IdMateria"]);

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
            return View();
        }

        public ActionResult Altasancion()
        {
            
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos();
            

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
           
            return View("ListaSanciones");
        }
        [HttpPost]
        public ActionResult EliminarSancion ()
        {
            return RedirectToAction("Sanciones");
        }

    

    }

}
