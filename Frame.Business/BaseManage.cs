using System;
using System.Linq.Expressions;
using Frame.MetaData;
using Frame.Models.SysModels.Common;

namespace Frame.Business
{
    public class BaseManage<T, TS> : IBaseManage<T> 
        where T : class
        where TS : IBaseService<T>, new()
    {
        protected IBaseService<T> BaseService { get; } = new TS();

        public DateTime ServerTime => BaseService.ServerTime;

        public bool Exist(Expression<Func<T, bool>> whereLambda)
        {
            using (var db = new FrameContext())
            {
                return BaseService.Exist(db, whereLambda);
            }
        }

        public GetConnectionResponseModel GetConnection()
        {
            var responseModel = new GetConnectionResponseModel();
            using (var db = new FrameContext())
            {
                var conn = db.Database.Connection;
                responseModel.ConnectionString = conn.ConnectionString;
                responseModel.DataSource = conn.DataSource;
                responseModel.Database = conn.Database;
            }
            return responseModel;
        }
    }
}
