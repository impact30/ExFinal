using ExFinal;
using System;

namespace SimuladorClientes
{
    struct Servidor
    {
        public bool Ocupado;
        public Cliente Cliente;
        public int TiempoRestante;
    }

    class Program
    {
        static Random rnd = new();
        static long idCounter = 1;

        static Cliente CrearCliente() => new Cliente(idCounter++, (TipoCliente)rnd.Next(1, 4));

        static void Main(string[] args)
        {
            int tiempoSimulacion = 30;
            int tiempoPorEtapa = 2;
            int c_Time = 0;

            Cola vigilancia = new();
            Cola recepcion = new();
            Cola atencion = new();
            Cola serviciosMovil = new();
            Cola serviciosHogar = new();
            Cola reclamos = new();
            Cola cajas = new();

            Servidor[] servVigilancia = new Servidor[1];
            Servidor[] servRecepcion = new Servidor[1];
            Servidor[] servAtencion = new Servidor[1];
            Servidor[] servMovil = new Servidor[1];
            Servidor[] servHogar = new Servidor[1];
            Servidor[] servReclamos = new Servidor[1];
            Servidor[] servCajas = new Servidor[2];

            while (c_Time < tiempoSimulacion)
            {
                if (c_Time % 2 == 0)
                {
                    var cliente = CrearCliente();
                    vigilancia.Encolar(cliente);
                    Console.WriteLine($"[{c_Time}] Cliente {cliente.Id} llega a Vigilancia (Tipo: {cliente.Tipo})");
                }

                ProcesarEtapa(vigilancia, recepcion, servVigilancia, tiempoPorEtapa, "Vigilancia");
                ProcesarEtapa(recepcion, atencion, servRecepcion, tiempoPorEtapa, "Recepción");

                for (int i = 0; i < servAtencion.Length; i++)
                {
                    if (!servAtencion[i].Ocupado && atencion.GetSize() > 0)
                    {
                        Cliente cliente = atencion.Desencolar();
                        servAtencion[i].Cliente = cliente;
                        servAtencion[i].TiempoRestante = tiempoPorEtapa;
                        servAtencion[i].Ocupado = true;
                        Console.WriteLine($"Cliente {cliente.Id} inicia etapa Atención al Cliente");
                    }
                    else if (servAtencion[i].Ocupado)
                    {
                        servAtencion[i].TiempoRestante--;
                        if (servAtencion[i].TiempoRestante <= 0)
                        {
                            var cliente = servAtencion[i].Cliente;
                            Console.WriteLine($"Cliente {cliente.Id} finaliza etapa Atención al Cliente");
                            switch (cliente.Tipo)
                            {
                                case TipoCliente.Movil:
                                    Console.WriteLine($"Cliente {cliente.Id} es derivado a Servicios Móviles");
                                    serviciosMovil.Encolar(cliente);
                                    break;
                                case TipoCliente.Hogar:
                                    Console.WriteLine($"Cliente {cliente.Id} es derivado a Servicios Hogar");
                                    serviciosHogar.Encolar(cliente);
                                    break;
                                case TipoCliente.Reclamo:
                                    Console.WriteLine($"Cliente {cliente.Id} es derivado a Reclamos");
                                    reclamos.Encolar(cliente);
                                    break;
                            }
                            servAtencion[i].Ocupado = false;
                        }
                    }
                }

                ProcesarEtapa(serviciosMovil, cajas, servMovil, tiempoPorEtapa, "Servicios Móviles");
                ProcesarEtapa(serviciosHogar, cajas, servHogar, tiempoPorEtapa, "Servicios Hogar");
                ProcesarEtapa(reclamos, cajas, servReclamos, tiempoPorEtapa, "Reclamos");
                ProcesarEtapa(cajas, null, servCajas, tiempoPorEtapa, "Caja", final: true);

                Console.WriteLine($"[{c_Time}] En atención - " +
                    $"Vigilancia:{ContarAtendidos(servVigilancia)} " +
                    $"Recepcion:{ContarAtendidos(servRecepcion)} " +
                    $"Atencion:{ContarAtendidos(servAtencion)} " +
                    $"Movil:{ContarAtendidos(servMovil)} " +
                    $"Hogar:{ContarAtendidos(servHogar)} " +
                    $"Reclamos:{ContarAtendidos(servReclamos)} " +
                    $"Cajas:{ContarAtendidos(servCajas)}");

                c_Time++;
            }

            Console.WriteLine("\nSimulación finalizada.");
        }

        static void ProcesarEtapa(Cola origen, Cola destino, Servidor[] servidores, int tiempo, string nombre, bool final = false)
        {
            for (int i = 0; i < servidores.Length; i++)
            {
                if (!servidores[i].Ocupado && origen.GetSize() > 0)
                {
                    Cliente cliente = origen.Desencolar();
                    servidores[i].Cliente = cliente;
                    servidores[i].TiempoRestante = tiempo;
                    servidores[i].Ocupado = true;
                    Console.WriteLine($"Cliente {cliente.Id} inicia etapa {nombre}");
                }
                else if (servidores[i].Ocupado)
                {
                    servidores[i].TiempoRestante--;
                    if (servidores[i].TiempoRestante <= 0)
                    {
                        Cliente cliente = servidores[i].Cliente;
                        Console.WriteLine($"Cliente {cliente.Id} finaliza etapa {nombre}");
                        if (!final && destino != null)
                        {
                            destino.Encolar(cliente);
                        }
                        servidores[i].Ocupado = false;
                    }
                }
            }
        }

        static int ContarOcupados(Servidor[] servidores)
        {
            int cont = 0;
            foreach (var s in servidores)
            {
                if (s.Ocupado) cont++;
            }
            return cont;
        }

        static int ContarAtendidos(Servidor[] servidores)
        {
            int cont = 0;
            foreach (var s in servidores)
            {
                if (s.Ocupado)
                    cont++;
            }
            return cont;
        }

    }
}
