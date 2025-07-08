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
        private Node first;
        private Node last;

        private class Node
        {
            public Cliente data;
            public Node next, prev;

            public Node(Cliente cliente)
            {
                data = cliente;
                next = null;
                prev = null;
            }
        }

        public Cola()
        {
            first = null;
            last = null;
        }

        public void Enqueue(Cliente cliente)
        {
            Node nPtr = new Node(cliente);
            if (first == null)
            {
                first = nPtr;
                last = nPtr;
            }
            else
            {
                last.next = nPtr;
                nPtr.prev = last;
                last = nPtr;
            }
        }

        public Cliente Dequeue()
        {
            if (first == null) throw new InvalidOperationException("Cola vacía");
            Cliente cliente = first.data;
            first = first.next;
            if (first != null) first.prev = null;
            else last = null;
            return cliente;
        }

        public int GetSize()
        {
            int size = 0;
            Node ptr = first;
            while (ptr != null)
            {
                size++;
                ptr = ptr.next;
            }
            return size;
        }
    }
}

