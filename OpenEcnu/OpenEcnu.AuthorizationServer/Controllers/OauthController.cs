using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using OpenEcnu.AuthorizationServer.Models;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;

namespace OpenEcnu.AuthorizationServer.Controllers
{
    public class OauthController : Controller
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Authorize接口

        //
        // GET: /Oauth/Authorize
        public ActionResult Authorize(string client_id, string redirect_uri, string response_type, string state = null)
        {
            int clientId = Convert.ToInt32(client_id);
            redirect_uri = HttpUtility.UrlEncode(redirect_uri); // 将redirect_uri进行URL编码以方便后续的处理

            #region 若用户没有登录则跳转到登录页面
            if (Request.Cookies["login"] == null)
            {
                CodeRequestModel codeRequest = new CodeRequestModel
                {
                    client_id = clientId,
                    redirect_uri = redirect_uri,
                    response_type = response_type,
                    state = state
                };
                Session.Add("codeRequest", codeRequest);

                return RedirectToAction("Login");
            }
            #endregion

            string userId = Request.Cookies["login"]["userId"];

            #region 若response_type不为code则进入出错页面
            if (response_type != "code")
                return RedirectToAction("Error", new { errorCode = 101 }); // 响应类型不为code

            #endregion

            string appName = repo.GetAppInfo(clientId).name;
            string userName = repo.GetUsersDetail(userId).Name;

            if (appName == null || string.IsNullOrEmpty(appName))
                return RedirectToAction("Error", new {errorCode = 102});

            UserAppModel userAppModel = new UserAppModel
            {
                AppKey = clientId,
                AppName = appName,
                RedirectUri = redirect_uri,
                UserId = userId,
                UserName = userName
            };

            Session.Add("app", userAppModel);

            return View(userAppModel); // 返回用户确认授权页面，如果用户确认授权，则将模型回发给带有HttpPost特性的Authorize方法
        }

        [HttpPost]
        public ActionResult Authorize()
        {
            UserAppModel userAppModel = Session["app"] as UserAppModel;

            if (userAppModel == null)
                return RedirectToAction("Error", new { errorCode = 102 }); // 错误请求，Session已过期

            int appKey = userAppModel.AppKey;
            string redirectUri = userAppModel.RedirectUri;
            string redirectUriInDb = repo.GetAppInfo(appKey).redirecturi;

            if (Server.UrlEncode(redirectUriInDb) != redirectUri)
                return RedirectToAction("Error", new { errorCode = 103 }); // 应用的回调地址与请求不一致

            string code = RandomString.RandomStringImpl.CreateRandomString(20); // 产生授权码
            redirectUri = HttpUtility.UrlDecode(redirectUri); // 将uri解码，用于重定向

            Authorization auth = new Authorization
            {
                appkey = appKey,
                userid = userAppModel.UserId,
                code = code,
                createtime = DateTime.Now,
                expire = DateTime.Now.AddMinutes(1) //TODO:暂时设为1分钟过期，部署后可能需要调整
            };

            if (repo.InsertAuthorization(auth)) // 如果授权码成功存储，则重定向回应用的重定向URI，否则跳转到出错界面
                return Redirect(redirectUri + "?code=" + code);

            return RedirectToAction("Error", new { errorCode = 104 }); // 未能产生合法的授权码
        }

        #endregion

        #region Login接口

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel loginModel)
        {
            if (!repo.UserLogin(loginModel.UserId, loginModel.Password))
                return RedirectToAction("Error", new { errorCode = 401 });

            // 登录成功后生成cookie并跳转回授权确认页面，否则跳转到错误页面
            HttpCookie cookie = new HttpCookie("login");
            cookie["userId"] = loginModel.UserId;
            cookie.Expires = DateTime.Now.AddSeconds(30); //TODO: 登录cookie暂时设为30秒过期，部署后应延长
            Response.Cookies.Add(cookie);

            CodeRequestModel codeRequest = Session["codeRequest"] as CodeRequestModel;
            if (codeRequest != null)
                return Redirect(string.Format("~/oauth/authorize?client_id={0}&redirect_uri={1}&response_type={2}&state={3}", codeRequest.client_id, codeRequest.redirect_uri, codeRequest.response_type, codeRequest.state));

            return RedirectToAction("Error", new { errorCode = 102 });
        }

        #endregion

        #region Error接口

        public ActionResult Error(int errorCode)
        {
            ViewBag.ErrorCode = errorCode;
            return View();
        }

        #endregion
    }
}