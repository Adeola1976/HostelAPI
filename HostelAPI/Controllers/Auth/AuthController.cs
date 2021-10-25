using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HostelAPI.Controllers.Auth
{

    public class AuthController : ControllerBase
    {

        private readonly IAuthetication _Authetication;
        private readonly IConfiguration _Configuration;
        public AuthController(IAuthetication Auth, IConfiguration configuration)
        {
            _Authetication = Auth;
            _Configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegRequest regRequest)
        {
            try
            {
                var result = await _Authetication.Register(regRequest);
             
                return Created("", result);
            }

            catch (MissingFieldException Mes)
            {
                return BadRequest(Mes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
          
                var handler = new JwtSecurityTokenHandler();
         

                try
                {
                    var response = await _Authetication.Login(userRequest);

                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(response));
                    JwtSecurityToken decodedValue = handler.ReadJwtToken(response.Token);
                    var Claim = decodedValue.Claims.ElementAt(3);
                    response.Role = Claim.Value;
                    return Ok(response);
                }
                catch (AccessViolationException)
                {
                    return BadRequest();
                }


       }
         
       
        [HttpGet("confirmemail")]

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
         
                var result = await _Authetication.ConfirmEmail(Id, token);
                if (result.IsSuccess) 
                return Redirect($"{ _Configuration["ReactUrl"]}/login");
                return BadRequest("Incorrect Credentials");
        }
            


        


        [HttpPost("forgetpassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] UserRequest userRequest)
        {


            try
            {
                var result = await _Authetication.ForgetPassword(userRequest);

                return Ok(result);

            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
        }



        [HttpGet("reset-password")]

        public async Task<IActionResult> ResetPassword(string Email, string token)
        {
            try
            {
                var result = await _Authetication.ResponseForResetPassword(Email, token);
               
                if (result.IsSuccess==true)
                {
                    return Redirect($"{ _Configuration["ReactUrl"]}/resetpassword?email={Email}&token={token}");
                }

                return Ok(result);

            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }

        }

            [HttpPost("resetpassword")]

        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordUserRequest userRequest)
        {
            try

            {
                var result = await _Authetication.ResetPassword(userRequest);
                return Ok(result);
            }


            catch (MissingFieldException Mes)
            {
                return BadRequest(Mes.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

