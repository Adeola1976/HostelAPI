using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Utilities.DTOs.User
{
    public class ResetPasswordResponseDto
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public string Token { get; set; }

        public string  Email { get; set; }
    }
}
