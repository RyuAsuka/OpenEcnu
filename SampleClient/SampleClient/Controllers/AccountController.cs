using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using OpenEcnuSDK;
using OpenEcnuSDK.Entities;
using SampleClient.Models;

namespace SampleClient.Controllers
{
    public class AccountController : Controller
    {
        // 这里填写所注册应用的APP_KEY, APP_SECRET和REDIRECT_URI
        string AppKey = Properties.Settings.Default.AppKey;
        string AppSecret = Properties.Settings.Default.AppSecret;
        string RedirectUri = Properties.Settings.Default.RedirectUri;

        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Oauth oauth = new Oauth();
        //
        // GET: /Account/
        public ActionResult Login()
        {
            if (Request.Cookies["oauth"] != null && !string.IsNullOrEmpty(Request.Cookies["oauth"]["userId"]) && !string.IsNullOrEmpty(Request.Cookies["oauth"]["token"]))
            {
                string userId = Request.Cookies["oauth"]["userId"];
                string token = Request.Cookies["oauth"]["token"];
                UserInfo user = oauth.UsersShow(token, userId);
                UserModel um = new UserModel
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Birthday = user.Birthday,
                    Email = user.Email,
                    Gender = user.Gender
                };

                Session["userId"] = um.UserId;
                Session["name"] = um.Name;
                Session["gender"] = um.Gender;
                Session["email"] = um.Email;
                Session["birthday"] = um.Birthday;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult OauthLogin(string code)
        {
            UserModel um;
            AccessToken token;
            logger.Info(string.Format("AppKey={0},AppSecret={1},RedirectUri={2},code={3}",AppKey,AppSecret,RedirectUri,code));
            if (code == null)
                return
                    Redirect(oauth.GetAuthorizationCode(int.Parse(AppKey),RedirectUri));

            try
            {
                token = oauth.GetAccessToken(AppKey, AppSecret, RedirectUri, code);
                if(token == null) throw new NullReferenceException();
                logger.InfoFormat("token: {0},{1},{2},{3}",token.access_token,token.expires_in,token.user_id,token.token_type);
                UserInfo user = oauth.UsersShow(token.user_id);
                if(user == null) throw new NullReferenceException();
                logger.InfoFormat("user: {0}",user.UserId);
                um = new UserModel
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Birthday = user.Birthday,
                    Email = user.Email,
                    Gender = user.Gender
                };
            }
            catch (Exception ex)
            {
                logger.Fatal("跳转到异常",ex);
                return RedirectToAction("Error", "Account", new { message = "登录失败！" });
            }

            HttpCookie cookie = new HttpCookie("oauth");
            cookie["userId"] = um.UserId;
            cookie["token"] = oauth.Token.access_token;
            cookie.Expires = DateTime.Now.AddSeconds(token.expires_in);
            Response.Cookies.Add(cookie);

            Session["userId"] = um.UserId;
            Session["name"] = um.Name;
            Session["gender"] = um.Gender;
            Session["email"] = um.Email;
            Session["birthday"] = um.Birthday;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}