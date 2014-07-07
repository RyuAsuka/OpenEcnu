using System.Collections.Generic;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// �û�������Ϣʵ��
    /// </summary>
    public class User
    {
        public User()
        {
            AccessTokens = new List<AccessToken>();
            Authorizations = new List<Authorization>();
        }

        /// <summary>
        /// �û�ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        public string passwd { get; set; }

        /// <summary>
        /// ����AccessToken��
        /// </summary>
        public virtual ICollection<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// ����Authorization��
        /// </summary>
        public virtual ICollection<Authorization> Authorizations { get; set; }

        /// <summary>
        /// ����UsersDetail��
        /// </summary>
        public virtual UsersDetail UsersDetail { get; set; }
    }
}
