using System.Collections.Generic;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Log;

namespace Frame.Business.interfaces
{
    public interface ILogManage : IBaseManage<Log>
    {
        /// <summary>
        /// 日志管理列表查询
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        List<LogAllResponseModel> LogManageSearch(string keywords);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        CommandResult<Log> Add(LogAddRequestModel requestModel);

        /// <summary>
        /// 删除全部
        /// </summary>
        void DeleteAll();
    }
}
