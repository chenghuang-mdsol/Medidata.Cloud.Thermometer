using System.Linq;
using System.Web.Script.Serialization;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;

namespace Medidata.Cloud.Thermometer.UnitTests.Extensions
{
    [TestClass]
    public class OwinResponseExtensionsTests
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
        }

        [TestMethod]
        public void OwinResponse_WriteAsJson_calls_OwinResponseWriteWithJson()
        {
            // Arrange
            var response = _fixture.Create<IOwinResponse>();
            var obj = new {x = 1, y = 2};
            var json = new JavaScriptSerializer().Serialize(obj);

            // Act
            response.WriteAsJson(obj);

            // Assert
            response.AssertWasCalled(x => x.Write(json));
        }
    }
}