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
    public class UsuariosDAO : IUsuariosDAO
    {
        private const string QRmostrarUsuarios = @"SELECT [idUsuario]
                                              ,[usernameUsuario]
                                              ,[passwordUsuario]
                                              ,[email]
                                              ,descripcion
                                              ,[idMazo]
                                                FROM [Extra2].[dbo].[USUARIO]";

        public IEnumerable<Usuario> GetAll()
        {

            try
            {
                using (var con = ConexionBD.AbrirConexion())
                {
                    var usuariosAnonimo = con.Query<Usuario>(QRmostrarUsuarios).AsEnumerable();
                    return usuariosAnonimo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
