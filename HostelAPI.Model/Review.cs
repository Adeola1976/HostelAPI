using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Model
{
    public  class Review : BaseEntity
    {
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public string HotelId { get; set; }
        public string CustomerId { get; set;}
        public Customer Customer { get; set; }
    }
}
