using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBaseCrudRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task<T> FindById(int id);
        Task Update(int id, T entity);
        Task Remove(int id);
    }
}