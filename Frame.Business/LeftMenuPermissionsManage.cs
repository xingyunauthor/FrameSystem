using System.Data;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenuPermissions;

namespace Frame.Business
{
    public class LeftMenuPermissionsManage : BaseManage<LeftMenuPermissions, LeftMenuPermissionsService>, ILeftMenuPermissionsManage
    {
        private readonly LeftMenuPermissionsService _leftMenuPermissionsService = new LeftMenuPermissionsService();
        private readonly RolesService _rolesService = new RolesService();
        private readonly PermissionsService _permissionsService = new PermissionsService();
        private readonly LeftMenusService _leftMenusService = new LeftMenusService();

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="displayNameSearchKey">菜单名称搜索关键词</param>
        /// <returns></returns>
        public DataTable GetLeftMenuPermissions(int roleId, string displayNameSearchKey)
        {
            using (var db = new FrameContext())
            {
                return _leftMenuPermissionsService.GetLeftMenuPermissions(db, roleId, displayNameSearchKey);
            }
        }

        public bool PermissionHave(string menuId, int permissionsId, int roleId)
        {
            using (var db = new FrameContext())
            {
                var model =
                    BaseService.Find(db,
                        a => a.LeftMenusForeign.MenuId == menuId && a.PermissionsId == permissionsId && a.RoleId == roleId);
                return model?.Have ?? false;
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <returns></returns>
        public CommandResult<object> ModifyPermissions(ModifyPermissionsRequestModel requestModel)
        {
            var result = new CommandResult<object>();
            using (var db = new FrameContext())
            {
                if (!_rolesService.Exist(db, a => a.Id == requestModel.RoleId))
                    result.Message = "该角色在系统中已不存在，请刷新后再试";
                else if (!_leftMenusService.Exist(db, a => a.Id == requestModel.LeftMenuId))
                    result.Message = "该菜单在系统中已不存在，请刷新后再试";
                else if (!_permissionsService.Exist(db, a => a.Id == requestModel.PermissionsId))
                    result.Message = "该权限系统中已不存在，请刷新后再试";
                else
                {
                    var entity = BaseService.Find(db, a =>
                    a.RoleId == requestModel.RoleId && a.LeftMenuId == requestModel.LeftMenuId &&
                    a.PermissionsId == requestModel.PermissionsId);
                    if (entity != null)
                    {
                        entity.Have = requestModel.Have;
                        if (!BaseService.Update(db, entity))
                            result.Message = "系统未知原因";
                        else
                            result.ResultStatus = ResultStatus.Success;
                    }
                    else
                    {
                        var model = new LeftMenuPermissions
                        {
                            RoleId = requestModel.RoleId,
                            LeftMenuId = requestModel.LeftMenuId,
                            PermissionsId = requestModel.PermissionsId,
                            Have = requestModel.Have
                        };
                        if (!BaseService.Add(db, model))
                            result.Message = "系统未知原因";
                        else
                            result.ResultStatus = ResultStatus.Success;
                    }
                }
            }
            return result;
        }
    }
}
