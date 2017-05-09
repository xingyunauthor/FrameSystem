namespace Frame.Models.SysModels.LeftMenuPermissions
{
    public class ModifyPermissionsRequestModel
    {
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
    }
}
