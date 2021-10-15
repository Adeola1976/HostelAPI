﻿using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Register(RegRequest regRequest)
        {
            try
            {
                var result = await _Authetication.Register(regRequest);
                return Created("", result);
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



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest userRequest)
        {
            try
            {
                var response = await _Authetication.Login(userRequest);
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


            try

            {
                var result = await _Authetication.ConfirmEmail(Id, token);
                return Created("", result);
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


        [HttpPost("forgetpassword")]
        public async Task<IActionResult> ForgetPassword(string Email)
        {


            try
            {
                var result = await _Authetication.ForgetPassword(Email);

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

                return Ok(result);

            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }

        }

            [HttpPost("resetpassword")]

        public async Task<IActionResult> ForgetPassword([FromForm]ResetPasswordUserRequest userRequest)
        {
            try

            {
                var result = await _Authetication.ResetPassword(userRequest);
                return Created("", result);
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

