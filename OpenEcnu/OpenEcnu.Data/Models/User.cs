using System.Collections.Generic;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 用户基本信息实体
    /// </summary>
    public class User
    {
        public User()
        {
            AccessTokens = new List<AccessToken>();
            Authorizations = new List<Authorization>();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string passwd { get; set; }

        /// <summary>
        /// 关联AccessToken表
        /// </summary>
        public virtual ICollection<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// 关联Authorization表
        /// </summary>
        public virtual ICollection<Authorization> Authorizations { get; set; }

        /// <summary>
        /// 关联UsersDetail表
        /// </summary>
        public virtual UsersDetail UsersDetail { get; set; }
    }
}
