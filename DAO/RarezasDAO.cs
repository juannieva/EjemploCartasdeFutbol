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
    public class RarezasDAO : IRarezaDAO
    {
        private const string QRMostrarRarezas = @"SELECT  [idRareza]
                                                      ,[descripcionRareza]
                                                      ,[color]
                                                  FROM [dbo].[RAREZAS]";
        private const string QRMostrarRareza = @"SELECT  [idRareza]
                                                      ,[descripcionRareza]
                                                      ,[color]
                                                  FROM [dbo].[RAREZAS]
                                                  WHERE idRareza=@id";
        public IEnumerable<Rareza> GetAll()
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Query<Rareza>(QRMostrarRarezas).AsEnumerable();
                    return colecciones;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Rareza GetId(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var rareza = con.QuerySingleOrDefault<Rareza>(QRMostrarRareza, param: new { id = id });
                    return rareza;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
