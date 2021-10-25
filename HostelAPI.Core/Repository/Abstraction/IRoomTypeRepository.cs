using HostelAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Abstraction
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        IEnumerable<RoomType> GetRoomTypeByName(string name);
        public RoomType FindEntityById(string id);
    }
}
