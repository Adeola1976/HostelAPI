using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
     public  class Room : BaseEntity
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public RoomType Roomtype { get; set; }
    }
}
