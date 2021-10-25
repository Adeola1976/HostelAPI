using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Database.Context;
using HostelAPI.Model;
using HostelAPI.Utilities.DTOs;
using HostelAPI.Utilities.DTOs.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HostelAPI.Core.Repository.Implementation
{
    public class RoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        private readonly HDBContext _context;
        private readonly DbSet<RoomType> _dbSet;

        public RoomTypeRepository(HDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<RoomType>();
        }



        public IEnumerable<RoomType> GetRoomTypeByName(string name)
        {
            var check = _dbSet.Where(x => x.Name == name);
            return check;
        }

        public RoomType FindEntityById(string id)
        {
            var check = _dbSet.Find(id);
            return check;
        }

       
    }
}
