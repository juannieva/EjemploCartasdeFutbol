using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Carta
    {
        public int idCarta { get; set; }
        public int idSerie { get; set; }
        public int idRareza { get; set; }
        public int idJugador { get; set; }
        public string descripcionRareza { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreEquipo { get; set; }
        public string descripcionRolJugador { get; set; }
        public string descripcionNacionalidad { get; set; }
        public int active { get; set; }
        public string color { get; set; }
        public string urlImagen { get; set; }

    }
}
