using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bitacora
    {
        public Bitacora() {}

        public Bitacora(int pId)
        {
            bitacora_id = pId;
        }

        public Bitacora(int bitacoraId, string cuentaUsuario, string criticidad, string description ,DateTime pFecha)
        {
            bitacora_id = bitacoraId;
            cuenta_usuario = cuentaUsuario;
            bitacora_criticidad = criticidad;
            bitacora_fecha = pFecha;
            descripcion = description;
        }

        public int bitacora_id { get; set; }
        public string cuenta_usuario { get; set; }
        public string bitacora_criticidad { get; set; }
        public string descripcion { get; set; }
        public DateTime bitacora_fecha { get; set; }

    }
}

