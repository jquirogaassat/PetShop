using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {
        public Usuario() { }

        public Usuario(string contraseña, bool webMaster)
        {
            Contraseña = contraseña;
            WebMaster = webMaster;
        }
        public long IDUsuario { get; set; }
        public string Contraseña { get; set; }
        public bool WebMaster { get; set; }
        public int DigitoVerificador { get; set; }
        public bool TieneAccesso { get; set; }
    }
}
