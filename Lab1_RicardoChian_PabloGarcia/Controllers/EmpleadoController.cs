using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab1_RicardoChian_PabloGarcia.Helpers;
using Lab1_RicardoChian_PabloGarcia.Models;

namespace Lab1_RicardoChian_PabloGarcia.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            return View(Data.Instance.ListaEmpleados);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Data.Instance.ListaEmpleados.Add(new Empleado(collection["Nombre"],collection["CodigoEmpleado"]));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
