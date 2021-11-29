using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Entities;

namespace Data
{
    public class PeopleRepository : Repository<Person>
    {
        public PeopleRepository(DbConnection connection) : base(connection)
        {
        }

        public override void Add(Person entity)
        {
            Insert("INTO People (Id, Name) VALUES (@0, @1)", new[] { entity.Id, entity.Name });
        }

        public override IList<Person> GetAll()
        {
            return Select("Id, Name FROM People");
        }

        public Person FindById(string id)
        {
            return Select("Id FROM People WHERE Id = @0", new[] { id }, MapWithoutName).First();
        }

        public void UpdateById(string id, string name)
        {
            Update("People SET Name = @0 WHERE Id = @1", new[] { name, id });
        }

        private static Person MapWithoutName(IDataRecord record)
        {
            return new Person
            {
                Id   = record.GetString(0),
            };
        }

        protected override Person DefaultMap(IDataRecord record)
        {
            return new Person
            {
                Id   = record.GetString(0),
                Name = record.GetString(1)
            };
        }
    }
}
