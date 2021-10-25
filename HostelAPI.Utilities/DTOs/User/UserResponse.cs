using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Utilities.DTOs.Register
{
    public class UserResponse
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Role{ get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePublicId { get; set; }
        public string PhoneNumber { get; set; }
        public string Id { get; set; }

        public string Token { get; set; }
    }
}
