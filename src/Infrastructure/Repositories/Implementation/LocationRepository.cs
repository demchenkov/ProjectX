using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implements
{
    public class LocationRepository : ILocationRepository
    {
        public async Task<IEnumerable<Location>> GetAll()
        {
            return new List<Location>()
            {
                new Location(1, "Location1", "Description1", 1),
                new Location(2, "Location2", "Description2", 2),
                new Location(3, "Location3", "Description3", 3),
                new Location(4, "Location4", "Description4", 4),
                new Location(5, "Location5", "Description5", 5)
            };
        }

        public async Task Add(Location entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Location> FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(int id, Location entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task SoftDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetLocationByLevel(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}