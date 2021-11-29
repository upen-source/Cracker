using System.Collections.Generic;

namespace Data.Contracts
{
    public interface IRepository<TEntity>
    {
        public void Add(TEntity entity);
        public IList<TEntity> GetAll();
    }
}
