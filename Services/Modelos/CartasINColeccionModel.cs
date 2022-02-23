using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class CartasINColeccionModel
    {
        public int idColeccion { get;  }
        public int idCarta { get;}

        public CartasINColeccionModel(int idColeccion, int idCarta)
        {
            this.idColeccion = idColeccion;
            this.idCarta = idCarta;
        }
    }
}
