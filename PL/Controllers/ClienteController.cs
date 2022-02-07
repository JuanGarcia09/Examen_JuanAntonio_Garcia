using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cliente.GetAll();

            ML.Cliente cliente = new ML.Cliente();
            cliente.Clientes = result.Objects;

            return View(cliente);
        }

        [HttpGet]
        public ActionResult Form(int? idCliente)
        {
            if (idCliente == null) //agregar
            {
                return View();
            }
            else //actualizar
            {
                ML.Cliente cliente = new ML.Cliente();

                ML.Result result = BL.Cliente.GetById(idCliente.Value);

                if (result.Correct)
                {
                    cliente = (ML.Cliente)result.Object;
                    return View(cliente);
                }
                else
                {
                    return View(cliente);
                }
            }            
        }

        [HttpPost]
        public ActionResult Form(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            if (cliente.IdCliente == 0)
            {
                result = BL.Cliente.Add(cliente);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Cliente agregado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar cliente";
                }
            }
            else
            {
                result = BL.Cliente.Update(cliente);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Cliente actulizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar cliente";
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idCliente)
        {            
            ML.Result result = BL.Cliente.Delete(idCliente);

            if (result.Correct)
            {
                ViewBag.Mensaje = "El cliente se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar cliente";
            }

            return PartialView("Modal");
        }
    }
}
