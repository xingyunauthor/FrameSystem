using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Dept;

namespace Frame.Business.interfaces
{
    public interface IDeptManage : IBaseManage<Dept>
    {
        /// <summary>
        /// 全部数据
        /// </summary>
        /// <returns></returns>
        ObservableCollection<DeptAllResponseModel> All();

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        CommandResult<Dept> GetModel(int deptId);

        /// <summary>
        /// 部门新增
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<Dept> Add(DeptAddRequestModel requestModel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<Dept> Update(int id, DeptUpdateRequestModel requestModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <returns></returns>
        CommandResult<Dept> Delete(int id);
    }
}
