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
        public ActionResult Inasistencias()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
           ViewBag.listaalumnos = unAlumno.Listar_Alumnos_Falta(año, division);
            return View();
        }
        public ActionResult ActualizarAnio(int anio)
        {
            Division unaDivision = new Division();
            ViewBag.listardivisiones = unaDivision.ListarDivisiones_X_Anio(anio);
            return PartialView("_cboAnio");
        }
        [HttpPost]
        public ActionResult CargarInasistencias(List<Falta> faltas)
        {

            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos();
            Falta falta = new Falta();
            falta.Cargar_Falta(faltas);

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
            Sancion unaSancion = new Sancion();
            ViewBag.listasanciones = unaSancion.ListarSanciones();
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
            Sancion unaSancion = new Sancion();
            ViewBag.listasanciones = unaSancion.ListarSanciones();
            return View("Sanciones");
        }
        [HttpPost]
        public ActionResult EliminarSancion ()
        {
            return RedirectToAction("Sanciones");
        }

    

    }

}
