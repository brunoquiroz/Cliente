using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress iPAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(iPAddress, 2050);



            try
            {
                Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remoteEP);

                Console.WriteLine("Conectado");
                string comando = "";
                while(comando != "chao")
                {
                    Console.WriteLine("Ingrese un mensaje para el servidor");
                    comando = Console.ReadLine();
                    byte[] msg = Encoding.ASCII.GetBytes(comando+"<EOF>");
                    int byteSent = sender.Send(msg);
                    byte[] bytes = new byte[1024];
                    int byteRec = sender.Receive(bytes);
                    string texto = Encoding.ASCII.GetString(bytes, 0, byteRec);
                    Console.WriteLine("Servidor: " + texto);
                }
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
