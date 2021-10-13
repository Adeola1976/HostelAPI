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

        public async Task<UserResponse> Register(RegRequest regRequest)
        {
            string errors = String.Empty;
            AppUser RegUserModel = UserMapping.GetUser(regRequest);
            var result = await _UserManager.CreateAsync(RegUserModel, regRequest.Password);

          

            if (result.Succeeded)
            {
              //  await _UserManager.AddToRoleAsync(RegUserModel, "Customer");
                var emailToken = await _UserManager.GenerateEmailConfirmationTokenAsync(RegUserModel);
                var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);



                string url = $"{_configuration["AppUrl"]}api/v1/Auth/confirmemail?email={RegUserModel.Email}&token={validEmailToken}";
                var mailDto = new MailRequest
                {
                    ToEmail = RegUserModel.Email,
                    Subject = "Confirm your email",
                    Body = $"<h1>Welcome to Dominion Koncept</h1>\n<p>Pls confirm your email by <a href='{url}'>clicking here</a></p>",
                    Attachments = null
                };

                await _mailservice.SendEmailAsync(mailDto);
                return UserMapping.GetUserResponse(RegUserModel);
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

            if (AUserModel != null)
            {
                if (await _UserManager.CheckPasswordAsync(AUserModel, userRequest.Password))
                {
                    var response = UserMapping.GetUserResponse(AUserModel);
                    response.Token = await _TokenGenerator.GenerateToken(AUserModel);
                    return response;
                }
                throw new AccessViolationException("Invalid Credentials");
            }

            throw new AccessViolationException("Invalid Credentials");
        }
    }
}
