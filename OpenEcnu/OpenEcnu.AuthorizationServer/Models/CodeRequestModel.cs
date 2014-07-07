using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// 授权码请求模型，用于保存授权码请求状态
    /// </summary>
    public class CodeRequestModel
    {
        /// <summary>
        /// 应用AppKey
        /// </summary>
        public int client_id { get; set; }
        
        /// <summary>
        /// 应用的重定向URI
        /// </summary>
        public string redirect_uri { get; set; }
        
        /// <summary>
        /// 响应类型，默认为code，暂不支持其他请求方式
        /// </summary>
        public string response_type { get; set; }
        
        /// <summary>
        /// 应用请求状态，该参数待定，默认为null
        /// </summary>
        public string state { get; set; }
    }
}