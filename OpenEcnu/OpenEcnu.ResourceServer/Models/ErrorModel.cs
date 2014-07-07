using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.ResourceServer.Models
{
    /// <summary>
    /// 请求出错的模型
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Request { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}