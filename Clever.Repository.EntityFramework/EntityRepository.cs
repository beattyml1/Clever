using System.Data.Entity;

namespace Clever.Repository.EntityFramework
{
    public abstract class EntityRepository<TEntity, TId, TQuery, TPutReturn, TDeleteReturn> 
        : QueryablRepository<TEntity, TId, TQuery>
        , IRepository<TId, TEntity, TEntity, TQuery, TEntity, TEntity, TEntity, bool, bool>
        where TEntity: class, new()
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly DbContext DbContext;
        public EntityRepository(DbContext context, DbSet<TEntity> dbset): base(dbset)
        {
            DbSet = dbset;
        }

        public TEntity Post(TEntity data)
        {
            var newData = DbSet.Add(data);
            DbContext.SaveChanges();
            return newData;
        }

        public bool Put(TId id, TEntity data)
        {
            DbSet.Attach(data);
            var entry = DbContext.Entry(data);
            entry.State = EntityState.Modified;
            DbContext.SaveChanges();
            return true;
        }

        public bool Delete(TId id)
        {
            var data = DbSet.Find(id);
            var entry = DbContext.Entry(data);
            entry.State = EntityState.Deleted;
            DbContext.SaveChanges();
            return true;
        }
    }
}
