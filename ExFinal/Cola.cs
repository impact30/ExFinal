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
        private Node primero;
        private Node ultimo;

        private class Node
        {
            public Cliente dato;
            public Node sig, ant;

            public Node(Cliente cliente)
            {
                dato = cliente;
                sig = null;
                ant = null;
            }
        }

        public Cola()
        {
            primero = null;
            ultimo = null;
        }

        public void Encolar(Cliente cliente)
        {
            Node nPuntero = new Node(cliente);
            if (primero == null)
            {
                primero = nPuntero;
                ultimo = nPuntero;
            }
            else
            {
                ultimo.sig = nPuntero;
                nPuntero.ant = ultimo;
                ultimo = nPuntero;
            }
        }

        public Cliente Desencolar()
        {
            if (primero == null) throw new InvalidOperationException("Cola vacía");
            Cliente cliente = primero.dato;
            primero = primero.sig;
            if (primero != null) primero.ant = null;
            else ultimo = null;
            return cliente;
        }

        public int GetSize()
        {
            int tamaño = 0;
            Node puntero = primero;
            while (puntero != null)
            {
                tamaño++;
                puntero = puntero.sig;
            }
            return tamaño;
        }
    }
}

