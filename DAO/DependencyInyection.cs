using DAO.Interface;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DependencyInyection : NinjectModule
    {
        public override void Load()
        {
            Bind<ICartasDAO>().To<CartasDAO>();
            Bind<IColeccionDAO>().To<ColeccionDAO>();
            Bind<IJugadoresDAO>().To<JugadoresDAO>();
            Bind<ISeriesDAO>().To<SeriesDAO>();
            Bind<IRarezaDAO>().To<RarezasDAO>();
        }
    }
}
