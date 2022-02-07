using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class DetalleFacturaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return RedirectToAction("GetAll", "Factura");
        }

        [HttpGet]
        public ActionResult DetalleFactura(int idFactura)
        {
            ML.Result resultFactura = BL.Factura.GetById(idFactura);
            ML.Result resultDetalles = BL.DetalleFactura.GetByIdFactura(idFactura);

            ML.DetalleFactura detalleFactura = new ML.DetalleFactura();                  
            detalleFactura.Factura = new ML.Factura();
            detalleFactura.Factura = (ML.Factura)resultFactura.Object;
            detalleFactura.IdDetalleFactura = detalleFactura.Factura.IdDetalle;

            detalleFactura.Producto = new ML.Producto();
            detalleFactura.Producto.Productos = resultDetalles.Objects;

            return View(detalleFactura);
        }
        
        [HttpGet]
        public ActionResult ProductosNoAsignadosByIdDetalle(int idFactura, int idDetalle)
        {
            ML.Result resultFactura = BL.Factura.GetById(idFactura);
            ML.Result resultProductos = BL.DetalleFactura.ProductoNoAsignadoByIdDetalle(idDetalle);

            ML.DetalleFactura detalleFactura = new ML.DetalleFactura();
            detalleFactura.Factura = new ML.Factura();
            detalleFactura.Factura = (ML.Factura)resultFactura.Object;

            detalleFactura.Producto = new ML.Producto();
            detalleFactura.Producto.Productos = resultProductos.Objects;
           

            return View(detalleFactura);
        }

        [HttpPost]
        public ActionResult ProductosFacturaAdd(ML.DetalleFactura detalleFactura)
        {
            foreach (string producto in detalleFactura.Productos)
            {
                int idProducto = int.Parse(producto);
                BL.DetalleFactura.ProductoFacturaAdd(detalleFactura.Factura.IdFactura,idProducto);
            }

            return RedirectToAction("DetalleFactura", "DetalleFactura", new { idFactura = detalleFactura.Factura.IdFactura });
        }

        [HttpGet]
        public ActionResult ProductoDeleteByIdFactura(int idDetalle, int idFactura)
        {
            ML.DetalleFactura detalleFactura = new ML.DetalleFactura();
            detalleFactura.Factura = new ML.Factura();         
            detalleFactura.Factura.IdFactura = idFactura;

            detalleFactura.IdDetalleFactura = idDetalle;

            ML.Result result = BL.DetalleFactura.ProductoDeleteByIdDetalleFactura(idDetalle);

            ViewBag.ProductosAsignados = true;
            ViewBag.idFactura= idFactura;

            if (result.Correct)
            {
                ViewBag.Mensaje = "Producto eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar correctamente";
            }

            return PartialView("Modal");
        }

    }
}
