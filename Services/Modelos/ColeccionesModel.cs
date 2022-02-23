using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Servicios
{
    public class ColeccionesModel
    {
        

        public List<SelectListItem> colecciones { get; set; }
        public int[] coleccionesIDs { get; set; }
    }
}
