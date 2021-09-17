﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class SomeJsonRepository : ISomeEntityRepository
    {
        private readonly string             _filePath;
        private readonly IFileUpdater       _fileUpdater;
        private readonly IFileContentMapper _fileMapper;

        public SomeJsonRepository(IConfiguration configuration, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper)
        {
            _fileUpdater = fileUpdater;
            _fileMapper  = fileMapper;
            _filePath    = configuration["Persistence:FilePath"];
        }

        public async Task Save(SomeEntity entity,
            CancellationToken cancellation)
        {
            IEnumerable<SomeEntity> entities = await GetAll(cancellation);
            var updateContent = new UpdateContent<SomeEntity>(_filePath, entities, entity);
            await _fileUpdater.UpdateFileWith(updateContent,
                (someEntities, someEntity) => someEntities.Add(someEntity),
                cancellation);
        }

        public async Task<IEnumerable<SomeEntity>> GetAll(CancellationToken cancellation)
        {
            return await _fileMapper.MapFileContent<SomeEntity>(_filePath);
        }

        public async Task<SomeEntity?> GetById(string id, CancellationToken cancellation)
        {
            return (await GetAll(cancellation)).FirstOrDefault(e => e.Id == id);
        }

        public Task RemoveById(string id, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateById(string id, SomeEntity newData,
            CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }
    }
}