using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Core.Services.Abstraction;
using HostelAPI.Model;
using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs.Mapping;
using HostelAPI.Utilities.DTOs.Register;
using HostelAPI.Utilities.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace HostelAPI.Core.Repository.Implementation
{
    public class Authetication : IAuthetication

    {

        private readonly UserManager<AppUser> _UserManager;
        private readonly ITokenGenerator _TokenGenerator;
        private readonly IMailService _mailservice;
        private readonly IConfiguration _configuration;

        public Authetication(UserManager<AppUser> userManager, ITokenGenerator token, IMailService mailservice, IConfiguration configuration)
        {
            _UserManager = userManager;
            _TokenGenerator = token;
            _mailservice = mailservice;
            _configuration = configuration;
        }

        public async Task<RegisterationResponse> Register(RegRequest regRequest)
        {

            string errors = String.Empty;
            
            AppUser RegUserModel = UserMapping.GetUser(regRequest);
            RegUserModel.UserName = regRequest.Email;
            
            var result = await _UserManager.CreateAsync(RegUserModel, regRequest.Password);



            if (RegUserModel!=null)
            {
                //  await _UserManager.AddToRoleAsync(RegUserModel, "Customer");
                var emailToken = await _UserManager.GenerateEmailConfirmationTokenAsync(RegUserModel);
                var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);



                string url = $"{_configuration["AppUrl"]}/confirmemail?id={RegUserModel.Id}&token={validEmailToken}";
                var mailDto = new MailRequest
                {
                    ToEmail = RegUserModel.Email,
                    Subject = "Confirm your email",
                    Body = $"<h1>Welcome to Dominion Koncept</h1>\n<p>Pls confirm your email by <a href='{url}'>clicking here</a></p>",
                    Attachments = null
                };

                await _mailservice.SendEmailAsync(mailDto);
                return new RegisterationResponse()
                {
                    Message = $"`{RegUserModel.Email} was registered successfully`",
                    IsSuccess = true
                };
            }


            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }

        public async Task<UserResponse> Login(UserRequest userRequest)

        {
            AppUser AUserModel = await _UserManager.FindByEmailAsync(userRequest.Email);


            if (AUserModel != null && AUserModel.EmailConfirmed != false)
            {
                if (await _UserManager.CheckPasswordAsync(AUserModel, userRequest.Password))
                {
                    var mailDto = new MailRequest
                    {
                        ToEmail = AUserModel.Email,
                        Subject = "You are Logged in ",
                        Body = $"<h1>Welcome to Dominion Koncept</h1>\n<p>Welcome {AUserModel.Email}, thanks for service </p>",
                        Attachments = null
                    };

                    await _mailservice.SendEmailAsync(mailDto);
                    var response = UserMapping.GetUserResponse(AUserModel);
                    response.Token = await _TokenGenerator.GenerateToken(AUserModel);
                    return response;
                }



                throw new AccessViolationException("Invalid Password");
            }

            throw new AccessViolationException("Invalid Credentials");
        }


            public async Task<UserConfirmEmailDTO> ConfirmEmail(string UserId, string token)
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token))
                throw new AccessViolationException("incorrect input");

            AppUser user = await _UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                var DecodedToken = WebEncoders.Base64UrlDecode(token);
                string NormalTokenForEmail = Encoding.UTF8.GetString(DecodedToken);
                var CheckIfEmailIsValid = await _UserManager.ConfirmEmailAsync(user, NormalTokenForEmail);




                if (CheckIfEmailIsValid.Succeeded)
                {
                    
                    return new UserConfirmEmailDTO()
                    {
                        Message = "Email Corfirmed successfully",
                        IsSuccess = true
                    };

                }

            }
            return new UserConfirmEmailDTO()
            {
                Message = "Invalid Credentials or Token has  expired",
            };

        }

        public async Task<UserConfirmEmailDTO> ForgetPassword(UserRequest userRequest)
        {

            if (string.IsNullOrWhiteSpace(userRequest.Email))
                throw new AccessViolationException("incorrect input");


            AppUser AUserModel = await _UserManager.FindByEmailAsync(userRequest.Email);

            if (AUserModel != null)
            {
                var passwordToken = await _UserManager.GeneratePasswordResetTokenAsync(AUserModel);
                var encodedPasswordToken = Encoding.UTF8.GetBytes(passwordToken);
                var validPasswordToken = WebEncoders.Base64UrlEncode(encodedPasswordToken);
                string url = $"{_configuration["AppUrl"]}/reset-password?Email={userRequest.Email}&token={validPasswordToken}";
                var mailDto = new MailRequest
                {
                    ToEmail = AUserModel.Email,
                    Subject = "Forget Password",
                    Body = $"<h1>Welcome to Dominion Koncept</h1>\n<p>reset your password <a href='{url}'>click here</a></p>",
                    Attachments = null
                };

                await _mailservice.SendEmailAsync(mailDto);
                return new UserConfirmEmailDTO()
                {
                    Message = "mail being sent for confirmation",
                    IsSuccess = true
                };
            }


            throw new AccessViolationException("Invalid Credentials");

        }

        public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordUserRequest userRequest)
        {
            string errors = String.Empty;

            if (string.IsNullOrWhiteSpace(userRequest.Email) || string.IsNullOrWhiteSpace(userRequest.Token) || string.IsNullOrWhiteSpace (userRequest.Password))
                throw new AccessViolationException("incorrect input");

            AppUser user = await _UserManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                var DecodedToken = WebEncoders.Base64UrlDecode(userRequest.Token);
                string NormalTokenForEmail = Encoding.UTF8.GetString(DecodedToken);

                var result = await _UserManager.ResetPasswordAsync(user, NormalTokenForEmail, userRequest.Password);

                if (result.Succeeded)
                {
                    return new ResetPasswordResponseDto()
                    {
                        Message = "Password Changed Successfully",
                        IsSuccess = true
                    };
                }

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
            }
            throw new MissingFieldException(errors);
        }


        public async Task<ResetPasswordResponseDto> ResponseForResetPassword(string Email, string token)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(token))
                throw new AccessViolationException("incorrect input");

            AppUser user = await _UserManager.FindByEmailAsync(Email);
            if (user != null)
            {
               return  new ResetPasswordResponseDto()
                {
                   
                    Email = Email,
                    IsSuccess = true,
                    Message = "email confirmed"
                };
            }

            throw new AccessViolationException("Invalid Credentials");

        }


    }

}
