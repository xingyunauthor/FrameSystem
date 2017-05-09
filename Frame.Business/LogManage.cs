using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Log;

namespace Frame.Business
{
    public class LogManage : BaseManage<Log, LogService>, ILogManage
    {
        public List<LogAllResponseModel> LogManageSearch(string keywords)
        {
            using (var db = new FrameContext())
            {
                var rowId = 0;
                var list = BaseService.GetAll(db, true, a => a.Id);
                if (keywords != null && keywords.Trim().Length != 0)
                    list = list.Where(a => a.LoginName.StartsWith(keywords));
                return list.ToList().Select(a => new LogAllResponseModel
                {
                    RowId = ++rowId,
                    LogId = a.Id,
                    LoginName = a.LoginName,
                    LoginTime = a.LoginTime.ToString("yyyy-MM-dd"),
                    LoginRole = a.LoginRole,
                    LoginMach = a.LoginMach,
                    LoginCpu = a.LoginCpu
                }).ToList();
            }
        }


        public CommandResult<Log> Add(LogAddRequestModel requestModel)
        {
            var result = new CommandResult<Log>();
            using (var db = new FrameContext())
            {
                var entity = new Log
                {
                    LoginName = requestModel.LoginName,
                    LoginTime = requestModel.LoginTime,
                    LoginRole = requestModel.LoginRole,
                    LoginMach = requestModel.LoginMach,
                    LoginCpu = requestModel.LoginCpu
                };
                if (!BaseService.Add(db, entity))
                    result.Message = "未知原因";
                else
                {
                    result.ResultStatus = ResultStatus.Success;
                    result.Message = "登陆日志新增成功";
                    result.Data = entity;
                }
            }
            return result;
        }

        public void DeleteAll()
        {
            using (var db = new FrameContext())
            {
                var list = BaseService.GetAll(db, true, a => a.Id).ToList();
                using (var scope = new TransactionScope())
                {
                    list.ForEach(a =>
                    {
                        BaseService.Delete(db, a);
                    });

                    scope.Complete();
                }
            }
        }
    }
}
