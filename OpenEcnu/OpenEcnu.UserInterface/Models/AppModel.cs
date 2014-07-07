using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenEcnu.UserInterface.Common;

namespace OpenEcnu.UserInterface.Models
{
    public class AppModel
    {
        public int AppKey { get; set; }
        public string AppSecret { get; set; }

        [Required(ErrorMessage = "请填写重定向地址")]
        [Display(Name = "应用的重定向URI")]
        [AntiXss(ErrorMessage = "请不要填写非法内容")]
        public string RedirectUri { get; set; }
        public string Owner { get; set; }
        
        [Required(ErrorMessage = "请填写应用名称")]
        [Display(Name = "应用名称")]
        [AntiXss(ErrorMessage = "请不要填写非法内容")]
        public string Name { get; set; }

        [Display(Name = "应用的描述")]
        [AntiXss(ErrorMessage = "请不要填写非法内容")]
        public string Description { get; set; }
    }
}