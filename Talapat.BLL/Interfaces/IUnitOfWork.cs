using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities;

namespace Talapat.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repoistory<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
