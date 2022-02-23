using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Services.Modelos
{
    public class ListaOpciones
    {
        public List<SelectListItem> colecciones { get; set; }
        public List<SelectListItem> jugadores { get; set; }
        public List<SelectListItem> series { get; set; }
        public List<SelectListItem> rarezas { get; set; }
    }
}
