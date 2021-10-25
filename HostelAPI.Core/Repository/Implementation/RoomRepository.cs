using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Database.Context;
using HostelAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Implementation
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        
        private readonly HDBContext _context;
        private readonly DbSet<Room> _dbSet;

        public RoomRepository(HDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Room>();
        }


    }
}
