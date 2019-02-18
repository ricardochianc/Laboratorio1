﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasGenericas.Data_Structures
{
    public class Stack<T> : ILinearDataStructure<T>, IEnumerable<T> where T : IComparable
    {
        //Top o cima, primer elemento en la pila
        private Nodo<T> Top { get; set; }
        public int Count { get; set; }
        public bool IsEmpty { get; set; }

        public void Add(T value)
        {
            if (IsEmpty)
            {
                Count = 1;
                Top = new Nodo<T>(value);
                IsEmpty = false;
            }
            else
            {
                var nuevo = new Nodo<T>(value);
                nuevo.Next = Top;
                Top = nuevo;
                Count++;
            }
        }

        public void Push(T value)
        {
          Add(value);   
        }

        public T Delete()
        {
            var aux = Top.Value;
            Top = Top.Next;
            return aux;
        }

        public T Pop()
        {
            return Delete();
        }

        public T Get(int posicion)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = Top;

            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}