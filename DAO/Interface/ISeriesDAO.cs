using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface ISeriesDAO
    {
        IEnumerable<Serie> GetAll();
        Serie GetId(int id);
    }
}
