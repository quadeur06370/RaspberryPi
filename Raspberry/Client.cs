using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Raspberry
{
    class Client
    {
        private Socket socket;
        public Client()
        {
            Console.Write("ip: ");// demande l'ip
            if (IPAddress.TryParse(Console.ReadLine(), out var ip)) // recupere l'ip
            {
                Console.Write("port: ");// demande le port
                if (int.TryParse(Console.ReadLine(), out var port))// recupere le port
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // creer un socket 
                    socket.Connect(new IPEndPoint(ip, port)); // connection au socket avec l'ip et le port rentré

                    Console.WriteLine("listen server... (server.Send)");
                    var clientThread = new Thread(() => // creation d'un thread
                    {
                        Server(socket);  // prend en parametre le socket ( connecteur reseau)
                    });
                    clientThread.Start();

                   
                }
            }
            Console.WriteLine("Appuyez pour afficher les mails...");
            Console.ReadLine();
        }

        public void send(string message) // methode send avec un string en parametre
        {
               
                var data = Encoding.UTF8.GetBytes(message); // data qui prends les bytes et les encode en utf8
                socket.Send(data); // envoie la donnée au socket
            
        }

        public  void Server(Socket serverSocket) // methode Server qui contient un socket
        {
            while (true)
            {
                var buffer = new byte[serverSocket.SendBufferSize]; // renvoie la taille
                var readBytes = serverSocket.Receive(buffer); // lit les bytes de serversocket
                if (readBytes > 0)
                {
                    var msg = Encoding.UTF8.GetString(buffer, 0, readBytes); // message qui prends le string et l'encode en utf8
                    Console.WriteLine(msg); //ecris le message
                }
            }
        }

    }
}
