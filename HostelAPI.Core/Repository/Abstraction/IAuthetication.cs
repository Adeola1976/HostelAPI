using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs.Register;
using HostelAPI.Utilities.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Repository.Abstraction
{
   public interface IAuthetication
    {
        Task<UserResponse> Register(RegRequest regRequest);

        Task<UserResponse> Login(UserRequest userRequest);

        Task<UserConfirmEmailDTO> ConfirmEmail(string UserId, string token);
    }
}
