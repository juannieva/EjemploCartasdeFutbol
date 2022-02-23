using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Modelos
{
    public class ColeccionModel
    {
        public int idColeccion { get;  }
        public string descripcionColeccion { get; }

        public ColeccionModel(int idColeccion, string descripcionColeccion)
        {
            this.idColeccion = idColeccion;
            this.descripcionColeccion = descripcionColeccion;
        }
    }
}
