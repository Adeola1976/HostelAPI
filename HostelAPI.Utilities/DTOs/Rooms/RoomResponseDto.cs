using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Utilities.DTOs.Rooms
{
   public  class RoomResponseDto
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }

    }
}
