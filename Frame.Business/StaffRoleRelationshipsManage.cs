using System.Collections.Generic;
using System.Linq;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;

namespace Frame.Business
{
    public class StaffRoleRelationshipsManage : BaseManage<StaffRoleRelationships, StaffRoleRelationshipsService>, IStaffRoleRelationshipsManage
    {
        public CommandResult<StaffRoleRelationships> ModifyRelationships(int staffId, int roleId, bool check)
        {
            var result = new CommandResult<StaffRoleRelationships>();
            using (var db = new FrameContext())
            {
                if (check)
                {
                    if (BaseService.Exist(db, a => a.StaffId == staffId && a.RoleId == roleId))
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "角色授权成功";
                        result.Data = BaseService.Find(db, a => a.StaffId == staffId && a.RoleId == roleId);
                    }
                    else
                    {
                        var entity = new StaffRoleRelationships
                        {
                            StaffId = staffId,
                            RoleId = roleId
                        };
                        if (!BaseService.Add(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "角色授权成功";
                            result.Data = entity;
                        }
                    }
                }
                else
                {
                    if (!BaseService.Exist(db, a => a.StaffId == staffId && a.RoleId == roleId))
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "授权取消成功";
                    }
                    else
                    {
                        var entity = BaseService.Find(db, a => a.StaffId == staffId && a.RoleId == roleId);
                        if (!BaseService.Delete(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "授权取消成功";
                            result.Data = entity;
                        }
                    }
                }
            }
            return result;
        }

        public List<StaffRoleRelationships> GetRelationships(int staffId)
        {
            List<StaffRoleRelationships> result;
            using (var db = new FrameContext())
            {
                result = BaseService.FindList(db, a => a.StaffId == staffId).Select(a => a).ToList();
            }
            return result;
        }
    }
}
