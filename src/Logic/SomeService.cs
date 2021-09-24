using Data.Contracts;

namespace Logic
{
    public class SomeService
    {
        private readonly ISomeEntityRepository _repository;

        public SomeService(ISomeEntityRepository repository)
        {
            _repository = repository;
        }
    }
}
