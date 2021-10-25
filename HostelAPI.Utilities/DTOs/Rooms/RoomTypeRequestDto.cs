using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Utilities.DTOs.Rooms
{
      public class RoomTypeRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public decimal Price { get; set; }

     
        public string Discount{ get; set; }
    }
}
