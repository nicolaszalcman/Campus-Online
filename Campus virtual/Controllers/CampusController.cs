using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus_virtual.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus_virtual.Controllers
{
    
    public class CampusController : Controller
    {
        //
        // GET: /Campus/     
        public ActionResult ModificarNoticia(int parametro)
        {
            Models.Noticias MINoticia = new Models.Noticias();
            MINoticia = MINoticia.TraerUnaNoticia(parametro);
            ViewBag.NomFoto = MINoticia.Foto;
            return View(MINoticia);
        }
        [HttpPost]
        public ActionResult ModificarNoticia(Noticias MiNoticia, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                { 
                    var fileName =  Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);

                    file.SaveAs(path);

                    MiNoticia.Foto = fileName;
                }
            }
            catch (Exception Error)
            {

            }

            FotosNoticias Mifoto = new FotosNoticias();

            Noticias MiFoto = new Noticias();
            Mifoto.Nombre = MiNoticia.Foto;
            Mifoto.AgregarFoto();

            MiNoticia.ModificarNoticia();

            Noticias UnaNoti = new Noticias();

            List<Noticias> lista = UnaNoti.ListarNoticias();
            ViewBag.ListarNoticias = lista;


            return View("Index");
        }
        public ActionResult VerFaltaAlumno()
        {
            Alumno unAlumno = new Alumno();
            Falta unaFalta = new Falta();
            List<Falta> listarFaltasAlumno = unaFalta.Faltas_por_Alumnos((int)TempData["IdAlumno"]);
            TempData.Clear();
            ViewBag.listafaltas = listarFaltasAlumno;
            return View();
        }
        public ActionResult AgregarNoticia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarNoticia(Noticias MiNoti, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/"), fileName);

                        file.SaveAs(path);

                        MiNoti.Foto = fileName;
                    }
                }
                catch (Exception Error)
                {

                }

                FotosNoticias Mifoto = new FotosNoticias();

                MiNoti.CargarNoticia();
                Mifoto.Nombre = MiNoti.Foto;
                Mifoto.AgregarFoto();
                Noticias UnaNoti = new Noticias();

                List<Noticias> lista = UnaNoti.ListarNoticias();
                ViewBag.ListarNoticias = lista;
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EliminarNoticia(int parametro)
        {
            Noticias MiNoti = new Noticias();

            MiNoti.EliminarNoticia(parametro);
            List<Noticias> lista = MiNoti.ListarNoticias();
            ViewBag.ListarNoticias = lista;
            return View("Index");

        }
        public ActionResult Noticias()
        {
            Noticias UnaNoti = new Noticias();

            List<Noticias> lista = UnaNoti.ListarNoticias();
            ViewBag.ListarNoticias = lista;
            return View();
        }

        public ActionResult Index()
        {                     
            return View();
        }
        public ActionResult Notas()
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            return View();
        }
        public ActionResult NotasAlumno()
        {
            int IdAlumno = (int)TempData["IdAlumno"];
            Nota unaNota = new Nota();
            List<Nota> ListaNotasAlumno = unaNota.ListarNotasXAlumno(IdAlumno);
            ViewBag.listanotas = ListaNotasAlumno;
            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection form)
        {
            //function VerFaltas(falta){
                //window.open("Informe_Inasistencia_Superior_Web.asp?f=" + falta + "&cl=" + document.all.CmbCiclos.value + "&c=" + document.all.ca.value, "InasistenciaDetalla", "top=200,left=150,menubar=no,width=550,dependent=yes,height=300,toolbar=no,scrollbars=yes")
//}

            ViewBag.mensaje = "";
            string nombre = Request.Form["usuario"].ToString();
            string contraseña = Request.Form["pwd"].ToString();
            Alumno unAlumno = new Alumno();
            Falta unaFalta = new Falta();
            
            if(nombre == "Admin" && contraseña == "Admin")
            {
                Session.Clear();
                Session["Nombre"] = "Admin";
                return View();
            }
            Boolean hayUsuario = unAlumno.IniciarSesion(nombre, contraseña);
             if (hayUsuario == false)
            {
                ViewBag.mensaje = "Usuario Invalido";
                return RedirectToAction("Login", "Account", new { msg = "usuario invalido" });
            } else 
            {
                
                unAlumno = unAlumno.BuscarId(nombre);
                TempData.Clear();
                TempData.Add("IdAlumno", unAlumno.idAlumno);
                List<Falta> listarFaltasAlumno = unaFalta.Faltas_por_Alumnos(unAlumno.idAlumno);
                ViewBag.listafaltas = listarFaltasAlumno;
                Session["Alumno"] = "1";
                Session["Nombre"] = unAlumno.Nombre;
                return View("VerInasistencias");
            }

        }
        [HttpPost]
        public ActionResult ModificarInasistencias(List<Falta> unaFalta)
        {
            Falta unaFaltastatic = new Falta();
            unaFaltastatic.Modificar_Falta(unaFalta, (DateTime)TempData["Fecha"]);
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            return View("Inasistencias");
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
            List<Falta> listaFaltas = objFalta.TraerFaltas_X_Fecha(unaFalta.fecha, anio, letra, idMateria);
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
        [HttpPost]
        public ActionResult CargarNotas(List<Nota> UnaNota)
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Nota unaNotas = new Nota();
            unaNotas.Cargar_Nota(UnaNota, (string)TempData["trim"], (int)TempData["Materia"], (int)TempData["Divi"]);
            return View("Notas");
        }
        [HttpPost]
        public ActionResult ModificarNotas (List<Nota> UnaNota)
        {
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            Nota unaNota = new Nota();
            unaNota.Modificar_Nota(UnaNota, (string)TempData["trim"], (int)TempData["Materia"]);
            return View("Notas");
        }
        [HttpPost]
        public ActionResult AdminNotas( string trimestre, int anio, string Letra, int IdMateria)
        {
            Alumno unAlumno = new Alumno();
            Division unaDivision = new Division();
            int divi;
            divi = unaDivision.TraerIdDivision(anio, Letra);
            TempData.Clear();
            TempData.Add("Materia", IdMateria);
            TempData.Add("divi", divi);
            TempData.Add("trim", trimestre);
            TempData.Keep();
            ViewBag.listaalumnos = unAlumno.Listar_Alumnos_Falta(anio, Letra);
            ViewBag.anio = anio;
            ViewBag.letra = Letra;

            Materia mate = new Materia();
            string materia;
            materia = mate.TraerMateria(IdMateria);
            ViewBag.Materia = materia;
            Nota unaNota = new Nota();
            List<Nota> lista;
            lista = unaNota.ListarNotas();
            Boolean haynota = unaNota.HayUnaNota(trimestre, divi, IdMateria, lista);
            if(haynota == false)
            {
                return View("AltaNotas");

            }
            else
            {
                List<Nota> listaNotas = unaNota.TraerFaltas_X_todo(trimestre, divi, IdMateria);
                return View("ModifNotas", listaNotas);
            }
        }
        [HttpPost]
        public ActionResult ActualizarAnio(Falta unaFalta, int anio, string Letra, int IdMateria)
        {
            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.Listar_Alumnos_Falta(anio, Letra);
            Falta Unafalta = new Falta();
            Boolean falta;
            Division unaDivision = new Division();
            int divi;
            divi = unaDivision.TraerIdDivision(anio, Letra);
            ViewBag.Anio = anio;
            ViewBag.Letra = Letra;
            TempData.Clear();
            TempData.Add("divi", divi);
            TempData.Keep();

            Materia mate = new Materia();
            string materia;
            materia = mate.TraerMateria(IdMateria);
            ViewBag.Materia = materia;
            
            List<Falta> lista;
            lista = Unafalta.ListarFaltas();
            falta = Unafalta.HayUnaFalta(unaFalta, divi, IdMateria, lista );
            ViewBag.nombrefecha = unaFalta.fecha;
            Unafalta.IdDivision = divi;
            if (falta == true)
            {
                Falta objFalta = new Falta();
                List<Falta> listaFaltas = objFalta.TraerFaltas_X_Fecha(unaFalta.fecha, anio, Letra, IdMateria);
                TempData.Add("Fecha", unaFalta.fecha);
                return View("ModificarFaltaCurso", listaFaltas);
            }
            else
            {

                unaFalta.IdMateria = IdMateria;
                TempData.Add("Falta", unaFalta);
                TempData.Keep();
                return View();
            }
            
            
        }
        [HttpPost]
        public ActionResult CargarInasistencias(List<Falta> faltas)
        {

            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();




            Falta falta = new Falta();
            falta.Cargar_Falta( faltas, (Falta)TempData["Falta"], (int)TempData["divi"]);
      
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
            Materia unaMateria = new Materia();
            ViewBag.listamateria = unaMateria.listarmateria();
            ViewBag.listasanciones = unaSancion.ListarTodasSanciones();
            return View();
        }
        [HttpPost]
        public ActionResult ListaSanciones(int anio, string Letra)
        {
            Sancion unaSancion = new Sancion();
            ViewBag.listasanciones = unaSancion.ListarSanciones(anio, Letra);
            TempData.Clear();
            TempData.Add("anio", anio);
            TempData.Add("letra", Letra);
            TempData.Keep();
            ViewBag.anio = anio;
            ViewBag.Letra = Letra;
            return View();
        }

        public ActionResult Altasancion()
        {

            Alumno unAlumno = new Alumno();
            ViewBag.listaalumnos = unAlumno.ListarAlumnos((int)TempData["anio"], (string)TempData["letra"]);
            Division unaDivi = new Division();
            int IdDivi;
            IdDivi=unaDivi.TraerIdDivision((int)TempData["anio"], (string)TempData["letra"]);

            TempData.Add("IdDivi", IdDivi);
            TempData.Keep();

            return View();
        }
        [HttpPost]
        public ActionResult Altasancion(Sancion unasancion)
        {
            unasancion.Cargar_Sancion((int)TempData["IdDivi"]);
            return RedirectToAction("Sanciones");
        }

        public ActionResult EliminarSancion(int IdSancion)
        {
            Sancion san = new Sancion();
            san.EliminarSacion(IdSancion);

            return View("Sanciones");
        }
        [HttpPost]
        public ActionResult EliminarSancion()
        {
            return RedirectToAction("Sanciones");
        }

        public ActionResult VerMateriaFalta( DateTime fecha)
        {
            Falta unaFalta = new Falta();
            Materia unaMateria = new Materia();
            List<Falta> faltaAlumnoFecha = unaFalta.TraerFaltas_X_Fecha_por_IdAlumno(fecha, (int)TempData["IdAlumno"]);
            ViewBag.listafaltas = faltaAlumnoFecha;
            ViewBag.listamaterias = unaMateria.listarmateria();
            ViewBag.fecha = fecha;
            return View();

        }
        [HttpPost]

        public ActionResult AgregarNoticia(Noticias MiNoti)
        {
            //MiNoti.AgregarFoto();
            MiNoti.CargarNoticia();
            return View("Index");
        }
        [HttpGet]
        public ActionResult DetalleNoticia(int parametro)
        {
            Models.Noticias UnaNoti = new Models.Noticias();
            UnaNoti= UnaNoti.TraerUnaNoticia(parametro);
            return View(UnaNoti);
        }
        public ActionResult VerNotasAlumno()
        {
            int IdAlumno = (int)TempData["IdAlumno"];
            Nota unaNota = new Nota();
            List<Nota> ListaNotasAlumno = unaNota.ListarNotasXAlumno(IdAlumno);
            ViewBag.listanotas = ListaNotasAlumno;
            return View();
        }
        public ActionResult Alumnos()
        {
            return View("Alumnos");
        }
        [HttpPost]

        public ActionResult ListarAlumnos(int anio, string letra)
        {
            Division miDivi = new Division();
            int IdDivi = miDivi.TraerIdDivision(anio, letra);
            TempData.Clear();
            TempData.Add("Divi", IdDivi);
            TempData.Keep();
            Alumno MiAlumno = new Alumno();

            List<Alumno> listaAlumnos = new List<Alumno>();
            listaAlumnos = MiAlumno.ListarAlumnosConId(IdDivi);
            ViewBag.ListarAlumnos = listaAlumnos;
            ViewBag.anio = anio;
            ViewBag.letra = letra;
            return View("ListaAlumnos");
        }
        public ActionResult AgregarAlumno()
        {

            Division MiDivi = new Division();
            List<Division> lista = new List<Division>();

            lista = MiDivi.ListarDivisiones();
            ViewBag.ListarDivisiones = lista;
            
            return View();
        }

        [HttpPost]
        public ActionResult AgregarAlumno(Alumno UnAlumno)
        {
           
            UnAlumno.AgregarAlumno(UnAlumno.IdDivision);

            
            Alumno MiAlumno = new Alumno();

           
            List<Alumno> listaAlumnos = new List<Alumno>();
            listaAlumnos = MiAlumno.ListarAlumnosConId(UnAlumno.IdDivision);
            Division midivi = new Division();
            midivi = midivi.TraerDivision(UnAlumno.IdDivision);
            ViewBag.anio = midivi.Año;
            ViewBag.letra = midivi.Nombre;
            ViewBag.ListarAlumnos = listaAlumnos;
            return View("ListaAlumnos");

        }

        public ActionResult EliminarAlumno(int idAlumno)
        {
            Alumno MiAlumno = new Alumno();
            MiAlumno.EliminarAlumno(idAlumno);

            List<Alumno> listaAlumnos = new List<Alumno>();
            listaAlumnos = MiAlumno.ListarAlumnosConId((int)TempData["Divi"]);
            TempData.Keep();
            ViewBag.ListarAlumnos = listaAlumnos;
            return View("ListaAlumnos");
            
        }

        public ActionResult ModificarAlumno(int idAlumno)
        {
            Alumno miAlumno = new Alumno();
            miAlumno = miAlumno.TraerUnAlumnoo(idAlumno);

           
            Division MiDivi = new Division();
            List<Division> list = new List<Division>();

            list = MiDivi.ListarDivisiones();
            ViewBag.ListarDivisiones = list;
            ViewBag.Division = miAlumno.IdDivision;


            return View(miAlumno);
        }
        [HttpPost]

        public ActionResult ModificarAlumno(Alumno UnAlumno)
        {
            UnAlumno.ModificarAlumno(UnAlumno.IdDivision);

            Alumno MiAlumno = new Alumno();
            List<Alumno> listaAlumnos = new List<Alumno>();
            listaAlumnos = MiAlumno.ListarAlumnosConId(UnAlumno.IdDivision);
            ViewBag.ListarAlumnos = listaAlumnos;
            Division midivi = new Division();
            midivi = midivi.TraerDivision(UnAlumno.IdDivision);
            ViewBag.anio = midivi.Año;
            ViewBag.letra = midivi.Nombre;
            return View("ListaAlumnos");


        }

        public ActionResult VerAlumno(int idAlumno)
        {
            DetalleAlumno miAlu = new DetalleAlumno();
            miAlu= miAlu.TraerAlumno(idAlumno);
            TempData.Clear();
            TempData.Add("IdAlumno", idAlumno);
            TempData.Keep();

            int cantFalta, cantSancion;
            cantFalta = 0;
            cantSancion = 0;
            
            cantSancion = miAlu.CantidadSancion(idAlumno);
            cantFalta = miAlu.CantidadFaltas(idAlumno);
            ViewData["CantFaltas"] = cantFalta;
            ViewData["CantSanciones"] = cantSancion;
            
            return View(miAlu);
        }


    }

}
