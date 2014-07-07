using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.UserInterface.Models
{
    /// <summary>
    /// 返回值说明
    /// </summary>
    public class ReturnItem : Item
    {
        /// <summary>
        /// 获取或设置返回值名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置返回值类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置返回值说明
        /// </summary>
        public string Description { get; set; }
    }
}