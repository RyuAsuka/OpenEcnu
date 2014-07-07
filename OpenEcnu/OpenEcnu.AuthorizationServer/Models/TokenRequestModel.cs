namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// Access Token请求报文的参数
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// 获取客户端id（即AppKey）
        /// </summary>
        public int client_id { get; set; }

        /// <summary>
        /// 获取客户端密钥。
        /// </summary>
        public string client_secret { get; set; }

        /// <summary>
        /// 获取请求类型。默认值为code。
        /// </summary>
        public string grant_type { get; set; }

        /// <summary>
        /// 获取应用的回调URI。
        /// </summary>
        public string redirect_uri { get; set; }

        /// <summary>
        /// 获取Authorization Code。
        /// </summary>
        public string code { get; set; }
    }
}