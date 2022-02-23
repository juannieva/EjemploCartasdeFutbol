using System;
using ENTIDADES;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface ICartasDAO
    {
        IEnumerable<Carta> GetAll();
        Carta GetId(int id);
        Jugador GetJugadorEnCarta(int id);
        void Insert(int idJugador, int idRareza, int idSerie);
        void Insert(int idJugador, int idMazo, int idRareza, int idSerie);
        void Insert(int idJugador, int idRareza, int idSerie, List<int> colecciones);
        void Insert(int idJugador, int idMazo, int idRareza, int idSerie, List<int> colecciones);
        void Update(int idCarta, int idRareza, int idSerie);
        void Update(int idCarta, int idMazo, int idRareza, int idSerie);
        void Update(int idCarta, int idRareza, int idSerie, List<int> colecciones);
        void Update(int idCarta, int idMazo, int idRareza, int idSerie, List<int> colecciones);
        void DeleteLogico(int idCarta);
    }
}
