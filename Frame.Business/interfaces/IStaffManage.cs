using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Staff;

namespace Frame.Business.interfaces
{
    public interface IStaffManage : IBaseManage<Staff>
    {
        /// <summary>
        /// 全部信息
        /// </summary>
        /// <returns></returns>
        ObservableCollection<StaffAllResponseModel> All();

        /// <summary>
        /// 获取一个新的 Code
        /// </summary>
        /// <returns></returns>
        string GetNewCode();

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="staffs"></param>
        /// <param name="keywords"></param>
        void StaffSearch(ObservableCollection<StaffAllResponseModel> staffs, string keywords);

        /// <summary>
        /// 员工参照列表搜索
        /// </summary>
        /// <param name="staffs"></param>
        /// <param name="keywords"></param>
        void StaffReferSearch(ObservableCollection<StaffAllResponseModel> staffs, string keywords);

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        CommandResult<Staff> GetModel(int staffId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<Staff> Add(StaffAddRequestModel requestModel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="staffId">员工编号</param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<Staff> Update(int staffId, StaffUpdateRequestModel requestModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="staffId">员工编号</param>
        /// <returns></returns>
        CommandResult<Staff> Delete(int staffId);
    }
}
