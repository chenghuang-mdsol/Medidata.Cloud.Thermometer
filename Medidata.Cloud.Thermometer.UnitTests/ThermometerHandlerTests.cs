using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medidata.Cloud.Thermometer.UnitTests
{
    [TestClass]
    public class ThermometerHandlerTests
    {
        [TestMethod]
        public void Constructor_ShouldSetQuestionAndHandler()
        {
            //Arrange
            ThermometerQuestion q = new ThermometerQuestion("/abc");
            var h = new Func<IThermometerQuestion, string>(question => "question");

            //Act
            ThermometerHandler th = new ThermometerHandler(q, h);

            //Assert
            Assert.AreEqual(q, th.Question);
            Assert.AreEqual(h, th.Handler);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),"question")]
        public void Constructor_ExpectArgumentNullExceptionForQuestion()
        {
            //Arrange
            var h = new Func<IThermometerQuestion, string>(question => "question");

            //Act
            ThermometerHandler th = new ThermometerHandler(null, h);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),"handler")]
        public void Constructor_ExpectArgumentNullExceptionForHandler()
        {
            //Arrange
            ThermometerQuestion q = new ThermometerQuestion("/abc");
            
            //Act
            ThermometerHandler th = new ThermometerHandler(q, null);

            //Assert
        }
    }
}
