﻿using System;
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
            Division unaDivision = new Division();
            ViewBag.listadivision = unaDivision.listardivisiones();
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos();
            return View();
        }
        [HttpPost]
        public ActionResult CargarInasistencias(List<Falta> faltas)
        {
            Division unaDivision = new Division();
            ViewBag.listadivision = unaDivision.listardivisiones();
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos();
            


            return View("Inasistecias");
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
            Sancion unaSancion = new Sancion();
            ViewBag.listasanciones = unaSancion.ListarSanciones();
            unasancion.Cargar_Sancion();
            return View("Sanciones");
        }




    
    }

}