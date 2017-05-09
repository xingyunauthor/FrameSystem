using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Models.SysModels.Permissions
{
    public class PermissionsUpdateRequestModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionsName { get; set; }
    }

    public class PermissionsUpdateSortRequestModel
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public int PermissionsId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
