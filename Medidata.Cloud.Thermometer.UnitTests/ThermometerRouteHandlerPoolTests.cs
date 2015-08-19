using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medidata.Cloud.Thermometer.UnitTests
{
    [TestClass]
    public class ThermometerRouteHandlerPoolTests
    {
        [TestMethod]
        public void Add_ShouldAddOneElement()
        {
            //Arrange
            ThermometerHandler th = new ThermometerHandler(new ThermometerQuestion("/abc"), question => "def?" );
            ThermometerRouteHandlerPool tp = new ThermometerRouteHandlerPool();

            //Act
            tp.Add(th);

            //Assert
            Assert.AreEqual(1, tp.Count());
            Assert.AreEqual(th, tp.First());
        }

        [TestMethod]
        public void Contains_ShouldReturnByRoute()
        {
            //Arrange
            ThermometerHandler th = new ThermometerHandler(new ThermometerQuestion("/abc"), question => "def?");
            ThermometerRouteHandlerPool tp = new ThermometerRouteHandlerPool();
            tp.Add(th);

            //Act

            //Assert
            Assert.IsTrue(tp.Contains("/abc"));
            Assert.IsFalse(tp.Contains("/def"));
        }

        [TestMethod]
        public void FindOrDefault_ShouldFind()
        {
            //Arrange
            ThermometerHandler th = new ThermometerHandler(new ThermometerQuestion("/abc"), question => "def?");
            ThermometerRouteHandlerPool tp = new ThermometerRouteHandlerPool();
            tp.Add(th);

            //Act
            //Assert
            Assert.AreEqual(th,tp.FindOrDefault("/abc"));
            Assert.IsNull(tp.FindOrDefault("/def"));

        }
    }
}
