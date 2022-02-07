using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class FacturaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Factura.GetAll();

            ML.Factura factura = new ML.Factura();
            factura.Facturas = result.Objects;

            return View(factura);
        }

        [HttpGet]
        public ActionResult NuevaFactura()
        {
            ML.Result resultCliente = BL.Cliente.GetAll();
            ML.Result resultProductos = BL.Producto.GetAll();

            ML.DetalleFactura detalleFactura = new ML.DetalleFactura();
            detalleFactura.Factura = new ML.Factura();
            detalleFactura.Factura.Cliente = new ML.Cliente();
            detalleFactura.Factura.Cliente.Clientes = resultCliente.Objects;

            detalleFactura.Producto = new ML.Producto();
            detalleFactura.Producto.Productos = resultProductos.Objects;
                                            
            return View(detalleFactura);
        }

        [HttpPost]
        public ActionResult Add(ML.DetalleFactura detalleFactura)
        {
            ML.Factura factura = new ML.Factura();
            factura.NumeroFactura = detalleFactura.Factura.NumeroFactura;            

            factura.Cliente = new ML.Cliente();
            factura.Cliente.IdCliente = detalleFactura.Factura.Cliente.IdCliente;

            ML.Result resultFactura = BL.Factura.Add(factura);
            int idFactura = ((int)resultFactura.Object);

            foreach (string producto in detalleFactura.Productos)
            {
                int idProducto = int.Parse(producto);
                BL.DetalleFactura.Add(idFactura, idProducto);

            }

            if (resultFactura.Correct) { 
                ViewBag.Mensaje = "Factura agregada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al agregar factura";
            }

            return PartialView("Modal");
        }

        
        
        [HttpGet]
        public ActionResult Delete(int idFactura)
        {
            ML.Result result = BL.Factura.Delete(idFactura);

            if (result.Correct)
            {
                ViewBag.Mensaje = "La factura se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar factura";
            }

            return PartialView("Modal");
        }
    }
}
