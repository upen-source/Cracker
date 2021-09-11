using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cracker.Data
{
    public interface IAsyncRepository<in TId, TEntity>
    {
        public Task<TEntity> Save(TEntity entity, CancellationToken cancellation);
        public Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellation);
#nullable enable
        public Task<TEntity?> GetById(TId id, CancellationToken cancellation);
#nullable disable
        public Task RemoveById(TId id, CancellationToken cancellation);
        public Task UpdateById(TId id, TEntity newData, CancellationToken cancellation);
    }}
