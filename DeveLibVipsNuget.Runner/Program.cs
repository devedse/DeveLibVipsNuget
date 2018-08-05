using System;
using System.IO;

namespace DeveLibVipsNuget.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var vipsFile = LibVipsManager.ExtractAndGetVipsExeFile();
            if (File.Exists(vipsFile))
            {
                Console.WriteLine("It works!");
            }
            else
            {
                Console.WriteLine("It doens't work :(");
            }

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }
    }
}
