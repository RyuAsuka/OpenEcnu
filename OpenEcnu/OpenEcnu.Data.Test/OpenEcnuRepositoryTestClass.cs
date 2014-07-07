using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenEcnu.Data.Models;

namespace OpenEcnu.Data.Test
{
    [TestClass]
    public class OpenEcnuRepositoryTestClass
    {
        [TestMethod]
        public void GetUserTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            User user = repo.GetUser("10102130253");

            Assert.IsNotNull(user, "测试失败！user对象为空！");
            Assert.AreEqual("10102130253", user.userid, string.Format("测试失败！期待的值是10102130253,实际为{0}", user.userid));
            Assert.AreEqual("10102130253", user.passwd, string.Format("测试失败！期待的值是10102130253,实际为{0}", user.passwd));
            Assert.AreEqual("Asuka", user.UsersDetail.Name, string.Format("测试失败！期待的值是Asuka,实际为{0}", user.UsersDetail.Name));

            repo.Dispose();
        }

        [TestMethod]
        public void GetUsersDetailTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            UsersDetail ud = repo.GetUsersDetail("10102130253");

            Assert.IsNotNull(ud, "测试失败！ud为空");
            Assert.AreEqual("Asuka", ud.Name, string.Format("测试失败！期待的值是Asuka,实际为{0}", ud.Name));
            Assert.AreEqual("paulexe@163.com", ud.Email, string.Format("测试失败！期待的值是paulexe@163.com,实际为{0}", ud.Email));
            Assert.AreEqual("男", ud.Gender, string.Format("测试失败！期待的值是\"男\",实际为{0}", ud.Gender));
            Assert.AreEqual(DateTime.Parse("1991-10-11 0:00:00"), ud.Birthday, string.Format("测试失败！期待的值是\"1991-10-11\",实际为{0}", ud.Birthday));

            repo.Dispose();
        }

        [TestMethod]
        public void GetAppInfoTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            AppInfo app = repo.GetAppInfo(1);

            Assert.IsNotNull(app, "测试失败！app为空！");
            Assert.AreEqual(1, app.appkey);
            Assert.AreEqual("0Jo0JBfNrE2ScF2iDVPn", app.appsecret);
            Assert.AreEqual("10102130253", app.owner);
            Assert.AreEqual("http://localhost:13342/Account/OauthLogin", app.redirecturi);
            Assert.AreEqual("Test", app.name);
            Assert.AreEqual("The first Test APp", app.description);

            repo.Dispose();
        }

        [TestMethod]
        public void GetAppInfoesTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.GetAppInfoes("10102130253");

            Assert.IsNotNull(result);
            var appInfos = result as IList<AppInfo> ?? result.ToList();
            Assert.AreEqual(1,appInfos.Count());
            Assert.AreEqual(1,appInfos.ElementAt(0).appkey);
            repo.Dispose();
        }

        [TestMethod]
        public void GetAuthorizationByUserIdAndAppKeyTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            Authorization auth = repo.GetAuthorizationByUserIdAndAppKey("10102130253", 1);

            Assert.IsNull(auth);
            //Assert.AreEqual("FJCiry3myLIZWC8zPlp3",auth.code);

            repo.Dispose();
        }

        [TestMethod]
        public void GetAuthorizationByCode()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            Authorization auth = repo.GetAuthorizationByCode("12345qwert12345qwert");

            //Assert.IsNotNull(auth);
            //Assert.AreEqual("10102130253",auth.userid);
            //Assert.AreEqual(1,auth.appkey);
            Assert.IsNull(auth);

            repo.Dispose();
        }

        [TestMethod]
        public void InsertAuthorizationTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());


            bool result = repo.InsertAuthorization(new Authorization
            {
                appkey = 1,
                code = "12345qwert54321trewq",
                userid = "10102130253",
                createtime = DateTime.Now,
                expire = DateTime.Now.AddMinutes(1)
            });
            repo.DbContext.SaveChanges();
            Assert.IsTrue(result);

            repo.Dispose();

        }

        [TestMethod]
        public void DeleteAuthorizationTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            bool result = repo.DeleteAuthorization(1, "10102130253");
            repo.DbContext.SaveChanges();
            Assert.IsTrue(result);

            repo.Dispose();
        }

        [TestMethod]
        public void GetAccessTokenTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.GetAccessToken(1, "10102130253");
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.appkey);
            Assert.AreEqual("qweqweqweqweqweqwqwe", result.accesstoken1);

            repo.Dispose();
        }

        [TestMethod]
        public void InsertAccessTokenTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.InsertAccessToken(new AccessToken
            {
                appkey = 1,
                userid = "10102130253",
                accesstoken1 = "qweqweqweqweqweqwqwe",
                createtime = DateTime.Now,
                expire = DateTime.Now.AddMinutes(1)
            });
            repo.DbContext.SaveChanges();
            Assert.IsTrue(result);

            repo.Dispose();
        }

        [TestMethod]
        public void DeleteAccessTokenTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.DeleteAccessToken(1, "10102130253");
            repo.DbContext.SaveChanges();
            Assert.IsTrue(result);

            repo.Dispose();
        }

        [TestMethod]
        public void UserLoginTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.UserLogin("10102130253", "10102130253");

            Assert.IsTrue(result);

            repo.Dispose();
        }

        [TestMethod]
        public void ValidateAccessTokenTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.ValidateAccessToken("qweqweqweqweqweqwqwe", "10102130253");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsAccessTokenExistsTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            var result = repo.IsAccessTokenExists(1, "qwertqwertqwertqwert");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAppTest()
        {
            OpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());

            AppInfo app = new AppInfo
            {
                appsecret = "12341234123412341234",
                redirecturi = "123",
                owner = "10102130253",
                name = "Test2",
                description = "Test2"
            };

            bool result = repo.UpdateApp(1, app);

            Assert.IsTrue(result);
        }
    }
}
