using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// Access Token响应报文
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// 获取或设置Access Token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 获取或设置Token类型，暂定为"AccessToken"
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 获取或设置过期时间，单位为秒，默认为一天（86400秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 获取或设置授权的用户id
        /// </summary>
        public string user_id { get; set; }

        public TokenResponseModel()
        {
            access_token = "";
            token_type = "AccessToken";
            expires_in = 86400;
            user_id = "";
        }
    }
}