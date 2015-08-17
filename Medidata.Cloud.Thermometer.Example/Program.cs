using System;
using System.Threading;

namespace Medidata.Cloud.Thermometer.Example
{
    internal class Program
    {
        private static void Main()
        {
            var app = new ThermometerApp()
                .Answer("/level1/subquestion", req =>
                {
                    return new { result = "OK", query_parameters = req, route = req.Route };
                })
                .Answer("overridenQuestionName" ,"/CanAccessDb", req => new {result = "NG", message = "Exception message"})
                .Answer("/GetException", req =>
                {
                    throw new Exception("Intential exception for demo");
                })
                .Listen(9000);


            var symbols = new[] { '-', '\\', '|', '/' };
            var i = 0;
            while (true)
            {
                // Main thread can do other things.
                Thread.Sleep(100);
                Console.Write("\b");
                Console.Write(symbols[i++ % symbols.Length]);
            }
        }
    }
}