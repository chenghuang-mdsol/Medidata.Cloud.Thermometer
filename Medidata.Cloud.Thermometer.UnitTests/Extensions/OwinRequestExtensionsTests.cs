using System.Linq;
using Medidata.Cloud.Thermometer.Extensions;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;

namespace Medidata.Cloud.Thermometer.UnitTests.Extensions
{
    [TestClass]
    public class OwinRequestExtensionsTests
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
        }

        [TestMethod]
        public void OwinRequest_ToThermometerQuestion_returns_Question()
        {
            // Arrange
            var request = _fixture.Create<IOwinRequest>();
            request.Stub(x => x.Path).Return(new PathString("/x/y/z"));
            request.Stub(x => x.QueryString).Return(new QueryString("x=1&y=2"));

            // Act
            var result = request.ToThermometerQuestion();

            // Assert
            Assert.AreEqual("x.y.z", result.Name);
            Assert.AreEqual("/x/y/z", result.Route);
            CollectionAssert.AreEquivalent(new []{"x", "y"}, result.Keys.ToArray());
            Assert.AreEqual("1", result["x"]);
            Assert.AreEqual("2", result["y"]);
        }
    }
}