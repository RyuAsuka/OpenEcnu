using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SampleClient.Models;

namespace SampleClient.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index(UserModel model)
        {
            return View(model);
        }
	}
}