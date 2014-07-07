namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 授权码信息实体
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// 请求授权的应用AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// 请求授权的用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 授权码创建时间
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// 授权码过期时间
        /// </summary>
        public System.DateTime expire { get; set; }

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
