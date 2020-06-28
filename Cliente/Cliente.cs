// ************************************************************************
// Practica 03
// Joseph Bravo, Esteban Machado
// Fecha de realizacion: 20/06/2020
// Fecha de entrega: 30/06/2020
// Resultados:
//* El codigo permite verificar el funcionamiento de
// la codificacion en binario y en forma de texto
// Conclusiones:
//*
// Recomendaciones:
//*
// ************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Codificador;

namespace Cliente
{
    class Cliente
    {
        static void Main(string[] args)
        {
            Thread.Sleep(500);

            IPAddress servidor = IPAddress.Parse("127.0.0.1");
            int puerto = 8080;
            IPEndPoint extremo = new IPEndPoint(servidor, puerto);

            TcpClient cliente = new TcpClient();
            cliente.Connect(extremo);

            NetworkStream flujoRed = cliente.GetStream();

            Elemento elemento = new Elemento(1234567890987654L, "Cadena de Bicicleta", 18, 1000, true, false);

            CodificadorTexto codificador = new CodificadorTexto();
            byte[] datosCodificados = codificador.Codificar(elemento);
            Console.WriteLine("Enviando elemento codificado en texto (" +
            datosCodificados.Length + " bytes): ");
            Console.WriteLine(elemento);

            flujoRed.Write(datosCodificados, 0, datosCodificados.Length);
            DecodificadorBinario decodificador = new DecodificadorBinario();
            Elemento elementoRecibido = decodificador.Decodificar(cliente.GetStream());

            Console.WriteLine("Se recibio un elemento codificado en formato binario:");
            Console.WriteLine(elementoRecibido);

            flujoRed.Close();
            cliente.Close();

        }
    }
}
