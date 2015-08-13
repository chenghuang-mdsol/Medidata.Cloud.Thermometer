using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin.Host.HttpListener;

namespace Medidata.Cloud.Thermometer.Helpers
{
    /// <summary>
    /// This unused field is to keep reference of Microsoft.Owin.Host.HttpListener.dll, 
    /// so as the assembly file can be copied to the caller's folder when build.
    /// </summary>
    [ExcludeFromCodeCoverage]
    class MicrosoftOwinHostHttpListenerAnchor
    {
#pragma warning disable 169
        private readonly OwinHttpListener _assemblyAnchor;
#pragma warning restore 169
    }
}