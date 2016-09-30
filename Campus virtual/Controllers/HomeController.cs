using Campus_virtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus_virtual.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Log_Out()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {

            Noticias UnaNoti = new Noticias();

            List<Noticias> lista = UnaNoti.ListarNoticias();
            ViewBag.ListarNoticias = lista;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

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



            Noticias MiFoto = new Noticias();
            MiFoto.Foto = MiNoticia.Foto;
            // MiFoto.AgregarFoto();

            MiNoticia.ModificarNoticia();

            Noticias UnaNoti = new Noticias();

            List<Noticias> lista = UnaNoti.ListarNoticias();
            ViewBag.ListarNoticias = lista;

            
            return View("Index");
        }

        public ActionResult AgregarNoticia(Noticias MiNoti)
        {
            //MiNoti.AgregarFoto();
            MiNoti.CargarNoticia();
            return View("Index");
        }
    }
}