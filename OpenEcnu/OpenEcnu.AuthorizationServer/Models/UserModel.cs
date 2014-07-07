using System.ComponentModel;

namespace OpenEcnu.AuthorizationServer.Models
{
    /// <summary>
    /// 用户登录模型，用于登录时传递登录数据
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }
    }
}