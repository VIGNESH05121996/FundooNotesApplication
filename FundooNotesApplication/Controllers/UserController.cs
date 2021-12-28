using Business.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IFundooUserBL<FundooUser> userBL;
        public UserController(IFundooUserBL<FundooUser> userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Success = false, message = "Details Missing" });
                }
                userBL.Register(model);
                return Ok(new { Success = true, message = "Registration Successfull " });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                string credentials = userBL.Login(model);
                if (credentials == null)
                {
                    return BadRequest(new { Success = false, message = "Email or Password Not Found" });
                }
                return Ok(new { Success = true, message = "Login Successful",JwtToken=credentials});
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
