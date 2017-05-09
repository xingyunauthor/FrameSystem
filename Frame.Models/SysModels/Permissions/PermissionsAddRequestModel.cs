using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Models.SysModels.Permissions
{
    public class PermissionsAddRequestModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionsName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
