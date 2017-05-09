using System.Collections.ObjectModel;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Operator;
using Frame.Models.SysModels.Staff;

namespace Frame.Business.interfaces
{
    public interface IOperatorManage : IBaseManage<Staff>
    {
        /// <summary>
        /// 获取所有管理员列表
        /// </summary>
        /// <returns></returns>
        ObservableCollection<OperatorAllResponseModel> All(string keywords);

        /// <summary>
        /// 获取一个用于编辑操作员信息的实体
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        CommandResult<Staff> GetModel(int staffId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="paramsModel"></param>
        /// <returns></returns>
        CommandResult<Staff> Add(OperatorAddResponseModel paramsModel);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="paramsModel"></param>
        /// <returns></returns>
        CommandResult<Staff> Edit(int staffId, OperatorEditResponseModel paramsModel);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="oldLogonPwd"></param>
        /// <param name="newLogonPwd"></param>
        /// <returns></returns>
        CommandResult<Staff> EditPwd(int staffId, string oldLogonPwd, string newLogonPwd);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        CommandResult<Staff> Delete(int staffId);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="paramsModel"></param>
        /// <returns></returns>
        CommandResult<Staff> Login(OperatorLogonRequestModel paramsModel);
    }
}