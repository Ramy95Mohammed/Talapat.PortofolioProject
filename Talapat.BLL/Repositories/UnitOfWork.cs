using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.BLL.Interfaces;
using Talapat.DAL.Contexts;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repoistory<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if(!_repositories.Contains(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _repositories.Add(type, repository);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
