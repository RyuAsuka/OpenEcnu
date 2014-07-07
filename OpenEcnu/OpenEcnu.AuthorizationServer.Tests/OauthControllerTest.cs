using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace OpenEcnu.AuthorizationServer.Tests
{
    [TestFixture]
    public class OauthControllerTest
    {
        [Test]
        public void RouteForEmbeddedResourceTest()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/oauth/authorize");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(mockContext.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("oauth", routeData.Values["controller"]);
            Assert.AreEqual("authorize",routeData.Values["action"]);
        }
    }
}
