using System;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace OpenEcnu.UserInterface.Tests
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void RouteTest1()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void RouteTest2()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/App");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("App", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void RouteTest3()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/App/Create");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("App", routeData.Values["controller"]);
            Assert.AreEqual("Create", routeData.Values["action"]);
        }

        [TestMethod]
        public void RouteTest4()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/App/Detail/1");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("App", routeData.Values["controller"]);
            Assert.AreEqual("Detail", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["appKey"]);
        }

        [TestMethod]
        public void RouteTest5()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/App/Delete/1");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("App", routeData.Values["controller"]);
            Assert.AreEqual("Delete", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["appKey"]);

        }

        [TestMethod]
        public void RouteTest6()
        {
            // Arrange
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/App/Modify/1");
            var routes = new RouteCollection();
            OpenEcnu.UserInterface.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("App",routeData.Values["controller"]);
            Assert.AreEqual("Modify",routeData.Values["action"]);
            Assert.AreEqual("1",routeData.Values["appKey"]);

        }
    }
}
