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
    public class JugadoresDAO : IJugadoresDAO
    {
        private const string QRmostrarJugadores = @"SELECT idJugador
                                                      ,[nombre]
                                                      ,[apellido]
                                                      ,[idEquipo]
                                                      ,[idRolJugador]
                                                      ,[idNacionalidad]
                                                      ,[urlImagen]
                                                  FROM  [dbo].[JUGADORES]";

        private const string QRmostrarJugador = @"SELECT idJugador
                                                      ,[nombre]
                                                      ,[apellido]
                                                      ,[idEquipo]
                                                      ,[idRolJugador]
                                                      ,[idNacionalidad]
                                                      ,[urlImagen]
                                                  FROM  [dbo].[JUGADORES]
                                                  WHERE idJugador=@id";

        private const string QRbuscarJugadoresEnSerie = @"SELECT  idJugador
                                                            ,nombreJugadorEnCarta	AS nombre	 
                                                            ,apellidoJugadorEnCarta	AS apellido
                                                            
                                                        FROM [Extra2].[dbo].[CARTAS]
                                                        WHERE idSerie = @idSerie";
        
        private const string QRbuscarJugadoresParaFiltrarSeries = @"SELECT idJugador
                                                      ,[nombre]
                                                      ,[apellido]
                                                  FROM  [dbo].[JUGADORES]";

        public IEnumerable<Jugador> GetAll()
        {
            try
            {
                using(var con = ConexionBD.AbrirConexion())
                {
                    var jugadores = con.Query<Jugador>(QRmostrarJugadores).AsEnumerable();
                    return jugadores;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Jugador GetId(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var jugador = con.QuerySingleOrDefault<Jugador>(QRmostrarJugador, param: new { id = id });
                    return jugador;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Jugador> GetAllInSerie(int idSerie)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var jugadoresEnSerie = con.Query<Jugador>(QRbuscarJugadoresEnSerie, param: new { idSerie = idSerie }).ToList();
                    var todosJugadores = con.Query<Jugador>(QRbuscarJugadoresParaFiltrarSeries).ToList();
                    var aux = new List<Jugador>(todosJugadores);
                    foreach (var i in jugadoresEnSerie)
                    {
                        foreach (var j in todosJugadores)
                        {
                            if (i.nombre==j.nombre && i.apellido==j.apellido)
                            {
                                aux.Remove(j);
                            }
                        }
                    }
                    return aux;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
