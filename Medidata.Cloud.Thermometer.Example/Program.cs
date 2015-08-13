using System;
using System.Linq;
using System.Threading;

namespace Medidata.Cloud.Thermometer.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new ThermometerApp()
                .Reply("/HowManyCpus", question =>
                {
                    var msg = String.Join(", ", question.Keys.Select(x => String.Format("[{0}: {1}]", x, question[x])));
                    return new {result = "OK", message = msg, name = question.Name };
                })
                .Reply("/CanAccessDb", question => new {result = "NG", message = "Exception message"})
                .Reply("/GetException", question =>
                {
                    throw new Exception("Intential exception for demo");
                })
                .Reply("/CacheFlush", question =>
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