using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;
using OpenEcnu.UserInterface.Models;

namespace OpenEcnu.UserInterface.Controllers
{
    public class ApiListController : Controller
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
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
        // GET: /ApiList/
        public ActionResult Index()
        {
            ViewBag.UserName = GetUserId() != null ? repo.GetUsersDetail(GetUserId()).Name : null;
            return View();
        }

        public ActionResult OauthAuthorize()
        {
            ViewBag.UserName = GetUserId() != null ? repo.GetUsersDetail(GetUserId()).Name : null;
            List<TableItem> tableList = new List<TableItem>
            {
                new TableItem {Name = "client_id", IsRequired = true, Type = "string", Description = @"申请应用时分配的appkey"},
                new TableItem
                {
                    Name = "redirect_uri",
                    IsRequired = true,
                    Type = "string",
                    Description = @"授权回调地址，必须和所申请的相一致。"
                },
                new TableItem {Name = "response_type", IsRequired = true, Type = "string", Description = "请求类型，必须为\"code\""},
                new TableItem{Name = "state", IsRequired = false, Type = "string", Description = "用于保持请求和回调的状态"}
            };

            List<ReturnItem> returnList = new List<ReturnItem>
            {
                new ReturnItem{Name = "code", Type = "string", Description = "用于调用Access Token接口"},
                new ReturnItem{Name = "state", Type = "string", Description = "如果传递参数，会回传该参数。"}
            };

            List<IList> model = new List<IList>
            {
                tableList,
                returnList
            };
            return View(model);
        }

        public ActionResult OauthToken()
        {
            ViewBag.UserName = GetUserId() != null ? repo.GetUsersDetail(GetUserId()).Name : null;

            List<TableItem> tableList = new List<TableItem>
            {
                new TableItem {Name = "client_id", IsRequired = true, Type = "string", Description = @"申请应用时分配的appkey"},
                new TableItem
                {
                    Name = "client_secret",
                    IsRequired = true,
                    Type = "string",
                    Description = @"申请应用时分配的AppSecret。"
                },
                new TableItem {Name = "grant_type", IsRequired = true, Type = "string", Description = "请求的类型，填写authorization_code"},
                new TableItem{Name = "code", IsRequired = true, Type = "string", Description = "调用authorize获得的code值。"},
                new TableItem{Name = "redirect_uri",IsRequired = true, Type = "string",Description = "回调地址，需需与注册应用里的回调地址一致。"}
            };

            List<ReturnItem> returnList = new List<ReturnItem>
            {
                new ReturnItem{Name = "access_token", Type = "string", Description = "用于调用access_token，接口获取授权后的access token。"},
                new ReturnItem{Name = "expires_in", Type = "int", Description = "access_token的生命周期，单位是秒数。"},
                new ReturnItem{Name = "token_type", Type = "string",Description = "令牌类型"},
                new ReturnItem{Name = "user_id",Type = "string",Description = "请求授权的用户ID"}
            };

            List<IList> model = new List<IList>
            {
                tableList,
                returnList
            };

            return View(model);
        }

        public ActionResult UsersShow()
        {
            ViewBag.UserName = GetUserId() != null ? repo.GetUsersDetail(GetUserId()).Name : null;

            List<TableItem> tableList = new List<TableItem>
            {
                new TableItem {Name = "access_token", IsRequired = true, Type = "string", Description = @"获取的Access Token"},
                new TableItem
                {
                    Name = "user_id",
                    IsRequired = true,
                    Type = "string",
                    Description = @"想查询的User ID"
                },
            };

            List<ReturnItem> returnList = new List<ReturnItem>
            {
                new ReturnItem{Name = "user_id", Type = "string", Description = "用户的ID"},
                new ReturnItem{Name = "name", Type = "string", Description = "用户的姓名"},
                new ReturnItem{Name = "email", Type = "string", Description = "用户的Email地址"},
                new ReturnItem{Name = "birthday", Type = "string", Description = "用户的生日"},
                new ReturnItem{Name = "gender", Type = "string", Description = "用户的性别"}
            };

            List<IList> model = new List<IList>
            {
                tableList,
                returnList
            };

            return View(model);
        }
    }
}