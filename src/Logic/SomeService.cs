using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Logic
{
    public class SomeService
    {
        private readonly ISomeEntityRepository _repository;

        public SomeService(ISomeEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SomeEntity>> GetAll(CancellationToken cancellation)
        {
            return await _repository.GetAll(cancellation);
        }

        public async Task Save(SomeEntity entity, CancellationToken cancellation)
        {
            await _repository.Save(entity, cancellation);
        }
    }
}
