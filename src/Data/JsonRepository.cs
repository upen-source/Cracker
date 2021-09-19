using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Utils;

namespace Data
{
    public class JsonRepository<TEntity> : IBaseRepository<TEntity>
    {
        private readonly string             _filePath;
        private readonly IFileUpdater       _fileUpdater;
        private readonly IFileContentMapper _fileMapper;

        protected JsonRepository(string filePath, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper)
        {
            _fileUpdater = fileUpdater;
            _fileMapper  = fileMapper;
            _filePath    = filePath;
        }

        public async Task Add(TEntity entity, CancellationToken cancellation)
        {
            List<TEntity> paymentsUpdated = (await GetAll(cancellation)).ToList();
            paymentsUpdated.Add(entity);
            var updateContent = new UpdateContent<TEntity>(_filePath, paymentsUpdated);
            await _fileUpdater.UpdateFileWith(updateContent, cancellation);
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellation)
        {
            return await _fileMapper.MapFileContent<TEntity>(_filePath);
        }

        protected async Task SaveAll(IEnumerable<TEntity> entities,
            CancellationToken cancellation)
        {
            var updateContent = new UpdateContent<TEntity>(_filePath, entities);
            await _fileUpdater.UpdateFileWith(updateContent, cancellation);
        }
    }
}
