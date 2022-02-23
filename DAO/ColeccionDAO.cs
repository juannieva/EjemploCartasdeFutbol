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
    public class ColeccionDAO : IColeccionDAO
    {
        private const string QRmostrarColecciones = @"SELECT  [idColeccion] ,[descripcionColeccion]
                                                         FROM [dbo].[COLECCIONES]";

        private const string QRmostrarColeccion = @"SELECT  [idColeccion] ,[descripcionColeccion]
                                                         FROM [dbo].[COLECCIONES]
                                                         WHERE idColeccion=@id";

        private const string QRmostrarCartasINColeccion = @"SELECT [idCarta]
                                                             ,[idColeccion]
                                                         FROM [dbo].[COLECCIONES_CARTAS]";
        private const string QRmostrarCartaINColeccion = @"SELECT [idCarta]
                                                             ,[idColeccion]
                                                         FROM [dbo].[COLECCIONES_CARTAS]
                                                         WHERE idCarta=@id";

        private const string QReliminarCartaINColeccion = @"DELETE FROM[dbo].[COLECCIONES_CARTAS]
                                                            WHERE (idCarta = @idCarta) AND(idColeccion= @idColeccion)";

        private const string QReliminarAllCartaINColeccion = @"DELETE FROM[dbo].[COLECCIONES_CARTAS]
                                                            WHERE (idCarta = @idCarta)";


        public IEnumerable<Coleccion> GetAll()
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Query<Coleccion>(QRmostrarColecciones).AsEnumerable();
                    return colecciones;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public Coleccion GetId(int id)
        {

            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var cartaEncontrada = con.QuerySingleOrDefault<Coleccion>(QRmostrarColeccion, param: new { id = id });

                    return cartaEncontrada;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        //CartasINColeccion METODOS


        public IEnumerable<CartasINColeccion> GetAllCartasINColeccion()
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Query<CartasINColeccion>(QRmostrarCartasINColeccion).AsEnumerable();
                    return colecciones;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public List<int> GetColeccionesINCarta(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Query<CartasINColeccion>(QRmostrarCartaINColeccion,param:new { id=id }).ToList();
                    List<int> coleccionesEncarta=new List<int>();
                    foreach (var i in colecciones)
                    {
                        if (i.idCarta == id)
                        {
                            coleccionesEncarta.Add(i.idColeccion);
                        }
                    }
                    return coleccionesEncarta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCartaINColeccion(int id, int coleccion)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Execute(QReliminarCartaINColeccion, param:new{idCarta=id,idColeccion=coleccion });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteAllCartaINColeccion(int id)
        {
            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var colecciones = con.Execute(QReliminarAllCartaINColeccion, param: new { idCarta = id });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
