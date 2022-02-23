using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SerieModel
    {
        public SerieModel(int id, string descripcion)
        {
            this.idSerie = id;
            this.descripcion = descripcion;
        }
        public int idSerie { get;  }
        public string descripcion { get; }
    }
}
