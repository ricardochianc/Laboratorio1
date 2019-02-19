using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructuras.Interface
{
    interface ILinearDataStructure<T>
    {
        void Add(T value);
        T Delete();
        T Get(int posicion);
    }
}
