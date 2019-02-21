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
        //Operaciones de empleado ------------------------------------------------------------------------------------------------


        public ActionResult Index()
        {
            return View(Data.Instance.ListaEmpleados);
        }

        
        public ActionResult Create()
        {
            return View();
        }
        
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

        //Operaciones de empleados en parqueo, así como el control de entrada y salida de los empleados------------------------------------------------------------------

        public ActionResult Parqueo()
        {
            return View(Data.Instance.PilaEmpleados);
        }

        public ActionResult RegistrarEntrada()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEntrada(FormCollection collection)
        {
            try
            {
                var codigo = collection["CodigoEmpleado"];

                Predicate<Empleado> BuscadorEmpleado = (Empleado emp) => { return emp.CodigoEmpleado == codigo; };

                foreach (var emp in Data.Instance.ListaEmpleados)
                {
                    if (emp.CodigoEmpleado == codigo)
                    {
                        emp.AsignarCitas();
                        emp.CalcularHoras(Data.Instance.PilaEmpleados.Count);
                        emp.Disponible = true;
                    }
                }

                var Empleado = Data.Instance.ListaEmpleados.Find(BuscadorEmpleado);


                if (Empleado != null)
                {
                    Data.Instance.PilaEmpleados.Add(Empleado);
                    return RedirectToAction("Parqueo");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult SimularSalida()
        {
            var codigoAux = Data.Instance.PilaEmpleados.Get().CodigoEmpleado;
            foreach (var emp in Data.Instance.ListaEmpleados)
            {
                if (emp.CodigoEmpleado == codigoAux)
                {
                    emp.Disponible = false;
                }
            }

            Data.Instance.PilaEmpleados.Pop();
            
            return RedirectToAction("Parqueo");
        }

        public ActionResult Busqueda_Nombre()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Busqueda_Nombre(FormCollection collection)
        {
            var nombre = collection["Nombre"];

            Predicate<Empleado> BuscadorEmpleado = (Empleado emp) => { return emp.Nombre == nombre;};

            var Empleado = Data.Instance.ListaEmpleados.Find(BuscadorEmpleado);

            return RedirectToAction("Busqueda_Nombre");
        }
    }
}