using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioModel
    {
        public UsuarioModel(string usernameUsuario, string passwordUsuario, string email, string descripcion, int idMazo)
        {
            this.usernameUsuario = usernameUsuario;
            this.passwordUsuario = passwordUsuario;
            this.email = email;
            this.descripcion = descripcion;
            this.idMazo = idMazo;
        }

        public string usernameUsuario { get; }
        public string passwordUsuario { get;}
        public string email { get;  }
        public string descripcion { get;  }
        public int idMazo { get;  }
    }
}
