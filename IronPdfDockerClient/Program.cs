using System;
using IronPdfDockerLib;

namespace IronPdfDockerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!  Generating a PDF");


            var generator = new Generator();

            var result = generator.BuildReport();

            // write out the result
            Console.WriteLine(result);



        }
    }
}
