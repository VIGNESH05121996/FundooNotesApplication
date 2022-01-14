using Business.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IFundooUserBL<FundooUser> userBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public UserController(IFundooUserBL<FundooUser> userBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.userBL = userBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                string forgetPassword = userBL.ForgetPassword(model);
                if (forgetPassword == null)
                {
                    return BadRequest(new { Success = false, message = "Email not in database" });
                }
                return Ok(new { Success = true, message = "Forget Password Mail Sent" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                bool resetPassword = userBL.ResetPassword(model,email);
                if (resetPassword)
                {
                    return Ok(new { Success = true, message = "Password Reset Successful" });
                }
                return BadRequest(new { Success = false, message = "New Password not match with confirm password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Redises the catching fundoo user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("redis")]
        public async Task<IActionResult> RedisCatchingFundooUser()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId != 0)
                {
                    var cacheKey = "fundooUserList";
                    string serializedNotesList;
                    var userList = new List<FundooUser>();
                    var redisUserList = await distributedCache.GetAsync(cacheKey);
                    if (redisUserList != null)
                    {
                        serializedNotesList = Encoding.UTF8.GetString(redisUserList);
                        userList = JsonConvert.DeserializeObject<List<FundooUser>>(serializedNotesList);
                    }
                    else
                    {
                        userList = (List<FundooUser>)userBL.RedisUser();
                        serializedNotesList = JsonConvert.SerializeObject(userList);
                        redisUserList = Encoding.UTF8.GetBytes(serializedNotesList);
                        return Ok(userList);
                    }
                }
                return BadRequest(new { Success = false, message = "No User Found With NotesId" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
