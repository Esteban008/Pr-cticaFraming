// ************************************************************************
// Practica 03
// Joseph Bravo, Esteban Machado
// Fecha de realizacion: 20/06/2020
// Fecha de entrega: 30/06/2020
// Resultados:
//* El codigo permite verificar el funcionamiento de
// la codificacion en binario y en forma de texto
// Ejecuta el programa, ¿qué puedes decir sobre las dos codificaciones?
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
using System.Threading;
using Codificador;

namespace Cliente
{
    class Cliente
    {
        static void Main(string[] args)
        {
            //Tiempo que pasa dormido el cliente
            Thread.Sleep(500);

            //Creamos el socket para poder comunicarnos con el servidor
            IPAddress servidor = IPAddress.Parse("127.0.0.1");
            int puerto = 8080;
            IPEndPoint extremo = new IPEndPoint(servidor, puerto);

            //Creamos un cliente TCP y nos conectamos
            TcpClient cliente = new TcpClient();
            cliente.Connect(extremo);

            //Nos proporciona la secuencia de datos para el acceso a la red
            NetworkStream flujoRed = cliente.GetStream();

            Elemento elemento1 = new Elemento(1234567890987654L, "Cadena de Bicicleta", 18, 1000, true, false);

            //Modifiacion para enviar dos elementos

            Elemento elemento2 = new Elemento(987654321012345L, "Casco de Bicicleta", 20, 45, true, true);


            //Utilizamos la libreria que creamos con el metodo Codificador Texto
            CodificadorTexto codificador = new CodificadorTexto();
            byte[] datosCodificados = codificador.Codificar(elemento1);
            Console.WriteLine("Enviando elemento codificado en texto (" +datosCodificados.Length + " bytes): ");
            Console.WriteLine(elemento1);

            NetworkStream flujoRed2 = cliente.GetStream();

            CodificadorTexto codificador1 = new CodificadorTexto();
            byte[] datosCodificados1 = codificador1.Codificar(elemento2);
            Console.WriteLine("Enviando elemento codificado en texto (" + datosCodificados1.Length + " bytes): ");
            Console.WriteLine(elemento2);
            
            //Procedemos a enviar los datos por la red
            
            flujoRed.Write(datosCodificados, 0, datosCodificados.Length);
            flujoRed2.Write(datosCodificados1, 0, datosCodificados1.Length);

            //Como tenemos un servidor ECO, ahora tenemos que realizar el rproceso contrario, el decodificador.
            
            DecodificadorBinario decodificador = new DecodificadorBinario();
            Elemento elementoRecibido = decodificador.Decodificar(cliente.GetStream());

            DecodificadorBinario decodificador1 = new DecodificadorBinario();
            Elemento elementoRecibido1 = decodificador1.Decodificar(cliente.GetStream());

            //Impresion del texto recibido
            Console.WriteLine("Se recibio un elemento codificado en formato binario:");
            Console.WriteLine(elementoRecibido);

            Console.WriteLine("Se recibio un elemento codificado en formato binario:");
            Console.WriteLine(elementoRecibido1);

            flujoRed.Close();
            cliente.Close();

        }
    }
}
