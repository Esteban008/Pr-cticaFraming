// ************************************************************************
// Practica 03
// Joseph Bravo, Esteban Machado
// Fecha de realizacion: 20/06/2020
// Fecha de entrega: 30/06/2020
// Resultados:
//* El codigo permite verificar el funcionamiento de
//* El codigo permite verificar el funcionamiento de
// la codificacion en binario y en forma de texto
// 
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
using Codificador;

namespace Servidor
{
    class Servidor
    {
        static void Main(string[] args)
        {
            int puerto = 8080;
            TcpListener socketEscucha = new TcpListener(IPAddress.Any, puerto);
            socketEscucha.Start();

            TcpClient cliente = socketEscucha.AcceptTcpClient();

            DecodificadorTexto decodificador = new DecodificadorTexto();
            Elemento elemento = decodificador.Decodificar(cliente.GetStream());

            Console.WriteLine("Se recibio un elemento codificado en texto:");
            Console.WriteLine(elemento);

            CodificadorBinario codificador = new CodificadorBinario();
            elemento.precio += 10;

            Console.Write("Enviando elemento en binario...");
            byte[] bytesParaEnviar = codificador.Codificar(elemento);
            Console.WriteLine("(" + bytesParaEnviar.Length + " bytes): ");
            cliente.GetStream().Write(bytesParaEnviar, 0, bytesParaEnviar.Length);

            cliente.Close();

            socketEscucha.Stop();
        }
    }
}
