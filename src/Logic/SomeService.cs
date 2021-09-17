using System;
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
            if (await _repository.GetById(entity.Id, cancellation) != null)
            {
                throw new InvalidOperationException("");
            }

            await _repository.Save(entity, cancellation);
        }

        public async Task<SomeEntity> GetById(string id, CancellationToken cancellation)
        {
            SomeEntity entity = await _repository.GetById(id, cancellation);
            if (entity == null) throw new InvalidOperationException("");
            return entity;
        }

        public async Task RemoveById(string id, CancellationToken cancellation)
        {
            if (await _repository.GetById(id, cancellation) == null)
            {
                throw new InvalidOperationException("");
            }

            await _repository.RemoveById(id, cancellation);
        }

        public async Task UpdateById(string id, SomeEntity entity, CancellationToken cancellation)
        {
            if (await _repository.GetById(id, cancellation) == null)
            {
                throw new InvalidOperationException("");
            }

            await _repository.UpdateById(id, entity, cancellation);
        }
    }
}
