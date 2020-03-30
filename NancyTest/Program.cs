using System;
using Nancy.Hosting.Self;
using System.Collections.Generic;
using System.Linq;

namespace NancyTest
{
    class Program
    {
        static void Main(string[] args)
        {

            
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }


        }
    }
}
