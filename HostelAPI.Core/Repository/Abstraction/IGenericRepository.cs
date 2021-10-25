using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        void Update(T entity);
        void DeleteAsync(T entity);
        IQueryable<T> GetEntity();
    }
}
