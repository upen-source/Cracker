using Data.Contracts;
using Data.Utils;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class SomeEntitiesJsonRepository : JsonRepository<SomeEntity>, ISomeEntityRepository
    {
        public SomeEntitiesJsonRepository(IConfiguration configuration, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper) : base(configuration["Persistence:Entities"],
            fileUpdater, fileMapper)
        {
        }
    }
}
