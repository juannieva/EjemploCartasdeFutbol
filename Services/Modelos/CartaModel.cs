using Servicios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class CartaModel
    {
        public int id { get; }
        public int serie { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string equipo { get; set; }
        public string nacionalidad { get; set; }
        public string rolJugador { get; set; }
        public string colorRareza { get; set; }
        public string rareza { get; set; }
        public string urlImagen { get; set; }
        public List<ColeccionModel> colecciones { get; set; }
        public int active { get; set; }


        public CartaModel(int id, int serie, string nombre, string apellido, string equipo,string nacionalidad, string rolJugador, string colorRareza, string rareza,string urlImagen,int active)
        {
            this.id = id;
            this.serie = serie;
            this.nombre = nombre;
            this.apellido = apellido;
            this.equipo = equipo;
            this.nacionalidad = nacionalidad;
            this.rolJugador = rolJugador;
            this.colorRareza = colorRareza;
            this.rareza = rareza;
            this.urlImagen = urlImagen;
            colecciones = new List<ColeccionModel>();
            this.active = active;
        }

        
    }
}
