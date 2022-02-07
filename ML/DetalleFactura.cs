using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public ML.Factura Factura { get; set; }
        public ML.Producto Producto { get; set; }        
        public List<Object> Productos { get; set; }
    }
}
