namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// 表示请求授权的用户和请求授权的应用的模型，用于通过控制器传递用户与应用数据
    /// </summary>
    public class UserAppModel
    {
        /// <summary>
        /// 应用的AppKey
        /// </summary>
        public int AppKey { get; set; }

        /// <summary>
        /// 应用的名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 用户的ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户的名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 应用的回调URI
        /// </summary>
        public string RedirectUri { get; set; }
    }
}