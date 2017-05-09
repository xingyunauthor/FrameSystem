using System.Collections.Generic;
using System.Data;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Permissions;

namespace Frame.Business.interfaces
{
    public interface IPermissionsManage : IBaseManage<Permissions>
    {
        /// <summary>
        /// 获取全部信息
        /// </summary>
        /// <returns></returns>
        List<Permissions> GetAll();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        CommandResult<Permissions> Add(PermissionsAddRequestModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="permissionsId"></param>
        /// <param name="updateRequestModel"></param>
        /// <returns></returns>
        CommandResult<Permissions>  Update(int permissionsId, PermissionsUpdateRequestModel updateRequestModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="permissionsId"></param>
        /// <returns></returns>
        CommandResult<Permissions> Delete(int permissionsId);

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        CommandResult<Permissions> UpdateSort(List<PermissionsUpdateSortRequestModel> list);
    }
}