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
// Ejecuta el programa, ¿qué puedes decir sobre las dos codificaciones?
// El metodo de codificacion varia dependiendo del elemento a enviar, y al momento 
// de reenviarlo en el servior igual se realiza una modificacion para poder enviarlo
// lo cual nos permite darnos cuenta como trabaja la libreria codificador con los 
// diferentes tipos de mensajes.
// Modifica el programa para enviar dos elementos y recibir dos elementos.
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
            //Creacion del socket en el que vamos a escuchar
            int puerto = 8080;
            TcpListener socketEscucha = new TcpListener(IPAddress.Any, puerto);
            socketEscucha.Start();

            //Aceptamos al cliente
            TcpClient cliente = socketEscucha.AcceptTcpClient();

            //Utilizamos la libreria Codificador para poder Decifrar el texto
            DecodificadorTexto decodificador = new DecodificadorTexto();
            Elemento elemento = decodificador.Decodificar(cliente.GetStream());

            //Se muestra el mensaje cifrado
            Console.WriteLine("Se recibio un elemento codificado en texto:");
            Console.WriteLine(elemento);

            DecodificadorTexto decodificador1 = new DecodificadorTexto();
            Elemento elemento1 = decodificador1.Decodificar(cliente.GetStream());

            Console.WriteLine("Se recibio un elemento codificado en texto:");
            Console.WriteLine(elemento1);
            //Se codifica de nuevo el mensaje para su renvio 
            CodificadorBinario codificador = new CodificadorBinario();
            elemento.precio += 10;

            CodificadorBinario codificador1 = new CodificadorBinario();
            elemento1.precio += 10;

            //Se envia al cliente el mensaje nuevamente codificado.
            Console.Write("Enviando elemento en binario...");
            byte[] bytesParaEnviar = codificador.Codificar(elemento);
            Console.WriteLine("(" + bytesParaEnviar.Length + " bytes): ");
            cliente.GetStream().Write(bytesParaEnviar, 0, bytesParaEnviar.Length);

            Console.Write("Enviando elemento en binario...");
            byte[] bytesParaEnviar1 = codificador1.Codificar(elemento1);
            Console.WriteLine("(" + bytesParaEnviar1.Length + " bytes): ");
            cliente.GetStream().Write(bytesParaEnviar1, 0, bytesParaEnviar1.Length);


            cliente.Close();

            socketEscucha.Stop();
        }
    }
}
