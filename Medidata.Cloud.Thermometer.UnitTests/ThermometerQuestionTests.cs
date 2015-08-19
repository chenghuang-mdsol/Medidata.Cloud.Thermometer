using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks.Constraints;

namespace Medidata.Cloud.Thermometer.UnitTests
{
    [TestClass]
    public class ThermometerQuestionTests
    {
        [TestMethod]
        public void Constructor_ShouldSetRouteAndName()
        {
            ThermometerQuestion question = new ThermometerQuestion("/abc", "def");
            Assert.AreEqual("/abc", question.Route);
            Assert.AreEqual("def", question.Name);

        }


        [TestMethod]
        public void Constructor_ShouldSetRouteAndDefaultName()
        {
            ThermometerQuestion question = new ThermometerQuestion("/abc");
            Assert.AreEqual("/abc", question.Route);
            Assert.AreEqual("abc", question.Name);

        }
    }
}
