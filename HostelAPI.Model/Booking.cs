using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
    public class Booking : BaseEntity
    {
        public string CustomerId { get; set; }
        public string HotelId { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public ICollection<Amenity> Amentities { get; set; }
    }
}
