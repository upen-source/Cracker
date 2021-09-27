using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Utils;

namespace Data
{
    public class JsonRepository<TEntity> : IRepository<TEntity>
    {
        private readonly string             _filePath;
        private readonly IFileUpdater       _fileUpdater;
        private readonly IFileContentMapper _fileMapper;

        protected JsonRepository(string filePath, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper)
        {
            _filePath    = filePath;
            _fileUpdater = fileUpdater;
            _fileMapper  = fileMapper;
        }

        public async Task Add(TEntity entity, CancellationToken cancellation)
        {
            List<TEntity> collectionUpdated = (await GetAll(cancellation)).ToList();
            collectionUpdated.Add(entity);
            await SaveAll(collectionUpdated, cancellation);
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellation)
        {
            return await _fileMapper.MapFileContent<TEntity>(_filePath);
        }

        public async Task RemoveWhere(Predicate<TEntity> predicate, CancellationToken cancellation)
        {
            IEnumerable<TEntity> removedEntityCollection = (await GetAll(cancellation))
                .Where(entity => !predicate(entity));
            await SaveAll(removedEntityCollection, cancellation);
        }

        public async Task<TEntity?> GetWhere(Func<TEntity, bool> predicate,
            CancellationToken cancellation)
        {
            return (await GetAll(cancellation)).FirstOrDefault(predicate);
        }

        private async Task SaveAll(IEnumerable<TEntity> entities, CancellationToken cancellation)
        {
            var updateContent = new UpdateContent<TEntity>(_filePath, entities);
            await _fileUpdater.UpdateFileWith(updateContent, cancellation);
        }
    }
}
