using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult CrearPorArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearPorArchivo(HttpPostedFileBase postedFile)
        {
            var FilePath = string.Empty;

            if (postedFile != null)
            {
                var path = Server.MapPath("~/CargaCSV/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FilePath = path + Path.GetFileName(postedFile.FileName);
                
                postedFile.SaveAs(FilePath);

                var CsvData = System.IO.File.ReadAllText(FilePath);
                

                foreach (var fila in CsvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(fila))
                    {
                        var filaSecundaria = fila.Split('\r');
                        var s = fila;
                        s = filaSecundaria[0];
                        Data.Instance.ListaEmpleados.Add(new Empleado(s.Split(',')[0], s.Split(',')[1]));
                    }
                }
                System.IO.File.Delete(FilePath);
                Directory.Delete(path);
            }
            return RedirectToAction("Index");
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

        //Búsquedas
        public ActionResult Busquedas()
        {
            if (TempData.Count != 0)
            {
                ViewBag.Nombre = TempData["nombre"].ToString();
                ViewBag.Codigo = TempData["codigo"].ToString();
                //TempData["dsponible"] = Empleado.Disponible;
                ViewBag.HoraEntrada = TempData["HoraEntrada"];
                ViewBag.HoraSalida = TempData["HoraSalida"].ToString();
                ViewBag.HorasTrabajadas = TempData["HorasTrabajadas"];
                ViewBag.Sueldo = TempData["sueldo"].ToString();
                ViewBag.Citas = TempData["citas"].ToString();
                return View();
            }
            //else
            //{
            //    ViewBag.Nombre = TempData["nombre"].ToString();
            //    ViewBag.Codigo = TempData["codigo"].ToString();
            //    //TempData["dsponible"] = Empleado.Disponible;
            //    ViewBag.HoraEntrada = TempData["HoraEntrada"].ToString();
            //    ViewBag.HoraSalida = TempData["HoraSalida"].ToString();
            //    ViewBag.HorasTrabajadas = TempData["HorasTrabajadas"].ToString();
            //    ViewBag.Sueldo = TempData["sueldo"].ToString();
            //    ViewBag.Citas = TempData["citas"].ToString();
            //}
            return View();
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

            if (Empleado != null)
            {
                TempData["nombre"] = Empleado.Nombre;
                TempData["codigo"] = Empleado.CodigoEmpleado;
                //TempData["disponible"] = Empleado.Disponible;
                TempData["HoraEntrada"] = Empleado.HoraDeEntrada.Hour;
                TempData["HoraSalida"] = Empleado.HoraDeSalida.Hour;
                TempData["HorasTrabajadas"] = Empleado.HorasTrabajadas.Hours;
                TempData["sueldo"] = Empleado.Sueldo;
                TempData["citas"] = Empleado.CantidaCitas;
                return RedirectToAction("Busquedas");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Busqueda_Codigo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Busqueda_Codigo(FormCollection collection)
        {
            var codigo = collection["CodigoEmpleado"];

            Predicate<Empleado> BuscadorEmpleado = (Empleado emp) => { return emp.CodigoEmpleado == codigo; };

            var Empleado = Data.Instance.ListaEmpleados.Find(BuscadorEmpleado);

            if (Empleado != null)
            {
                TempData["nombre"] = Empleado.Nombre;
                TempData["codigo"] = Empleado.CodigoEmpleado;
                //TempData["disponible"] = Empleado.Disponible;
                TempData["HoraEntrada"] = Empleado.HoraDeEntrada.Hour;
                TempData["HoraSalida"] = Empleado.HoraDeSalida.Hour;
                TempData["HorasTrabajadas"] = Empleado.HorasTrabajadas.Hours;
                TempData["sueldo"] = Empleado.Sueldo;
                TempData["citas"] = Empleado.CantidaCitas;
                return RedirectToAction("Busquedas");
            }
            else
            {
                return View();
            }
        }
    }
}