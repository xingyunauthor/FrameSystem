using System.Data;
using System.Linq;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenuPermissions;

namespace Frame.Business.interfaces
{
    public interface ILeftMenuPermissionsManage : IBaseManage<LeftMenuPermissions>
    {
        /// <summary>
        /// 根据角色编号查询该角色所有的授权信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="displayNameSearchKey">菜单名称搜索关键词</param>
        /// <returns></returns>
        DataTable GetLeftMenuPermissions(int roleId, string displayNameSearchKey);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<object> ModifyPermissions(ModifyPermissionsRequestModel requestModel);

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="menuId">菜单唯一标识</param>
        /// <param name="permissionsId">权限编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        bool PermissionHave(string menuId, int permissionsId, int roleId);
    }
}