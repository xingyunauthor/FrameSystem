using System.Collections.Generic;
using Frame.Models;
using Frame.Models.SysModels;

namespace Frame.Business.interfaces
{
    public interface IStaffRoleRelationshipsManage : IBaseManage<StaffRoleRelationships>
    {
        /// <summary>
        /// 更改员工角色授权
        /// </summary>
        /// <param name="staffId">员工编号</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="check">是否授权</param>
        /// <returns></returns>
        CommandResult<StaffRoleRelationships> ModifyRelationships(int staffId, int roleId, bool check);

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="staffId">员工编号</param>
        /// <returns></returns>
        List<StaffRoleRelationships> GetRelationships(int staffId);
    }
}