// <copyright file="UserController.cs" company="Fundoo Notes Application">
//     UserController copyright tag.
// </copyright>

namespace FundooNotesApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.Models;
    using Common.UserModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using Repository.Entities;
    using Repository.ExceptionHandling;

    /// <summary>
    /// User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The user bl
        /// </summary>
        private readonly IFundooUserBL<FundooUser> userBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user bl.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
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
        /// <returns>Response Body after registration</returns>
        [HttpPost("register")]
        public IActionResult Register(RegistrationModel model)
        {
            if (model == null)
            {
                return NotFound(new { Success = false, message = "Details Missing" });
            }
            RegistrationResponse user = userBL.Register(model);
            return Ok(new { Success = true, message = "Registration Successfull ", user });
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json Web Token after login</returns>
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            string credentials = userBL.Login(model);
            if (credentials == null)
            {
                return NotFound(new { Success = false, message = "Email or Password Not Found" });
            }
            return Ok(new { Success = true, message = "Login Successful", JwtToken = credentials });
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Email Sent message</returns>
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {
            string forgetPassword = userBL.ForgetPassword(model);
            if (forgetPassword == null)
            {
                return NotFound(new { Success = false, message = "Email not in database" });
            }
            return Ok(new { Success = true, message = "Forget Password Mail Sent" });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Password reset message</returns>
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
            bool resetPassword = userBL.ResetPassword(model, email);
            if (resetPassword)
            {
                return Ok(new { Success = true, message = "Password Reset Successful" });
            }
            return NotFound(new { Success = false, message = "New Password not match with confirm password" });
        }

        /// <summary>
        /// Redises the catching fundoo user.
        /// </summary>
        /// <returns>All user in data base</returns>
        [HttpGet("redis")]
        public async Task<IActionResult> RedisCatchingFundooUser()
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
            return NotFound(new { Success = false, message = "No User Found With UserId" });
        }
    }
}
