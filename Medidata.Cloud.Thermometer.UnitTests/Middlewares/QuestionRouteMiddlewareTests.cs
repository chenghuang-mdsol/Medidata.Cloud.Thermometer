using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medidata.Cloud.Thermometer.Extensions;
using Medidata.Cloud.Thermometer.Middlewares;
using Medidata.Cloud.Thermometer.UnitTests.TestHelpers;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;

namespace Medidata.Cloud.Thermometer.UnitTests.Middlewares
{
    /// <summary>
    /// Summary description for QuestionRouteMiddlewareTests
    /// </summary>
    [TestClass]
    public class QuestionRouteMiddlewareTests
    {
        private IFixture _fixture;
        private QuestionRouteMiddleware _sut;
        private OwinMiddleware _nextMiddleware;
        private ThermometerRouteHandlerPool _handlerPool;
        
        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
            _nextMiddleware = MockRepository.GenerateMock<OwinMiddleware>(new FakeRootMiddelware());
            _handlerPool = new ThermometerRouteHandlerPool();
            
            _sut = new QuestionRouteMiddleware(_nextMiddleware, _handlerPool);
        }

        [TestMethod]
        public void Invoke_ShouldHandleAndReturn200()
        {
            //Arrange
            var context = _fixture.Create<IOwinContext>();
            _handlerPool.Add(new ThermometerHandler(new ThermometerQuestion("/abc", "def"), req => new { result = "ghi" }));

            context.Stub(x => x.Request).Return(_fixture.Create<IOwinRequest>());
            context.Request.Stub(x => x.Path).Return(new PathString("/abc"));
            //context.Request.Stub(x => x.ToThermometerQuestion()).Return(new ThermometerQuestion("/abc", "/abc"));
                
            IOwinResponse response = MockRepository.GenerateStub<IOwinResponse>();
            context.Stub(x => x.Response).Return(response);
            context.Response.Stub(x => x.WriteAsJson(Arg<object>.Is.Anything));
            response.StatusCode = 200;
            _nextMiddleware.Stub(x => x.Invoke(context)).Return(Task.FromResult("success"));
            //Act
            _sut.Invoke(context);
            
            //Assert
            context.Response.AssertWasCalled(x=>x.WriteAsJson(Arg<object>.Is.Anything));
            Assert.AreEqual(200, response.StatusCode);
            _nextMiddleware.AssertWasCalled(x=>x.Invoke(context));
            
        }

        [TestMethod]
        public void Invoke_ShouldReturn404()
        {
            //Arrange
            var context = _fixture.Create<IOwinContext>();

            context.Stub(x => x.Request).Return(_fixture.Create<IOwinRequest>());
            context.Request.Stub(x => x.Path).Return(new PathString("/abc"));
            //context.Request.Stub(x => x.ToThermometerQuestion()).Return(new ThermometerQuestion("/abc", "/abc"));

            IOwinResponse response = MockRepository.GenerateStub<IOwinResponse>();
            context.Stub(x => x.Response).Return(response);
            context.Response.Stub(x => x.WriteAsJson(Arg<object>.Is.Anything));
            response.StatusCode = 200;
            _nextMiddleware.Stub(x => x.Invoke(context)).Return(Task.FromResult("success"));
            //Act
            _sut.Invoke(context);

            //Assert
            context.Response.AssertWasNotCalled(x => x.WriteAsJson(Arg<object>.Is.Anything));
            Assert.AreEqual(404, response.StatusCode);
            _nextMiddleware.AssertWasNotCalled(x => x.Invoke(context));

        }


        [TestMethod]
        public void Invoke_ShouldReturn500()
        {
            //Arrange
            var context = _fixture.Create<IOwinContext>();
            _handlerPool.Add(new ThermometerHandler(new ThermometerQuestion("/abc", "def"), req => new { result = "ghi" }));
            context.Stub(x => x.Request).Return(_fixture.Create<IOwinRequest>());
            context.Request.Stub(x => x.Path).Return(new PathString("/abc"));
            
            IOwinResponse response = MockRepository.GenerateStub<IOwinResponse>();
            context.Stub(x => x.Response).Return(response);
            context.Response.Stub(x => x.WriteAsJson(Arg<object>.Is.Anything));
            response.StatusCode = 200;
            _nextMiddleware.Stub(x => x.Invoke(context)).Throw(new Exception("Intended Exception"));
            //Act
            _sut.Invoke(context);

            //Assert
            Assert.AreEqual(500, response.StatusCode);
            _nextMiddleware.AssertWasCalled(x => x.Invoke(context));

        }
    }
}
