using System;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 用户详细信息实体
    /// </summary>
    public class UsersDetail
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 用户Email地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 与User表关联
        /// </summary>
        public virtual User User { get; set; }
    }
}
