using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; }
        public string FechaHora { get; set; }
        public ML.Cliente Cliente { get; set; }
        public List<Object> Facturas { get; set; }
        public int IdDetalle { get; set; }
    }
}
