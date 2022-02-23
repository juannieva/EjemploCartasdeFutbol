using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class JugadorModel
    {
        public JugadorModel(int idJugador, string nombreApellido, int idEquipo, int idRolJugador, int idNacionalidad)
        {
            this.idJugador = idJugador;
            this.nombreApellido = nombreApellido;
            this.idEquipo = idEquipo;
            this.idRolJugador = idRolJugador;
            this.idNacionalidad = idNacionalidad;
        }

        public int idJugador { get; }
        public string nombreApellido { get; }
        public int idEquipo { get; }
        public int idRolJugador { get;  }
        public int idNacionalidad { get;  }
    }
}
