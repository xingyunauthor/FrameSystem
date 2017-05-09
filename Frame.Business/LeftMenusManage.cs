using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenus;

namespace Frame.Business
{
    public class LeftMenusManage : BaseManage<LeftMenus, LeftMenusService>, ILeftMenusManage
    {
        private readonly LeftMenuPermissionsService _leftMenuPermissionsService = new LeftMenuPermissionsService();
        public LeftMenus GetLeftMenuById(int leftMenuId)
        {
            using (var db = new FrameContext())
            {
                return BaseService.Find(db, a => a.Id == leftMenuId);
            }
        }

        public List<LeftMenus> GetAll()
        {
            using (var db = new FrameContext())
            {
                return BaseService.GetAll(db, true, a => a.Sort).ToList();
            }
        }

        public List<LeftMenus> GetLeftMenusesByRoleId(int roleId)
        {
            using (var db = new FrameContext())
            {
                var queryable = _leftMenuPermissionsService.GetAll(db, true, a => a.Id)
                    .Where(a => a.RoleId == roleId && a.Have && a.PermissionsId == 6)
                    .Select(a => a.LeftMenusForeign)
                    .OrderBy(a => a.Sort);
                var list = queryable.ToList();
                return list;
            }
        }

        public List<LeftMenus> GetStartWithSysLeftMenuses()
        {
            using (var db = new FrameContext())
            {
                return BaseService.FindList(db, a => a.StartWithSys).OrderBy(a => a.Sort).ToList();
            }
        }

        public CommandResult<LeftMenus> Update(int leftMenuId, long timestamp, LeftMenusUpdateRequestModel requestModel)
        {
            var result = new CommandResult<LeftMenus>();
            using (var db = new FrameContext())
            {
                if (!BaseService.Exist(db, a => a.Id == leftMenuId))
                    result.Message = "该数据在系统中已不存在，请检查后再试……";
                else if (!BaseService.Exist(db, a => a.Id == leftMenuId && a.Timestamp == timestamp))
                    result.Message = "该节点已在别处被更改过！";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == leftMenuId);
                    entity.DisplayName = requestModel.DisplayName;
                    entity.DllPath = requestModel.DllPath;
                    entity.EntryFunction = requestModel.EntryFunction;
                    entity.Ico = requestModel.Ico;
                    entity.MenuId = requestModel.MenuId;
                    entity.NavBarGroupId = requestModel.NavBarGroupId;
                    entity.ParentId = requestModel.ParentId;
                    entity.StartWithSys = requestModel.StartWithSys;
                    entity.Timestamp = requestModel.Timestamp;

                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "更新数据成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<LeftMenus> UpdateSort(int leftMenuId, long timestamp, LeftMenusUpdateSortRequestModel requestModel)
        {
            var result = new CommandResult<LeftMenus>();
            if (!Exist(a => a.Id == leftMenuId))
                result.Message = $"节点名称“{requestModel.DisplayName}”在系统中已不存在";
            else if (!Exist(a => a.Id == leftMenuId && a.Timestamp == timestamp))
                result.Message = $"节点名称为“{requestModel.DisplayName}”的节点排序已在别处被修改";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == leftMenuId);
                    entity.Sort = requestModel.Sort;
                    entity.Timestamp = requestModel.Timestamp;
                    if (!BaseService.Update(db, entity))
                        result.Message = "系统出现未知原因，节点排序更新失败！";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "节点排序更新成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<LeftMenus> Add(LeftMenusAddRequestModel requestModel)
        {
            var result = new CommandResult<LeftMenus>();
            using (var db = new FrameContext())
            {
                if (requestModel.MenuId != null && requestModel.MenuId.Trim().Length != 0 && BaseService.Exist(db, a => a.MenuId == requestModel.MenuId))
                    result.Message = $"菜单唯一标识“{requestModel.MenuId}”,在系统中已存在，请重新输入……";
                else
                {
                    var entity = new LeftMenus
                    {
                        DisplayName = requestModel.DisplayName,
                        DllPath = requestModel.DllPath,
                        EntryFunction = requestModel.EntryFunction,
                        MenuId = requestModel.MenuId,
                        NavBarGroupId = requestModel.NavBarGroupId,
                        ParentId = requestModel.ParentId,
                        Sort = requestModel.Sort,
                        StartWithSys = requestModel.StartWithSys,
                        Timestamp = requestModel.Timestamp
                    };
                    if (!BaseService.Add(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "新的节点信息添加成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 删除该节点以及该节点一下的所有数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public CommandResult<List<LeftMenus>> Deletes(Expression<Func<LeftMenus, bool>> whereLambda)
        {
            var result = new CommandResult<List<LeftMenus>>();
            using (var db = new FrameContext())
            {
                var list = GetAll();
                var entity = BaseService.Find(db, whereLambda);
                if (entity.Sys)
                    result.Message = $"菜单“{entity.DisplayName}”为系统级菜单，不允许删除";
                else if (list.Any(a => a.Sys))
                    result.Message = $"菜单“{entity.DisplayName}”子目录下包含系统级菜单，请重新选择";
                else
                {
                    var deleteList = new List<LeftMenus>();
                    CommonManage.GetAllChildrenItem(list, entity.Id, deleteList);

                    using (var scope = new TransactionScope())
                    {
                        deleteList.ForEach(a =>
                        {
                            BaseService.Delete(db, a);
                        });
                        BaseService.Delete(db, entity);

                        scope.Complete();
                    }

                    result.ResultStatus = ResultStatus.Success;
                    result.Data.Add(entity);
                    deleteList.ForEach(result.Data.Add);
                }
            }
            return result;
        }
    }
}
