
using System.Collections.Generic;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.NavBarGroups;

namespace Frame.Business.interfaces
{
    public interface INavBarGroupsManage : IBaseManage<NavBarGroups>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<NavBarGroups> GetAll();

        /// <summary>
        /// 根据主键获得一个实体
        /// </summary>
        /// <param name="navBarGroupId"></param>
        /// <returns></returns>
        NavBarGroups GetNavBarGroupById(int navBarGroupId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<NavBarGroups> Add(NavBarGroupsAddRequestModel requestModel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="navBarGroupId">主键</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<NavBarGroups> Update(int navBarGroupId, long timestamp, NavBarGroupsUpdateRequestModel requestModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="navBarGroupId">主键</param>
        /// <returns></returns>
        CommandResult<NavBarGroups> Delete(int navBarGroupId);
    }
}
