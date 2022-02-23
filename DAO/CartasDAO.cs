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
    public class CartasDAO : ICartasDAO
    {
        private const string QRmostrarCartas = @"SELECT	idCarta
                                                    ,[idSerie]
		                                            ,[descripcionRareza]
		                                            ,nombreJugadorEnCarta as nombre
		                                            ,apellidoJugadorEnCarta as apellido
		                                            ,nombreEquipo
		                                            ,descripcionRolJugador 
		                                            ,descripcionNacionalidad
                                                    ,active
                                                    ,color
                                                    ,urlImagenEnCarta as urlImagen
                                                    ,idJugador
		                                          FROM [dbo].[CARTAS] a

                                                JOIN RAREZAS ON a.idRareza=RAREZAS.idRareza
                                                JOIN EQUIPO ON a.idEquipoEnCarta=EQUIPO.idEquipo
                                                JOIN ROLJUGADOR on a.idRolJugadorEnCarta=ROLJUGADOR.idRolJugador
                                                JOIN NACIONALIDAD on a.idNacionalidadEnCarta=NACIONALIDAD.idNacionalidad";

        private const string QRmostrarCarta = @"SELECT	idCarta
                                                    ,[idSerie]
		                                            ,[descripcionRareza]
		                                            ,nombreJugadorEnCarta as nombre
		                                            ,apellidoJugadorEnCarta as apellido
		                                            ,nombreEquipo
		                                            ,descripcionRolJugador 
		                                            ,descripcionNacionalidad
                                                    ,active
                                                    ,color
                                                    ,urlImagenEnCarta as urlImagen
                                                    ,idJugador

		                                          FROM [dbo].[CARTAS] a

                                                JOIN RAREZAS ON a.idRareza=RAREZAS.idRareza
                                                JOIN EQUIPO ON a.idEquipoEnCarta=EQUIPO.idEquipo
                                                JOIN ROLJUGADOR on a.idRolJugadorEnCarta=ROLJUGADOR.idRolJugador
                                                JOIN NACIONALIDAD on a.idNacionalidadEnCarta=NACIONALIDAD.idNacionalidad
                                                WHERE idCarta=@id";

        private const string QRmostrarJugadorEnCarta = @"SELECT	
                                                                 [nombre]
		                                                        ,[apellido]
		                                                        ,[idEquipo]
		                                                        ,[idRolJugador]
		                                                        ,[idNacionalidad]
		                                                        ,[urlImagen]	
		                                                        FROM [dbo].[CARTAS] a
                                                        JOIN JUGADORES ON a.idJugador=JUGADORES.idJugador
                                                        WHERE idCarta= @idCarta
                                                        ";
        private const string QRmostrarIdJugadorEnCarta = @"SELECT [idJugador]
                                                            FROM [dbo].[CARTAS]
                                                            WHERE idCarta=@idCarta
                                                        ";

        private const string QRinsertarCarta = @"INSERT INTO CARTAS (idJugador,idMazo,idRareza,idSerie,active)
                                                values (@idJugador,@idMazo,@idRareza,@idSerie,1)";

        private const string QRinsertarCartaSinMazo = @"INSERT INTO CARTAS (idJugador,idRareza,idSerie,active)
                                                values (@idJugador,@idRareza,@idSerie,1)";


        private const string QRinsertCartasINColeccion = @"INSERT INTO COLECCIONES_CARTAS(idCarta,idColeccion)
                                                           values(@idCarta,@idColeccion)";

        private const string QRinsertarDatosACartas = @"UPDATE  CARTAS 
		                                                SET nombreJugadorEnCarta=@nombre, apellidoJugadorEnCarta=@apellido
                                                        ,idEquipoEnCarta=@idEquipo, idRolJugadorEnCarta=@idRolJugador
                                                        ,idNacionalidadEnCarta=@idNacionalidad,urlImagenEnCarta=@urlImagen
		                                                WHERE idCarta=@idCarta";

        private const string QRmostrarUltimoIDcarta = @"SELECT TOP (1) [idCarta] FROM [dbo].[CARTAS] ORDER BY idCarta DESC";

        private const string QRdeleteLogico = @"UPDATE CARTAS
                                                  SET active='0'
                                               WHERE idCarta=@idCarta";

        private const string QRupdateCarta = @"UPDATE CARTAS
                                               SET [idMazo] = @idMazo
                                                  ,[idRareza] = @idRareza
                                                  ,[idSerie] = @idSerie
                                             WHERE idCarta=@idCarta";

        private const string QRupdateCartaSinMazo = @"UPDATE CARTAS
                                               SET [idRareza] = @idRareza
                                                  ,[idSerie] = @idSerie
                                                WHERE idCarta=@idCarta";


        public IEnumerable<Carta> GetAll()
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var cartas = con.Query<Carta>(QRmostrarCartas).AsEnumerable();
                    return cartas;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Carta GetId(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var cartaEncontrada = con.QuerySingleOrDefault<Carta>(QRmostrarCarta, param: new { id = id });

                    return cartaEncontrada;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Jugador GetJugadorEnCarta(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var jugador = con.QuerySingleOrDefault<Jugador>(QRmostrarJugadorEnCarta, param: new { idCarta = id });
                    jugador.idJugador= con.QuerySingleOrDefault<int>(QRmostrarIdJugadorEnCarta, param: new { idCarta = id });
                    return jugador;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //INSERTAR CARTA SIN COLECCION
        public void Insert(int idJugador, int idMazo, int idRareza, int idSerie)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRinsertarCarta, param: new { idJugador = idJugador, idMazo = idMazo, idRareza = idRareza, idSerie = idSerie });
                    AuxiliarInsertar(con);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //INSERTAR CARTA SIN COLECCIONES NI MAZO

        public void Insert(int idJugador, int idRareza, int idSerie)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRinsertarCartaSinMazo, param: new { idJugador = idJugador, idRareza = idRareza, idSerie = idSerie });
                    AuxiliarInsertar(con);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //INSERTAR CARTA SIN MAZO CON COLECCIONES

        public void Insert(int idJugador, int idRareza, int idSerie, List<int> colecciones)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRinsertarCartaSinMazo, param: new { idJugador = idJugador, idRareza = idRareza, idSerie = idSerie });
                    int idCarta = con.QuerySingleOrDefault<int>(QRmostrarUltimoIDcarta);
                    foreach (int i in colecciones)
                    {
                        con.Execute(QRinsertCartasINColeccion, param: new { idCarta = idCarta, idColeccion = i });
                    }
                    AuxiliarInsertar(con);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //INSERTAR CARTA CON MAZO Y CON UNA O MAS COLECCIONES

        public void Insert(int idJugador, int idMazo, int idRareza, int idSerie, List<int> colecciones)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRinsertarCarta, param: new { idJugador = idJugador, idMazo = idMazo, idRareza = idRareza, idSerie = idSerie });
                    int idCarta = (int)con.QuerySingleOrDefault(QRmostrarUltimoIDcarta);
                    foreach (var i in colecciones)
                    {
                        con.Execute(QRinsertCartasINColeccion, param: new { idCarta = idCarta, idColeccion = i });
                    }
                    AuxiliarInsertar(con);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void AuxiliarInsertar(System.Data.IDbConnection con)
        {
            int id = con.QuerySingleOrDefault<int>(QRmostrarUltimoIDcarta);
            var jugador = con.QuerySingleOrDefault<Jugador>(QRmostrarJugadorEnCarta, param: new { idCarta = id });
            con.Execute(QRinsertarDatosACartas, param: new { nombre = jugador.nombre, apellido = jugador.apellido, idEquipo = jugador.idEquipo, idRolJugador = jugador.idRolJugador, idNacionalidad = jugador.idNacionalidad, urlImagen = jugador.urlImagen, idCarta = id });
        }
        //ELIMINADO LOGICO
        public void DeleteLogico(int idCarta)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRdeleteLogico, param: new { idCarta = idCarta });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //UPDATE SIN MAZO Y SIN COLECCIONES

        public void Update(int idCarta,int idRareza,int idSerie)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRupdateCartaSinMazo, param: new { idCarta = idCarta,idRareza=idRareza,idSerie = idSerie });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //UPDATE CON MAZO Y SIN COLECCIONES
        public void Update(int idCarta, int idMazo, int idRareza, int idSerie)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRupdateCarta, param: new { idCarta = idCarta, idMazo = idMazo, idRareza = idRareza, idSerie = idSerie });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //UPDATE SIN MAZO CON COLECCIONES

        public void Update(int idCarta, int idRareza, int idSerie, List<int> colecciones)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRupdateCartaSinMazo, param: new { idCarta = idCarta, idRareza = idRareza, idSerie = idSerie });
                    foreach (var i in colecciones)
                    {
                        con.Execute(QRinsertCartasINColeccion, param: new { idCarta = idCarta, idColeccion = i });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Update(int idCarta,int idMazo, int idRareza, int idSerie, List<int> colecciones)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    con.Execute(QRupdateCartaSinMazo, param: new { idCarta = idCarta, idRareza = idRareza, idSerie = idSerie });
                    foreach (var i in colecciones)
                    {
                        con.Execute(QRinsertCartasINColeccion, param: new { idCarta = idCarta, idColeccion = i });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
