using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Implementation
{
   public  class UnitOfWork:IUnitOfWork
    {
    
        private IRoomTypeRepository _roomType;
     
        private readonly HDBContext _context;

        public UnitOfWork(HDBContext context)
        {
            _context = context;
        }
    

        public IRoomTypeRepository RoomType => _roomType ??= new RoomTypeRepository(_context);
       



        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

