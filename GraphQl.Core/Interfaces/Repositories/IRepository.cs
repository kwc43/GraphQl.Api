using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQl.Core.Entities;
using GraphQl.Core.Interfaces.Specifications;

namespace GraphQl.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetBySpecificationAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> GetAllBySpecificationAsync(ISpecification<T> specification);

        Task<T> AddAsync(T entity);

        Task Delete(T entity);

        Task Update(T entity);
    }
}
