
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private ServicioCartas _servicioCartas;

        public HomeController()
        {
            _servicioCartas = new ServicioCartas();

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            List<CartaModel> cartas = _servicioCartas.BuscarCartas();
            return View(cartas);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}