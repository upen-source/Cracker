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
        private readonly IPeopleRepository     _peopleRepository;

        public SomeService(ISomeEntityRepository repository, IPeopleRepository peopleRepository)
        {
            _repository       = repository;
            _peopleRepository = peopleRepository;
        }

        public async Task<IEnumerable<SomeEntity>> GetAll(CancellationToken cancellation)
        {
            return await _repository.GetAll(cancellation);
        }

        [RequiredArguments(ErrorMessage = "Los datos ingresados son inválidos")]
        public async Task Add(SomeEntity entity, CancellationToken cancellation)
        {
            if (await _repository.GetWhere(someEntity => someEntity.Id == entity.Id,
                cancellation) != null)
            {
                throw new InvalidOperationException("Id de la entidad ya se encuentra registrada");
            }

            await _repository.Add(entity, cancellation);
            await _peopleRepository.Add(new Person(entity.Name), cancellation);
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
