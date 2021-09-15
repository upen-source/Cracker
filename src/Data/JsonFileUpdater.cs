using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class JsonFileUpdater : IFileUpdater
    {
        public async Task UpdateFileWith<TEntity>(UpdateContent<TEntity> updateContent,
            Action<List<TEntity>> updateMethod,
            CancellationToken cancellation)
        {
            (string fileName, IEnumerable<TEntity> entities) = updateContent;

            List<TEntity> convertedEntities = entities.ToList();
            updateMethod(convertedEntities);
            string serializedObject = JsonSerializer.Serialize(convertedEntities,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented               = true
                });
            await File.WriteAllTextAsync(fileName, serializedObject, cancellation);
        }
    }
}
