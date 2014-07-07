using System.Collections.Generic;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 应用信息实体
    /// </summary>
    public class AppInfo
    {
        public AppInfo()
        {
            AccessTokens = new List<AccessToken>();
            Authorizations = new List<Authorization>();
        }

        /// <summary>
        /// 应用的AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// 应用的AppSecret
        /// </summary>
        public string appsecret { get; set; }

        /// <summary>
        /// 应用的所有者的ID
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 应用的重定向URI
        /// </summary>
        public string redirecturi { get; set; }

        /// <summary>
        /// 应用的名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 应用的描述信息
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 关联AccessToken表
        /// </summary>
        public virtual ICollection<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// 关联Authorization表
        /// </summary>
        public virtual ICollection<Authorization> Authorizations { get; set; }
    }
}
