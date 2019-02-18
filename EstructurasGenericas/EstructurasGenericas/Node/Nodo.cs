using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasGenericas
{
    class Nodo<T>
    {
        public T Value { get; set; }
        public Nodo<T> Next { get; set; }

        public Nodo(T value)
        {
            this.Value = value;
            Next = null;
        }
    }
}
