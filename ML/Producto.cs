using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public List<Object> Productos { get; set; }

    }
}
