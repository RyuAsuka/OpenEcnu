using System.Net;
using System.Net.Http;

using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenEcnu.ResourceServer.Controllers.Users;

namespace OpenEcnu.ResourceServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string access_token = "w5s8bGXZXZIElNUFIHfI";
            string user_id = "10102130253";

            ShowController controller = new ShowController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            var result = controller.GetUserById(access_token, user_id);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, string.Format("测试失败，期待的状态码为200，实际为{0}, 内容为{1}", result.StatusCode, result.Content.ReadAsStringAsync().Result));
            string content = result.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(
                "{\"UserId\":\"10102130253\",\"Name\":\"Asuka\",\"Gender\":\"男\",\"Email\":\"paulexe@163.com\",\"Birthday\":\"1991年10月11日\"}",
                content);
        }
    }
}
