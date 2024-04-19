using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.BLL.Specifications;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetById(int? id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetByIdWithSpec(ISpecicfication<T> spec);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecicfication<T> spec);
        public Task<int> GetCountAsync(ISpecicfication<T> spec);
        public Task Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
