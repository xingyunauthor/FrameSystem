
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenus;

namespace Frame.Business.interfaces
{
    public interface ILeftMenusManage : IBaseManage<LeftMenus>
    {
        /// <summary>
        /// 根据菜单编号得到一个实体
        /// </summary>
        /// <param name="leftMenuId"></param>
        /// <returns></returns>
        LeftMenus GetLeftMenuById(int leftMenuId);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        List<LeftMenus> GetAll();

        /// <summary>
        /// 根据角色查询出列表
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        List<LeftMenus> GetLeftMenusesByRoleId(int roleId);

        /// <summary>
        /// 获取随系统启动的菜单
        /// </summary>
        /// <returns></returns>
        List<LeftMenus> GetStartWithSysLeftMenuses();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="leftMenuId">菜单编号</param>
        /// <param name="timestamp">原始时间戳</param>
        /// <param name="requestModel">更新部分的信息</param>
        /// <returns></returns>
        CommandResult<LeftMenus> Update(int leftMenuId, long timestamp, LeftMenusUpdateRequestModel requestModel);

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="leftMenuId">菜单编号</param>
        /// <param name="timestamp">原始时间戳</param>
        /// <param name="requestModel">更新部分的信息</param>
        /// <returns></returns>
        CommandResult<LeftMenus> UpdateSort(int leftMenuId, long timestamp, LeftMenusUpdateSortRequestModel requestModel);
        
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<LeftMenus> Add(LeftMenusAddRequestModel requestModel);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        CommandResult<List<LeftMenus>> Deletes(Expression<Func<LeftMenus, bool>> whereLambda);
    }
}
