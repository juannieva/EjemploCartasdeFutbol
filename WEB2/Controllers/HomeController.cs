using Newtonsoft.Json;
using Servicios;
using Servicios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        [HttpGet]
        public ActionResult AddCard()
        {
            ViewBag.listaOpciones = _servicioCartas.ConvertirListasASelectListItem();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddCard(InsertCartaModel carta,string[] DDColecciones)
        {
            if (DDColecciones != null)
            {
                carta.colecciones = new List<int>();
                foreach (string i in DDColecciones)
                {
                    carta.colecciones.Add(int.Parse(i));
                }
            }
            
            if (ModelState.IsValid)
            {
                _servicioCartas.InsertCarta(carta);
                return RedirectToAction("Index");
            }else
            ViewBag.listaOpciones = _servicioCartas.ConvertirListasASelectListItem();
            if(carta.idSerie!=0)
            {
                ViewBag.listaOpciones.jugadores = _servicioCartas.JugadoresEnSerieError(carta.idSerie);
            }
            return View(carta);
        }

        
        public JsonResult ActualizarJugadoresSeries(int codigo)
        {
            var data = JsonConvert.SerializeObject(_servicioCartas.JugadoresEnSerie(codigo));
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration=60)]
        [HttpGet]
        public ActionResult VerCartas()
        {
            ViewBag.Cartas = _servicioCartas.BuscarCartas();
            return View();
        }
        
        [HttpPost]
        public ActionResult VerCartas(string submitButton, string postId)
        {
            switch (submitButton)
            {
                case "Eliminar":
                    _servicioCartas.DeleteLogico(int.Parse(postId));
                    string path = Url.Action("VerCartas");
                    Response.RemoveOutputCacheItem(path);
                    break;
                case "Editar":
                    return RedirectToAction("UpdateCartas", "Home", new { postID = postId });
                default:
                    break;
            }
            ViewBag.Cartas = _servicioCartas.BuscarCartas();
            return View();
        }

        
        [HttpGet]
        public ActionResult UpdateCartas(int postId) {
            ViewBag.listaOpciones = _servicioCartas.ConvertirListasASelectListItem(postId);
            ViewBag.id = postId;
            return View();
        }
        
        [HttpPost]
        public ActionResult UpdateCartas(InsertCartaModel carta, string[] DDColecciones, int postId)
        {
            if (DDColecciones != null)
            {
                carta.colecciones = new List<int>();
                foreach (string i in DDColecciones)
                {
                    carta.colecciones.Add(int.Parse(i));
                }
            }

            if (ModelState.IsValid)
            {
                _servicioCartas.Update(postId, carta);
                string path = Url.Action("VerCartas");
                Response.RemoveOutputCacheItem(path);
                return RedirectToAction("VerCartas");
            }
            else
            ViewBag.id = postId;
            ViewBag.listaOpciones = _servicioCartas.ConvertirListasASelectListItem(postId);
            return View();
        }
        
        

        public ActionResult Cartas()
        {
            return View();
        }

    }
}