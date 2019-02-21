using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab1_RicardoChian_PabloGarcia.Models
{
    public class Empleado : IComparable
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Debe ingresar código")]
        public string CodigoEmpleado { get; set; }

        [Display(Name = "Tiempo trabajado")]
        [DataType(DataType.Time)]
        public TimeSpan HorasTrabajadas { get; set; }

        [Display(Name = "Hora de entrada")]
        [DataType(DataType.Time)]
        public DateTime HoraDeEntrada { get; set; }

        [Display(Name = "Hora de retorno/salida")]
        [DataType(DataType.Time)]
        public DateTime HoraDeSalida { get; set; }

        [Display(Name = "¿Se encuentra en la oficina?")]
        public bool Disponible { get; set; }

        [Display(Name = "Sueldo")]
        [DataType(DataType.Currency)]
        public double Sueldo { get; set; }

        [Display(Name = "Citas programadas")]
        public int CantidaCitas { get; set; }

        public Empleado(string nombre, string codigo)
        {
            Nombre = nombre;
            CodigoEmpleado = codigo;
            HorasTrabajadas = TimeSpan.Parse("0:00");
            Disponible = false;

            HoraDeEntrada = DateTime.Parse("00:00");
            HoraDeSalida = DateTime.Parse("00:00");
        }

        public int CompareTo(object obj)
        {
            var comparer = (Empleado) obj;

            return Nombre.CompareTo(comparer.Nombre);
        }

        public static Comparison<Empleado> OrdenarPorNombre = delegate(Empleado empleado1, Empleado empleado2)
        {
            return empleado1.CompareTo(empleado2);
        };

        public void AsignarCitas()
        {
            var Num = new Random();
            CantidaCitas = Num.Next(1, 5);
        }

        public void CalcularHoras(int count)
        {
            HoraDeEntrada = HoraDeEntrada.Add(new TimeSpan((count + 1), 0, 0));

            var trabajadas = new TimeSpan(0,(CantidaCitas*90),0);

            HorasTrabajadas = trabajadas;
            HorasTrabajadas += new TimeSpan(0,120,0);

            var numHoras = HorasTrabajadas.Hours;
            var numMinutos = HorasTrabajadas.Minutes;

            HoraDeSalida = HoraDeEntrada.Add(new TimeSpan(numHoras, numMinutos, 0));

            var HoraT = HorasTrabajadas.Hours;
            HoraT += HorasTrabajadas.Minutes/60;

            Sueldo = (HoraT*38);
        }
    }
}