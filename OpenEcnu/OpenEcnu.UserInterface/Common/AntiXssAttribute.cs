using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Security.Application;

namespace OpenEcnu.UserInterface.Common
{
    public class AntiXssAttribute : ValidationAttribute, IMetadataAware
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var str = value as string;
        //    if (!string.IsNullOrEmpty(str) &&
        //        validationContext.ObjectInstance != null &&
        //        !string.IsNullOrWhiteSpace(validationContext.MemberName))
        //    {
        //        str = Sanitizer.GetSafeHtmlFragment(str);
        //        if (string.IsNullOrEmpty(str))
        //        {
        //            return new ValidationResult("请不要输入非法数据");
        //        }
        //        PropertyInfo pi = validationContext.ObjectType.GetProperty(validationContext.MemberName,
        //            BindingFlags.Public | BindingFlags.Instance);

        //        pi.SetValue(validationContext.ObjectInstance, str);
        //    }
        //    return ValidationResult.Success;
        //}

        public override bool IsValid(object value)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                str = Sanitizer.GetSafeHtmlFragment(str);
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
            }
            return true;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.RequestValidationEnabled = false;
        }
    }
}