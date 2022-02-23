using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IJugadoresDAO
    {
        IEnumerable<Jugador> GetAll();
        Jugador GetId(int id);
        List<Jugador> GetAllInSerie(int idSerie);
    }
}
