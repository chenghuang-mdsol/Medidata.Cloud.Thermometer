using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Medidata.Cloud.Thermometer.UnitTests
{
    [TestClass]
    public class ThermometerAppTests
    {
        [TestMethod]
        public void Answer_ShouldNotThrowException()
        {
            // Arrange
            ThermometerApp app = MockRepository.GenerateStub<ThermometerApp>();
            
            //Act
            app.Answer("/abc", question => "def");
            
        }

        [TestMethod]
        public void Listen_ShouldListenOnPort()
        {
            // Arrange
            ThermometerApp app = new ThermometerApp();
            var ips = IPGlobalProperties.GetIPGlobalProperties();
            var portsTaken = ips.GetActiveTcpListeners().Select(p => p.Port);
            // Act
            var allPorts = Enumerable.Range(1000, 65535).ToList();
            var avaiblePort = allPorts.First(p => !portsTaken.Contains(p));
            app.Listen(avaiblePort);

            //Assert
            portsTaken = ips.GetActiveTcpListeners().Select(p => p.Port);
            Assert.IsTrue(portsTaken.Contains(avaiblePort));

        }
    }
}
