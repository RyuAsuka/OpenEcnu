using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;

namespace OpenEcnu.UserInterface.Controllers
{
	public class HomeController : Controller
	{
		readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
		//
		// GET: /Home/
		public ActionResult Index()
		{
			string userId = GetUserId();
			UsersDetail ud = repo.GetUsersDetail(userId);
			if(ud != null) 
				ViewBag.UserName = ud.Name;
			return View();
		}

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
	}
}