using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Utils;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class SomeJsonRepository : JsonRepository<SomeEntity>, ISomeEntityRepository
    {
        public SomeJsonRepository(IConfiguration configuration, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper) : base(configuration["Persistence:Entities"],
            fileUpdater, fileMapper)
        {
        }

        public async Task<SomeEntity?> GetById(string id, CancellationToken cancellation)
        {
            return (await GetAll(cancellation)).FirstOrDefault(e => e.Id == id);
        }

        public async Task RemoveById(string id, CancellationToken cancellation)
        {
            IEnumerable<SomeEntity> removedEntityCollection =
                (await GetAll(cancellation)).Where(e => e.Id != id);
            await SaveAll(removedEntityCollection, cancellation);
        }
    }
}
