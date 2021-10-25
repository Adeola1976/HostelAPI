using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Implementation
{
   public  class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HDBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(HDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void  DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }


        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable <T> GetEntity()
        {
            return _dbSet;
        }

    }
}
