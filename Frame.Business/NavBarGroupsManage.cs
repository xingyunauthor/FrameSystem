using System.Collections.Generic;
using System.Linq;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.NavBarGroups;

namespace Frame.Business
{
    public class NavBarGroupsManage : BaseManage<NavBarGroups, NavBarGroupsService>, INavBarGroupsManage
    {
        private readonly LeftMenusService _leftMenusService = new LeftMenusService();
        public List<NavBarGroups> GetAll()
        {
            using (var db = new FrameContext())
            {
                return BaseService.GetAll(db, true, a => a.Sort).ToList();
            }
        }

        public NavBarGroups GetNavBarGroupById(int navBarGroupId)
        {
            using (var db = new FrameContext())
            {
                return BaseService.Find(db, a => a.Id == navBarGroupId);
            }
        }

        public CommandResult<NavBarGroups> Add(NavBarGroupsAddRequestModel requestModel)
        {
            var result = new CommandResult<NavBarGroups>();
            using (var db = new FrameContext())
            {
                if (Exist(a => a.Name == requestModel.NavBarGroupName))
                    result.Message = $"新增失败，功能组名称“{requestModel.NavBarGroupName}”在系统中已存在！";
                else
                {
                    var entity = new NavBarGroups
                    {
                        Name = requestModel.NavBarGroupName,
                        Ico = requestModel.IcoPath,
                        Sort = requestModel.Sort,
                        Timestamp = requestModel.Timestamp
                    };
                    if (!BaseService.Add(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "新增功能组菜单成功！";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<NavBarGroups> Update(int navBarGroupId, long timestamp, NavBarGroupsUpdateRequestModel requestModel)
        {
            var result = new CommandResult<NavBarGroups>();
            using (var db = new FrameContext())
            {
                if (!Exist(a => a.Id == navBarGroupId))
                    result.Message = "该功能菜单在系统中已不存在！";
                else if (!Exist(a => a.Id == navBarGroupId && a.Timestamp == timestamp))
                    result.Message = "该信息已在别处被修改，请刷新后再试！";
                else if (Exist(a => a.Id != navBarGroupId && a.Name == requestModel.NavBarGroupName))
                    result.Message = $"系统中已存在“{requestModel.NavBarGroupName}”名称！";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == navBarGroupId);
                    entity.Name = requestModel.NavBarGroupName;
                    entity.Ico = requestModel.IcoPath;
                    entity.Sort = requestModel.Sort;
                    entity.Timestamp = requestModel.Timestamp;

                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "功能菜单信息修改成功！";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<NavBarGroups> Delete(int navBarGroupId)
        {
            var result = new CommandResult<NavBarGroups>();
            if (!Exist(a => a.Id == navBarGroupId))
                result.Message = "该信息在系统中已不存在！";
            if (Exist(a => a.Id == navBarGroupId && a.Sys))
                result.Message = "该功能组是系统级的，不能删除";
            else
            {
                using (var db = new FrameContext())
                {
                    if (_leftMenusService.Exist(db, a => a.NavBarGroupId == navBarGroupId))
                        result.Message = "该功能组下还存在功能菜单，请先删除功能菜单后再试";
                    else
                    {
                        var entity = BaseService.Find(db, a => a.Id == navBarGroupId);
                        if (!BaseService.Delete(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "功能组信息删除成功！";
                            result.Data = entity;
                        }
                    }
                }
            }
            return result;
        }
    }
}