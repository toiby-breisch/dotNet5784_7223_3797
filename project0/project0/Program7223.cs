using System;
using System.Xml.Linq;

namespace project0
{
    partial class Program
    {
         static void Main(string[] args)

        {
            Welcome7223();
            Welcome3797();
            Console.ReadKey();
        }

        static partial void Welcome3797();
        private static void Welcome7223()
        {
            Console.WriteLine("Enter your  name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first consile application", name);

        }
    }
}
