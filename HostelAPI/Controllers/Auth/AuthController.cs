using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelAPI.Controllers.Auth
{
    public class AuthController : ControllerBase
    {

        private readonly IAuthetication _Authetication;

        public AuthController(IAuthetication Auth)
        {
            _Authetication = Auth;
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
        }



    }
}
