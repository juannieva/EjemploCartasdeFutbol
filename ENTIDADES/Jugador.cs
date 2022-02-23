using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Jugador
    {
        public int idJugador { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idEquipo { get; set; }
        public int idRolJugador { get; set; }
        public int idNacionalidad { get; set; }
        public string urlImagen { get; set; }
    }
}
