using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQl.Core.Entities;
using GraphQl.Core.Interfaces;
using GraphQl.Core.Interfaces.Repositories;
using GraphQl.Core.Interfaces.Specifications;
using GraphQl.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        protected Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> specification)
        {
            var result = await GetAllBySpecificationAsync(specification);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAllBySpecificationAsync(ISpecification<TEntity> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(_applicationDbContext.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = specification.IncludesStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return await secondaryResult
                .Where(specification.Criteria)
                .ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        { 
            _applicationDbContext.Set<TEntity>().Add(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
