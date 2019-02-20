using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab1_RicardoChian_PabloGarcia.Models;

namespace Lab1_RicardoChian_PabloGarcia.Helpers
{
    public class Data
    {
        private static Data _instance = null;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }

        public List<Empleado> ListaEmpleados = new List<Empleado>();
        public Estructuras.Data_Structures.Stack<Empleado> PilaEmpleados = new Estructuras.Data_Structures.Stack<Empleado>();
    }
}