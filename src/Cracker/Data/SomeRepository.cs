using System.Collections.Generic;
using Cracker.Entity;
using Microsoft.Extensions.Configuration;

namespace Cracker.Data
{
    public class SomeRepository : IRepository<string, SomeEntity>
    {
        private readonly string _filePath;

        public SomeRepository(IConfiguration configuration)
        {
            _filePath = configuration["Persistence:FilePath"];
        }

        public SomeEntity Save(SomeEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SomeEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

#nullable enable
        public SomeEntity? GetById(string id)
#nullable disable
        {
            throw new System.NotImplementedException();
        }

        public void RemoveById(string id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateById(string id, SomeEntity newData)
        {
            throw new System.NotImplementedException();
        }
    }
}
