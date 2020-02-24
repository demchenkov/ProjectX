using Core.Entities.BaseProperties;

namespace Core.Entities
{
    public sealed class Location : IBaseEntityId, IBaseEntityName, IBaseEntityDescription
    {
        public int Id { get; }
        public string Name { get; }

        public string Description { get; }

        public int LocationLevel { get; }

        public Location(int id, string name, string description, int locationLevel)
        {
            Id = id;
            Name = name;
            Description = description;
            LocationLevel = locationLevel;
        }
    }
}