using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Modelos
{
    public class InsertCartaModel : IValidatableObject
    {

        [Required(ErrorMessage = "Seleccione un Jugador")]
        [DisplayName("Jugador")]
        public int idJugador { get; set; }
        [Required(ErrorMessage = "Seleccione una Rareza")]
        [DisplayName("Rareza")]
        public int idRareza { get; set; }
        [Required(ErrorMessage = "Seleccione una Serie")]
        [DisplayName("Serie")]
        public int idSerie { get; set; }
        public int idMazo { get; set; }
        public List<int> colecciones { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errores = new List<ValidationResult>();

            if (idJugador == 0)
            {
                errores.Add(new ValidationResult("Seleccione un Jugador", new string[] { "idJugador" }));
            }
            if (idSerie == 0)
            {
                errores.Add(new ValidationResult("Seleccione una Serie", new string[] { "idSerie" }));
            }
            if (idRareza == 0)
            {
                errores.Add(new ValidationResult("Seleccione una Rareza", new string[] { "idRareza" }));
            }
            return errores;
        }
    }
}
