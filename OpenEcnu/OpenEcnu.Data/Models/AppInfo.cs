using System.Collections.Generic;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// Ӧ����Ϣʵ��
    /// </summary>
    public class AppInfo
    {
        public AppInfo()
        {
            AccessTokens = new List<AccessToken>();
            Authorizations = new List<Authorization>();
        }

        /// <summary>
        /// Ӧ�õ�AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// Ӧ�õ�AppSecret
        /// </summary>
        public string appsecret { get; set; }

        /// <summary>
        /// Ӧ�õ������ߵ�ID
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// Ӧ�õ��ض���URI
        /// </summary>
        public string redirecturi { get; set; }

        /// <summary>
        /// Ӧ�õ�����
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Ӧ�õ�������Ϣ
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// ����AccessToken��
        /// </summary>
        public virtual ICollection<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// ����Authorization��
        /// </summary>
        public virtual ICollection<Authorization> Authorizations { get; set; }
    }
}
