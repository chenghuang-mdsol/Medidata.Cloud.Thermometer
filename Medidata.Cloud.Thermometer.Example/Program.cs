using System;
using System.Linq;
using System.Threading;

namespace Medidata.Cloud.Thermometer.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new LiteRestApp();
            app
                .WhenGet("/HowManyCpus", databag =>
                {
                    var msg = String.Join(", ", databag.Select(x => String.Format("[{0}: {1}]", x.Key, x.Value)));
                    return new {result = "OK", message = msg};
                })
                .WhenGet("/CanAccessDb", databag => new {result = "NG", message = "Exception message"})
                .WhenGet("/GetException", databag =>
                {
                    throw new Exception("Intential exception for demo");
                })
                .WhenGet("/CacheFlush", databag =>
                {
                    // Call Rave's cache flush funciton.
                    return new {result = "OK", message = "Cache flush is done."};
                })
                .Listen(9000);

            var symbols = new[] {'-', '\\', '|', '/'};
            var i = 0;
            while (true)
            {
                // Main thread can do other things.
                Thread.Sleep(100);
                Console.Write("\b");
                Console.Write(symbols[i++%symbols.Length]);
            }
        }
    }
}