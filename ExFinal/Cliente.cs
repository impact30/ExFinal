using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFinal
{
    public enum TipoCliente { Movil = 1, Hogar, Reclamo }

    public class Cliente
    {
        public long Id;
        public TipoCliente Tipo;

        public Cliente(long id, TipoCliente tipo)
        {
            Id = id;
            Tipo = tipo;
        }
    }
}
