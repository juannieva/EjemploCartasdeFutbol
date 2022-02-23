using DAO.Interface;
using Dapper;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SeriesDAO : ISeriesDAO
    {
        private const string QRmostrarSeries = @"SELECT [idSerie]
                                                      ,[numSerie]
                                                      ,[fechaSalidaSerie]
                                                  FROM [dbo].[SERIES]";

        private const string QRmostrarSerie = @"SELECT [idSerie]
                                                      ,[numSerie]
                                                      ,[fechaSalidaSerie] 
                                                  FROM [dbo].[SERIES]
                                                  WHERE idSerie=@id";


        public IEnumerable<Serie> GetAll()
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var series = con.Query<Serie>(QRmostrarSeries).AsEnumerable();
                    return series;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Serie GetId(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var serie = con.QuerySingleOrDefault<Serie>(QRmostrarSerie,param: new { id= id });
                    return serie;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
