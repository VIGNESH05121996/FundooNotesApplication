using Business.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
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

        [HttpGet("GetAllData")]
        public IActionResult GetAllData()
        {
            try
            {
                IEnumerable<FundooUser> user = userBL.GetAllData();
                if (user == null)
                {
                    return BadRequest(new { Success = false, message = "There are no users in database " });
                }
                return Ok(new { Success = true, message = "Fundoo Users In DataBase ", user });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetWithId/{id}")]
        public IActionResult GetWithId(long id)
        {
            try
            {
                FundooUser user = userBL.GetWithId(id);
                if (user == null)
                {
                    return BadRequest(new { Success = false, message = "No User With Particular Id " });
                }
                return Ok(new { Success = true, message = "User Available with Entered Id ", user });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("UpdateDataWithId/{id}")]
        public IActionResult Update(long id, [FromBody] FundooUser user)
        {
            try
            {
                FundooUser updateUser = userBL.GetWithId(id);
                if (updateUser == null)
                {
                    return BadRequest(new { Success = false, message = "No User Found With Id" });
                }
                userBL.Update(updateUser, user);
                return Ok(new { Success = true, message = "Udate Sucessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteWithId/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                FundooUser user = userBL.GetWithId(id);
                if (user == null)
                {
                    return BadRequest(new { Success = false, message = "User with entered id not found" });
                }
                userBL.Delete(user);
                return Ok(new { Success = true, message = "User Deleted From DataBase" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                FundooUser credentials = userBL.Login(model.Email, model.Password);
                if (credentials.Email == null)
                {
                    return BadRequest(new { Success = false, message = "Email or Password Not Found" });
                }
                return Ok(new { Success = true, message = "Login Successful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
