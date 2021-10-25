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
        Task<RegisterationResponse> Register(RegRequest regRequest);

        Task<UserResponse> Login(UserRequest userRequest);

        Task<UserConfirmEmailDTO> ConfirmEmail(string UserId, string token);

        Task<UserConfirmEmailDTO> ForgetPassword(UserRequest userRequest);

        Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordUserRequest userRequest);
         
        Task<ResetPasswordResponseDto> ResponseForResetPassword(string Email, string token);

    }
        
}
