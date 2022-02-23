using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RarezaModel
    {
        public RarezaModel(int idRareza, string descripcionRareza)
        {
            this.idRareza = idRareza;
            this.descripcionRareza = descripcionRareza;
        }

        public int idRareza { get;  }
        public string descripcionRareza { get; }
    }
}
