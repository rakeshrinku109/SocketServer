using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SocketClient _sockClient = new SocketClient("10.255.53.17", 9298);


                Task ConnectTask = _sockClient.ConnectAsync();
                ConnectTask.Wait();

                do
                {
                    Console.WriteLine("Type a message to send");
                    string message = Console.ReadLine();
                    _sockClient.SendAsync(message);
                    _sockClient.ReceiveAsync();
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }

        }

    }
}
