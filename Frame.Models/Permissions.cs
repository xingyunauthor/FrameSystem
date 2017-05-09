using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Frame.Models
{
    [Table(nameof(Permissions))]
    public class Permissions
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionsName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否是系统级的
        /// </summary>
        public bool Sys { get; set; }

        public override string ToString()
        {
            return PermissionsName;
        }
    }
}
