using Campus_virtual.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);

                    file.SaveAs(path);

                    MiNoticia.Foto = fileName;
                }
            }
            catch(Exception Error)
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
    }
}