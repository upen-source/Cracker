using Data.Contracts;
using Data.Utils;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class PeopleJsonRepository : JsonRepository<Person>, IPeopleRepository
    {
        public PeopleJsonRepository(IConfiguration configuration, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper) : base(configuration["Persistence:People"], fileUpdater,
            fileMapper)
        {
        }
    }
}
