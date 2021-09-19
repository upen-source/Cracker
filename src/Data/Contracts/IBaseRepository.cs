using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IBaseRepository<TEntity>
    {
        public Task Add(TEntity entity, CancellationToken cancellation);
        public Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellation);
    }
}
