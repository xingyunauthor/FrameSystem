using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    //NavBarGroups
    [Table(nameof(NavBarGroups))]
    public class NavBarGroups
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ico
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// Sort
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 是否为系统级的菜单
        /// </summary>
        public bool Sys { get; set; }
    }
}