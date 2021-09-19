using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRepository<in TId, TEntity> : IBaseRepository<TEntity>
    {
        public Task<TEntity?> GetById(TId id, CancellationToken cancellation);
        public Task RemoveById(TId id, CancellationToken cancellation);
    }
}
