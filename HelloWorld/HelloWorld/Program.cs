using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "human";
            if (args.Length > 0)
            {
                name = args[0];
            }
            Console.WriteLine($"Hello {name}!");
        }
    }
}
