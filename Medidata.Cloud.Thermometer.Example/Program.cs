using System;
using System.Linq;
using System.Threading;

namespace Medidata.Cloud.Thermometer.Example
{
    internal class Program
    {
        private static void Main()
        {
            var app = new ThermometerApp()
                .Answer("/HowManyCpus/fasf", question =>
                {
                    return new {result = "OK", x = question.x , y = question.y, name = question.Name };
                })
                .Answer("/CanAccessDb", question => new {result = "NG", message = "Exception message"})
                .Answer("/GetException", question =>
                {
                    throw new Exception("Intential exception for demo");
                })
                .Answer("/CacheFlush", question =>
                {
                    // Call Rave's cache flush funciton.
                    return new {result = "OK", message = "Cache flush is done."};
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