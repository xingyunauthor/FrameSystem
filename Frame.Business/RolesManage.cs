using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Roles;

namespace Frame.Business
{
    public class RolesManage : BaseManage<Roles, RolesService>, IRolesManage
    {
        private readonly LeftMenuPermissionsService _leftMenuPermissionsService = new LeftMenuPermissionsService();
        private readonly StaffRoleRelationshipsService _staffRoleRelationshipsService = new StaffRoleRelationshipsService();
        public List<Roles> GetAll()
        {
            using (var db = new FrameContext())
            {
                return BaseService.GetAll(db, true, a => a.Id).ToList();
            }
        }

        public CommandResult<Roles> Add(string roleName, long timestamp)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Roles> { ResultStatus = ResultStatus.Error };
                if (BaseService.Exist(db, a => a.RoleName == roleName))
                    result.Message = "角色名称在系统中已存在！";
                else
                {
                    var entity = new Roles { RoleName = roleName, Timestamp = timestamp };
                    if (!BaseService.Add(db, entity))
                        result.Message = "系统未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "角色添加成功！";
                        result.Data = entity;
                    }
                }
                return result;
            }
        }

        public CommandResult<Roles> Update(RoleEditRequestModel model, RoleEditRequestNewModel newModel)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Roles> { ResultStatus = ResultStatus.Error };
                if (!BaseService.Exist(db, a => a.Id == model.RoleId))
                    result.Message = "该角色信息在数据库中已不存在！";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == model.RoleId);
                    if (!BaseService.Exist(db, a => a.Id == model.RoleId && a.Timestamp == model.Timestamp))
                        result.Message = "该信息已在别处修改";
                    else
                    {
                        if (entity.RoleName == newModel.RoleName)
                            result.Message = "角色名称与原名称相同";
                        else
                        {
                            if (BaseService.Exist(db, a => a.RoleName == newModel.RoleName && a.Id != model.RoleId))
                                result.Message = "角色名称在系统中已存在";
                            else
                            {
                                entity.RoleName = newModel.RoleName;
                                entity.Timestamp = newModel.Timestamp;
                                if (!BaseService.Update(db, entity))
                                    result.Message = "系统未知原因";
                                else
                                {
                                    result.ResultStatus = ResultStatus.Success;
                                    result.Data = entity;
                                    result.Message = "角色信息修改成功";
                                }
                            }
                        }
                    }
                }
                return result;
            }
        }

        public CommandResult<Roles> Delete(int roleId)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Roles> { ResultStatus = ResultStatus.Error };
                if (!BaseService.Exist(db, a => a.Id == roleId))
                    result.Message = "该角色信息在系统中已不存在";
                else
                {
                    if (_leftMenuPermissionsService.Exist(db, a => a.RoleId == roleId && a.Have))
                        result.Message = "该角色下还有授权的菜单权限，请先取消授权";
                    else if (_staffRoleRelationshipsService.Exist(db, a => a.RoleId == roleId))
                        result.Message = "还有用户拥有此角色，请先取消授权";
                    else
                    {
                        using (var scope = new TransactionScope())
                        {
                            _leftMenuPermissionsService.Deletes(db, a => a.RoleId == roleId);
                            BaseService.Delete(db, BaseService.Find(db, a => a.Id == roleId));

                            scope.Complete();
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "角色信息删除成功";
                        }
                    }
                }
                return result;
            }
        }
    }
}
