using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEcnu.UserInterface.Models
{
    /// <summary>
    /// 参数列表表项
    /// </summary>
    public class TableItem : Item
    {
        /// <summary>
        /// 获取或设置参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置是否必须
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 获取或设置参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置参数说明
        /// </summary>
        public string Description { get; set; }
    }
}