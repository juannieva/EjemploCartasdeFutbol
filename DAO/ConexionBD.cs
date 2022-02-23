using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public static class ConexionBD
    {
        private static readonly string _connectionString = "Data Source=.;Initial Catalog=Extra2;Integrated Security=True;TrustServerCertificate=True";
        public static IDbConnection AbrirConexion(bool opened = false)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            if (opened) connection.Open();
            return connection;
        }
    }
}
