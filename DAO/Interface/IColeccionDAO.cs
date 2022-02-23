using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IColeccionDAO
    {
        IEnumerable<Coleccion> GetAll();
        Coleccion GetId(int id);
        IEnumerable<CartasINColeccion> GetAllCartasINColeccion();
        List<int> GetColeccionesINCarta(int id);
        void DeleteCartaINColeccion(int id, int coleccion);
        void DeleteAllCartaINColeccion(int id);
    }
}
