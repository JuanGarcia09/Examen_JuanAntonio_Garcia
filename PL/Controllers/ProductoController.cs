using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();

            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;

            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? idProducto)
        {
            if (idProducto == null) //agregar
            {
                return View();
            }
            else //actualizar
            {
                ML.Producto producto= new ML.Producto();

                ML.Result result = BL.Producto.GetById(idProducto.Value);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    return View(producto);
                }
                else
                {
                    return View(producto);
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            if (producto.IdProducto == 0)
            {
                result = BL.Producto.Add(producto);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Producto agregado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar producto";
                }
            }
            else
            {
                result = BL.Producto.Update(producto);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Producto actulizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar producto";
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idProducto)
        {
            ML.Result result = BL.Producto.Delete(idProducto);

            if (result.Correct)
            {
                ViewBag.Mensaje = "El producto se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar producto";
            }

            return PartialView("Modal");
        }
    }
}
