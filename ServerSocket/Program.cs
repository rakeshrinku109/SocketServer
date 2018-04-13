using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("-- Server connecting --\n");

            try
            {
                Server _serv = new Server();
                _serv.CreateSocket(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n \n \n");
            Console.WriteLine("-- press any key to exist--");
            Console.ReadLine();
        }
    }
}
