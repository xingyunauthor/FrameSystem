using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Permissions;

namespace Frame.Business
{
    public class PermissionsManage : BaseManage<Permissions, PermissionsService>, IPermissionsManage
    {
        private readonly LeftMenuPermissionsService _leftMenuPermissionsService = new LeftMenuPermissionsService();

        public List<Permissions> GetAll()
        {
            using (var db = new FrameContext())
            {
                return BaseService.GetAll(db, true, a => a.Sort).ToList();
            }
        }

        public CommandResult<Permissions> Add(PermissionsAddRequestModel model)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Permissions>();
                if (BaseService.Exist(db, a => a.PermissionsName == model.PermissionsName))
                    result.Message = "权限名称在系统中已存在";
                else
                {
                    var entity = new Permissions { PermissionsName = model.PermissionsName, Sort = model.Sort };
                    if (!BaseService.Add(db, entity))
                        result.Message = "系统未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "权限信息添加成功";
                    }
                }
                return result;
            }
        }

        public CommandResult<Permissions> Update(int permissionsId, PermissionsUpdateRequestModel updateRequestModel)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Permissions>();
                if (!BaseService.Exist(db, a => a.Id == permissionsId))
                    result.Message = "该权限信息在系统中已不存在";
                else if (BaseService.Exist(db, a => a.Id == permissionsId && a.Sys))
                    result.Message = "该权限是系统级的，不能被修改";
                else if (BaseService.Exist(db, a => a.PermissionsName == updateRequestModel.PermissionsName && a.Id != permissionsId))
                    result.Message = "该权限名称在系统中已存在";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == permissionsId);
                    entity.PermissionsName = updateRequestModel.PermissionsName;
                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "权限信息修改成功";
                        result.Data = entity;
                    }
                }
                return result;
            }
        }

        public CommandResult<Permissions> Delete(int permissionsId)
        {
            using (var db = new FrameContext())
            {
                var result = new CommandResult<Permissions>();
                if (!BaseService.Exist(db, a => a.Id == permissionsId))
                    result.Message = "该权限信息在系统中已不存在";
                else if (BaseService.Exist(db, a => a.Id == permissionsId && a.Sys))
                    result.Message = "该权限是系统级的，不能被删除";
                else if (_leftMenuPermissionsService.Exist(db, a => a.PermissionsId == permissionsId && a.Have))
                {
                    var role = _leftMenuPermissionsService.FindList(db, a => a.PermissionsId == permissionsId && a.Have)
                        .Select(a => a.RolesForeign).First();
                    result.Message = $"该权限在角色“{role.RoleName}”中还仍被使用";
                }
                else
                {
                    using (var scope = new TransactionScope())
                    {
                        _leftMenuPermissionsService.Deletes(db, a => a.PermissionsId == permissionsId);
                        BaseService.Delete(db, BaseService.Find(db, a => a.Id == permissionsId));

                        scope.Complete();
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "权限信息删除成功";
                    }
                }
                return result;
            }
        }

        public CommandResult<Permissions> UpdateSort(List<PermissionsUpdateSortRequestModel> list)
        {
            using (var db = new FrameContext())
            {
                var updateStatus = new List<bool>();
                var result = new CommandResult<Permissions>();
                var ids = list.Select(a => a.PermissionsId).ToArray();
                var entityList = BaseService.FindList(db, a => ids.Any(z => z == a.Id));
                if (list.Count != entityList.Count())
                    result.Message = "部分信息与系统中的不一致，请刷新后再试";
                else
                {
                    using (var scope = new TransactionScope())
                    {
                        entityList.ToList().ForEach(entity =>
                        {
                            entity.Sort = list.Single(z => z.PermissionsId == entity.Id).Sort;
                            updateStatus.Add(BaseService.Update(db, entity));
                        });

                        scope.Complete();
                    }

                    if (updateStatus.All(a => !a))
                        result.Message = "系统没有更新任何数据";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "排序保存成功";
                    }
                }

                return result;
            }
        }
    }
}
