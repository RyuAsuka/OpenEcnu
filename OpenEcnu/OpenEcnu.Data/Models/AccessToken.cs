using System;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 访问令牌实体
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 请求访问令牌的应用AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// 请求访问令牌的用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        public string accesstoken1 { get; set; }

        /// <summary>
        /// 访问令牌的创建时间
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        public DateTime expire { get; set; }

        /// <summary>
        /// 关联AppInfo表
        /// </summary>
        public virtual AppInfo AppInfo { get; set; }

        /// <summary>
        /// 关联User表
        /// </summary>
        public virtual User User { get; set; }
    }
}
