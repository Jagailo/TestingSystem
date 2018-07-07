using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : RepositoryBaseWithoutKey<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
    {
        protected RepositoryBase(ApplicationDbContext context)
            : base(context)
        {
        }

        public virtual TEntity GetById(TKey id)
        {
            return this.Dbset.Find(id);
        }
    }
}
