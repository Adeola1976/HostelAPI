using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
     public  class Gallery : BaseEntity
    {
        public RoomType Roomtype { get; set; }
        public ICollection<Room> Room  { get; set; }
    }
}
