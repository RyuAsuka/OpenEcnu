namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// ��Ȩ����Ϣʵ��
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// ������Ȩ��Ӧ��AppKey
        /// </summary>
        public int appkey { get; set; }

        /// <summary>
        /// ������Ȩ���û�ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// ��Ȩ�봴��ʱ��
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// ��Ȩ�����ʱ��
        /// </summary>
        public System.DateTime expire { get; set; }

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
