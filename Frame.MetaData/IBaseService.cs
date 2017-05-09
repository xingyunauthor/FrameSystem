using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Frame.MetaData
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 服务器时间
        /// </summary>
        DateTime ServerTime { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        bool Add(DbContext db, T entity);

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns>记录数</returns>
        int Count(DbContext db, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Update(DbContext db, T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Delete(DbContext db, T entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="whereLambda">查询表达式</param>
        void Deletes(DbContext db, Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="anyLambda">查询表达式</param>
        /// <returns>布尔值</returns>
        bool Exist(DbContext db, Expression<Func<T, bool>> anyLambda);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns>实体</returns>
        T Find(DbContext db, Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="whereLamdba"></param>
        /// <returns></returns>
        IQueryable<T> FindList(DbContext db, Expression<Func<T, bool>> whereLamdba);

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="db">数据库上下文</param>
        /// <param name="whereLamdba">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList<TS>(DbContext db, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, TS>> orderLamdba);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="db">数据库上下文</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> GetAll<TS>(DbContext db, bool isAsc, Expression<Func<T, TS>> orderLamdba);
    }
}
