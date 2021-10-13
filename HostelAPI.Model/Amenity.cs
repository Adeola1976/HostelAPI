using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
    public class Amenity : BaseEntity
    {
        public string Name { get; set; }

        public string  BookingId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Booking Bookings { get; set; }
    }
}
