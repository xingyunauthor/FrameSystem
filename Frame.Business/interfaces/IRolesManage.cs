using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Roles;

namespace Frame.Business.interfaces
{
    public interface IRolesManage : IBaseManage<Roles>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Roles> GetAll();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        CommandResult<Roles> Add(string roleName, long timestamp);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">原数据</param>
        /// <param name="newModel">新数据</param>
        /// <returns></returns>
        CommandResult<Roles> Update(RoleEditRequestModel model, RoleEditRequestNewModel newModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        CommandResult<Roles> Delete(int roleId);
    }
}
