using System.Collections.Generic;

namespace Cracker.Data
{
    public interface IRepository<in TId, TEntity>
    {
        public TEntity Save(TEntity entity);
        public IEnumerable<TEntity> GetAll();
#nullable enable
        public TEntity? GetById(TId id);
#nullable disable
        public void RemoveById(TId id);
        public void UpdateById(TId id, TEntity newData);
    }}
