using System;
using System.Linq.Expressions;
using Frame.Models.SysModels.Common;

namespace Frame.Business
{
    public interface IBaseManage<T> where T : class
    {
        /// <summary>
        /// 服务器时间
        /// </summary>
        DateTime ServerTime { get; }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        GetConnectionResponseModel GetConnection();
    }
}
