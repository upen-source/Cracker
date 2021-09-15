using Entities;

namespace Data
{
    public interface ISomeEntityRepository : IAsyncRepository<string, SomeEntity>
    {
    }
}
