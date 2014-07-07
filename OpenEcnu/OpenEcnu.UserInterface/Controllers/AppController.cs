using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net;
using Microsoft.Security.Application;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;
using OpenEcnu.UserInterface.Models;

namespace OpenEcnu.UserInterface.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 从Cookie中取得用户ID
        /// </summary>
        /// <returns>用户ID</returns>
        private string GetUserId()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                return ticket.Name;
            return null;
        }

        //
        // GET: /App/
        public ActionResult Index(string userId)
        {
            if (userId == null)
            {
                try
                {
                    userId = GetUserId();
                    if (userId == null)
                        return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            string userName = repo.GetUsersDetail(userId).Name;
            ViewData["UserName"] = userName;
            List<AppInfo> appInfoList = repo.GetAppInfoes(userId);
            IList<AppModel> appModels = appInfoList.Select(app => new AppModel
            {
                AppKey = app.appkey,
                AppSecret = app.appsecret,
                Description = app.description,
                Name = app.name,
                Owner = app.owner,
                RedirectUri = app.redirecturi
            }).ToList();
            return View(appModels);
        }

        //
        // GET: /App/Create
        public ActionResult Create()
        {
            ViewBag.UserName = repo.GetUsersDetail(GetUserId()).Name;
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppModel appModel)
        {
            if (ModelState.IsValid)
            {
                AppInfo newApp = new AppInfo
                {
                    appsecret = RandomString.RandomStringImpl.CreateRandomString(20),
                    name = appModel.Name,
                    description = appModel.Description,
                    redirecturi = appModel.RedirectUri,
                    owner = GetUserId()
                };
                if (repo.CreateApp(newApp))
                {
                    AppInfo theApp = repo.GetAppInfo(newApp.name);
                    return RedirectToAction("Detail", "App", new { appKey = theApp.appkey });
                }
                ViewBag.ErrorMessage = "对不起，这个应用已经有人申请过了！请换个名字试试";
            }
            
            ViewBag.UserName = repo.GetUsersDetail(GetUserId()).Name;
            return View(appModel);
        }

        //
        // GET: /App/Modify/{appKey}
        public ActionResult Modify(int appKey)
        {
            AppInfo app = repo.GetAppInfo(appKey);
            if (app == null)
            {
                ViewBag.ErrorMessage = "找不到指定的应用！";
                return View();
            }
            AppModel model = new AppModel
            {
                AppKey = app.appkey,
                AppSecret = app.appsecret,
                RedirectUri = app.redirecturi,
                Name = app.name,
                Description = app.description,
                Owner = app.owner
            };
            string userName = repo.GetUsersDetail(app.owner).Name;
            ViewBag.UserName = userName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Modify(AppModel model)
        {
            if (ModelState.IsValid)
            {
                AppInfo newAppInfo = new AppInfo
                {
                    appsecret = model.AppSecret,
                    owner = model.Owner,
                    name = model.Name,
                    redirecturi = model.RedirectUri,
                    description = model.Description
                };
                if (repo.UpdateApp(model.AppKey, newAppInfo))
                    return RedirectToAction("Detail", new { appKey = model.AppKey });
                ModelState.AddModelError("", "Update Failed");
                ViewBag.ErrorMessage = "你所使用的应用名称已被使用，请换一个试试！";
            }
            ViewBag.UserName = repo.GetUsersDetail(GetUserId()).Name;
            return View(model);
        }

        public ActionResult Delete(int appKey)
        {
            AppInfo app = repo.GetAppInfo(appKey);
            if (app == null)
            {
                ViewBag.ErrorMessage = "找不到指定的应用！";
                return View();
            }
            AppModel model = new AppModel
            {
                AppKey = app.appkey,
                AppSecret = app.appsecret,
                RedirectUri = app.redirecturi,
                Name = app.name,
                Description = app.description,
                Owner = app.owner
            };
            string userName = repo.GetUsersDetail(app.owner).Name;
            ViewBag.UserName = userName;
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmedResult(int appKey)
        {
            if (repo.DeleteApp(appKey))
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Detail(int appKey)
        {
            AppInfo app = repo.GetAppInfo(appKey);
            if (app == null)
            {
                ViewBag.ErrorMessage = "找不到指定的应用！";
                return View();
            }
            AppModel model = new AppModel
            {
                AppKey = app.appkey,
                AppSecret = app.appsecret,
                RedirectUri = app.redirecturi,
                Name = app.name,
                Description = app.description,
                Owner = app.owner
            };
            string userName = repo.GetUsersDetail(GetUserId()).Name;
            ViewBag.UserName = userName;
            string ownerName = repo.GetUsersDetail(app.owner).Name;
            ViewBag.Owner = ownerName;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            repo.DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}