using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public List<Object> Clientes { get; set; }
        public string NombreCompleto { get; set; }
    }
}
