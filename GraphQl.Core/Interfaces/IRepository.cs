using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GraphQl.Core.Entities;

namespace GraphQl.Core.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);
        Task AddRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
