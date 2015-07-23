# Medidata.Cloud.Thermometer
In the host process, it spawns a thread as REST service and listens to the specified port.

# NuGet package
http://nuget.imedidata.net/feed/mdsol/package/nuget/Medidata.Cloud.Thermometer

```powershell
Install-Package Medidata.Cloud.Thermometer
```

# How to use

```cs
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
                .WhenGet("/actionName", databag =>
                {
                    var msg = String.Join(", ", databag.Select(x => String.Format("[{0}: {1}]", x.Key, x.Value)));
                    return new {result = "OK", message = msg};
                })
                .WhenGet("/anotherAction", databag => new {result = "NG", message = "Exception message"})
                .Listen(9000);

            // Main thread can do other things.
            Console.ReadLine();
            }
        }
    }
}
```
