using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// Access Token错误响应报文
    /// </summary>
    public class TokenErrorResponseModel
    {
        /// <summary>
        /// 获取或设置错误码。
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// 获取或设置错误描述。
        /// </summary>
        public string error_description { get; set; }

        /// <summary>
        /// 获取或设置错误信息uri。
        /// </summary>
        public string error_uri { get; set; }

        public TokenErrorResponseModel()
        {
            error = "";
            error_description = "";
            error_uri = "";
        }
    }
}