using System;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// ��������ʵ��
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// ����������Ƶ�Ӧ��AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// ����������Ƶ��û�ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        public string accesstoken1 { get; set; }

        /// <summary>
        /// �������ƵĴ���ʱ��
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// �������ƹ���ʱ��
        /// </summary>
        public DateTime expire { get; set; }

        /// <summary>
        /// ����AppInfo��
        /// </summary>
        public virtual AppInfo AppInfo { get; set; }

        /// <summary>
        /// ����User��
        /// </summary>
        public virtual User User { get; set; }
    }
}
