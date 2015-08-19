using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
    [TestClass]
    public class ListAllQuestionMiddlewareTests
    {
        private IFixture _fixture;
        private ListAllQuestionMiddleware _sut;
        private OwinMiddleware _nextMiddleware;
        private ThermometerRouteHandlerPool _handlerPool;

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
            _nextMiddleware = MockRepository.GenerateMock<OwinMiddleware>(new FakeRootMiddelware());
            _handlerPool = new ThermometerRouteHandlerPool();
            _handlerPool.Add(new ThermometerHandler(new ThermometerQuestion("/abc", "def"), req => new {result = "ghi"}));
            _sut = new ListAllQuestionMiddleware(_nextMiddleware, _handlerPool);


        }

        [TestMethod]
        public void Invoke_ShouldByPassAndReturnEmptyResponse()
        {
            // Arrage
            var context = _fixture.Create<IOwinContext>();
            context.Stub(x => x.Response).Return(_fixture.Create<IOwinResponse>());
            var request = _fixture.Create<IOwinRequest>();
            request.Stub(x => x.Uri).Return(new Uri("http://localhost:8888"));
            context.Stub(x => x.Request).Return(request);
            context.Response.Stub(x => x.Headers).Return(_fixture.Create<IHeaderDictionary>());

            // Act
            var result = _sut.Invoke(context);

            // Assert
            Assert.IsNull(context.Response.Body);
            context.Response.AssertWasNotCalled(x=>x.WriteAsJson(Arg<object>.Is.Anything));
            _nextMiddleware.AssertWasCalled(x=>x.Invoke(context));
        }

        [TestMethod]
        public void Invoke_ShouldCallWriteAsJson()
        {
                    
            // Arrage
            var context = _fixture.Create<IOwinContext>();

            var request = _fixture.Create<IOwinRequest>();
            request.Stub(x => x.Path).Return(new PathString("/"));
            request.Stub(x => x.Uri).Return(new Uri("http://localhost:8888"));
            context.Stub(x => x.Request).Return(request);

            var response = _fixture.Create<IOwinResponse>();
            response.Stub(x => x.Headers).Return(_fixture.Create<IHeaderDictionary>());
            context.Stub(x => x.Response).Return(response);

            // Act
            var result = _sut.Invoke(context);

            // Assert
            context.Response.AssertWasCalled(x => x.WriteAsJson(Arg<object>.Is.Anything));
            _nextMiddleware.AssertWasNotCalled(x=>x.Invoke(context));
            
        }
    }

}
