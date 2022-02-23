using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IRarezaDAO
    {
        IEnumerable<Rareza> GetAll();
        Rareza GetId(int id);
    }
}
