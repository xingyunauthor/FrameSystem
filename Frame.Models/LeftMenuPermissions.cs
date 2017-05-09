using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(LeftMenuPermissions))]
    public class LeftMenuPermissions
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int LeftMenuId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary>
        public int PermissionsId { get; set; }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool Have { get; set; }

        [ForeignKey(nameof(LeftMenuId))]
        public LeftMenus LeftMenusForeign { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Roles RolesForeign { get; set; }

        [ForeignKey(nameof(PermissionsId))]
        public Permissions PermissionsForeign { get; set; }

    }
}
