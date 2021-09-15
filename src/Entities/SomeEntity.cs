namespace Entities
{
    public class SomeEntity
    {
        public string Id   { get; set; }
        public string Name { get; set; }

        public SomeEntity(string id, string name)
        {
            Id   = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}
