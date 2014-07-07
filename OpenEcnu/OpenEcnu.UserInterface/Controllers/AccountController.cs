using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;
using OpenEcnu.UserInterface.Models;

namespace OpenEcnu.UserInterface.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (repo.UserLogin(loginModel.UserId, loginModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginModel.UserId, loginModel.AutoLogin);
                    //logger.InfoFormat("用户{0}登录成功",loginModel.UserId);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                                            && returnUrl.StartsWith("/")
                                            && !returnUrl.StartsWith("//") &&
                                            !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                //logger.ErrorFormat("用户{0}试图登录失败",loginModel.UserId);
                ModelState.AddModelError("","登录失败，用户名或密码错误");
            }
            return View(loginModel);
        }

        //
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
	}
}