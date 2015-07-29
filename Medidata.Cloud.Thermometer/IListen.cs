using System;
using System.Threading.Tasks;

namespace Medidata.Cloud.Thermometer
{
    public interface IListen
    {
        Task<IDisposable> Listen(int port);
        ActionRouteConfiguration Routes { get; }
    }
}