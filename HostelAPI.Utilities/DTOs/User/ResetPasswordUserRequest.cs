using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Utilities.DTOs.User
{
    public  class ResetPasswordUserRequest
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }

    
        [Required]
        public string Token { get; set; }


        [Required]
   
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 50)]
        public string ComfirmPassword { get; set; }
    }
}
