using System;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// �û���ϸ��Ϣʵ��
    /// </summary>
    public class UsersDetail
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �û��Ա�
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// �û�Email��ַ
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// ��User�����
        /// </summary>
        public virtual User User { get; set; }
    }
}
