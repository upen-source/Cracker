using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;
using Logic.Filters;

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

        [RequiredArguments(ErrorMessage = "Los datos ingresados son inválidos")]
        public async Task Add(SomeEntity entity, CancellationToken cancellation)
        {
            if (await IsEntityIdRepeated(entity, cancellation))
            {
                throw new InvalidOperationException("Id de la entidad ya se encuentra registrada");
            }

            await _repository.Add(entity, cancellation);
        }

        private async Task<bool> IsEntityIdRepeated(SomeEntity entity, CancellationToken cancellation)
        {
            bool IdMatches(SomeEntity someEntity) => someEntity.Id == entity.Id;
            return await _repository.GetWhere(IdMatches, cancellation) != null;
        }

        [RequiredReturn(typeof(SomeEntity), ErrorMessage = "Entidad no encontrada")]
        public async Task<SomeEntity> GetById([NonEmptyString("Id inválido")] string id,
            CancellationToken cancellation)
        {
            return await _repository.GetWhere(entity => entity.Id == id, cancellation);
        }

        public async Task RemoveById(string id, CancellationToken cancellation)
        {
            await GetById(id, cancellation);
            await _repository.RemoveWhere(entity => entity.Id == id, cancellation);
        }

        public async Task UpdateById(string id, SomeEntity entity, CancellationToken cancellation)
        {
            await RemoveById(id, cancellation);
            await Add(entity, cancellation);
        }
    }
}
