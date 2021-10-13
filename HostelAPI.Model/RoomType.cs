using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
    public class RoomType : BaseEntity
    {
      
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }     
        public string PublicId { get; set; }
    
        public ICollection<Room> Rooms { get; set; }

    }
}
