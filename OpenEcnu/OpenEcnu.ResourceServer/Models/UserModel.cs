namespace OpenEcnu.ResourceServer.Models
{
    /// <summary>
    /// 用户信息实体模型
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Gender { get; set; }
        
        /// <summary>
        /// 用户电子邮件地址
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 字符串形式表示的用户生日
        /// </summary>
        public string Birthday { get; set; }
    }
}