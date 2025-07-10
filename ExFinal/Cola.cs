using SimuladorClientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFinal
{
    public class Cola
    {
        private Nodo primero;
        private Nodo ultimo;

        private class Nodo
        {
            public Cliente dato;
            public Nodo sig;

            public Nodo(Cliente cliente)
            {
                dato = cliente;
                sig = null;
            }
        }

        public Cola()
        {
            primero = null;
            ultimo = null;
        }

        public void Encolar(Cliente cliente)
        {

            Nodo nPuntero = new Nodo(cliente);
            if (primero == null)
            {
                primero = nPuntero;
                ultimo = nPuntero;
            }
            else
            {
                ultimo.sig = nPuntero;
                ultimo = nPuntero;
            }
        }

        public Cliente Desencolar()
        {
            if (primero == null)
                Console.WriteLine("Cola vacía");
            Cliente cliente = primero.dato;
            primero = primero.sig;
            if (primero == null)
            ultimo = null;
            return cliente;
        }

        public int GetSize()
        {
            int tamaño = 0;
            Nodo puntero = primero;
            while (puntero != null)
            {
                tamaño++;
                puntero = puntero.sig;
            }
            return tamaño;
        }
    }
}

