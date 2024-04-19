using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.BLL.Interfaces;
using Talapat.BLL.Specifications;
using Talapat.DAL.Contexts;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }
        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetAllAsync()
        {

            return await context.Set<T>().ToListAsync();
        }

        async Task<T> IGenericRepository<T>.GetById(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        
        public async Task<T> GetByIdWithSpec(ISpecicfication<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecicfication<T> spec)
        {
            return  await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecicfication<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecicfication<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
