using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Frame.MetaData
{
    /// <summary>
    /// 仓储基类
    /// <remarks></remarks>
    /// </summary>
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public DateTime ServerTime
        {
            get
            {
                using (var db = new FrameContext())
                {
                    return db.Database.SqlQuery<DateTime>("select now()").Single();
                }
            }
        }

        public virtual bool Add(DbContext db, T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges() > 0;
        }

        public virtual int Count(DbContext db, Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Count(predicate);
        }

        public virtual bool Update(DbContext db, T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public virtual bool Delete(DbContext db, T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public virtual void Deletes(DbContext db, Expression<Func<T, bool>> whereLambda)
        {
            var list = db.Set<T>().Where(whereLambda).ToList();
            list.ForEach(entity =>
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            });
        }

        public virtual bool Exist(DbContext db, Expression<Func<T, bool>> anyLambda)
        {
            return db.Set<T>().Any(anyLambda);
        }

        public virtual T Find(DbContext db, Expression<Func<T, bool>> whereLambda)
        {
            T entity = db.Set<T>().FirstOrDefault(whereLambda);
            return entity;
        }

        public virtual IQueryable<T> FindList(DbContext db, Expression<Func<T, bool>> whereLamdba)
        {
            return db.Set<T>().Where(whereLamdba);
        }

        public virtual IQueryable<T> FindList<TS>(DbContext db, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, TS>> orderLamdba)
        {
            var list = db.Set<T>().Where(whereLamdba);
            list = isAsc ? list.OrderBy(orderLamdba) : list.OrderByDescending(orderLamdba);
            return list;
        }

        public virtual IQueryable<T> GetAll<TS>(DbContext db, bool isAsc, Expression<Func<T, TS>> orderLamdba)
        {
            var list = db.Set<T>().Select(a => a);
            list = isAsc ? list.OrderBy(orderLamdba) : list.OrderByDescending(orderLamdba);
            return list;
        }
    }
}
