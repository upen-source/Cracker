using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;
using Logic.Guards;

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

        [RequiredArguments]
        public async Task Add(SomeEntity entity, CancellationToken cancellation)
        {
            if (await _repository.GetById(entity.Id, cancellation) != null)
            {
                throw new InvalidOperationException("");
            }

            await _repository.Add(entity, cancellation);
        }

        [RequiredReturn]
        public async Task<SomeEntity> GetById([NonEmptyString] string id, CancellationToken cancellation)
        {
            return await _repository.GetById(id, cancellation);
        }

        public async Task RemoveById([NonEmptyString] string id, CancellationToken cancellation)
        {
            if (await _repository.GetById(id, cancellation) == null)
            {
                throw new InvalidOperationException("");
            }

            await _repository.RemoveById(id, cancellation);
        }

        public async Task UpdateById(string id, SomeEntity entity, CancellationToken cancellation)
        {
            await RemoveById(id, cancellation);
            await Add(entity, cancellation);
        }
    }
}
