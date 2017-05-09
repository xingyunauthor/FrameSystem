using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Frame.Models.interfaces;

namespace Frame.Models
{
    [Table(nameof(TopMenus))]
    public class TopMenus : IMenus
    {
        /// <summary>
        /// 主键 Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 该编号对应唯一的用户控件窗口
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 图标名称
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// 父级编号 Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 动态链接库路径
        /// </summary>
        public string DllPath { get; set; }
        /// <summary>
        /// 入口函数
        /// </summary>
        public string EntryFunction { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 是否为系统级的菜单
        /// </summary>
        public bool Sys { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
