using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class SomeJsonRepository : ISomeEntityRepository
    {
        private readonly string       _filePath;
        private readonly IFileUpdater _fileUpdater;

        public SomeJsonRepository(IConfiguration configuration, IFileUpdater fileUpdater)
        {
            _fileUpdater = fileUpdater;
            _filePath    = configuration["Persistence:FilePath"];
        }

        public async Task Save(SomeEntity entity,
            CancellationToken cancellation)
        {
            IEnumerable<SomeEntity> entities = await GetAll(cancellation);
            var updateContent = new UpdateContent<SomeEntity>(_filePath, entities);
            await _fileUpdater.UpdateFileWith(updateContent, e => e.Add(entity),
                cancellation);
        }

        public async Task<IEnumerable<SomeEntity>> GetAll(CancellationToken cancellation)
        {
            if (!File.Exists(_filePath)) return new List<SomeEntity>();

            using StreamReader fileReader = File.OpenText(_filePath);
            string             content    = await fileReader.ReadToEndAsync();
            return (string.IsNullOrEmpty(content)
                ? new List<SomeEntity>()
                : JsonSerializer.Deserialize<List<SomeEntity>>(content))!;
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
