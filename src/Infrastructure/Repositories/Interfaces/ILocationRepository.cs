using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ILocationRepository : IBaseCrudRepository<Location>
    {
        Task GetLocationByLevel(int level);
    }
}