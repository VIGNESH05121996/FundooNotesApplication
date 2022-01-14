using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class FundooUserRL : IFundooUserRL<FundooUser>
    {
        readonly FundooUserContext context;

        private readonly IConfiguration _config;

        public FundooUserRL(FundooUserContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }

        /// <summary>
        /// Encrypteds the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string EncryptedPassword(string password)
        {
            try
            {
                byte[] encptPass = new byte[password.Length];
                encptPass = Encoding.UTF8.GetBytes(password);
                string encrypted = Convert.ToBase64String(encptPass);
                return encrypted;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// JWTs the token generate.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string JwtTokenGenerate(string email, long userId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, email),
                        new Claim("UserId",userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Register(RegistrationModel model)
        {
            try
            {
                FundooUser user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = EncryptedPassword(model.Password),
                    CreatedAt = model.CreatedAt
                };
                this.context.Add(user);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string Login(LoginModel model)
        {
            try
            {
                var loginValidation = this.context.UserTable.FirstOrDefault(e => e.Email == model.Email && e.Password == EncryptedPassword(model.Password));
                if (loginValidation != null)
                {
                    var token = JwtTokenGenerate(model.Email, loginValidation.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var emailValidation = this.context.UserTable.FirstOrDefault(e => e.Email == model.Email);
                if (emailValidation != null)
                {
                    var token = JwtTokenGenerate(emailValidation.Email, emailValidation.UserId);
                    new MsmqModel().MsmqSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public bool ResetPassword(ResetPasswordModel model,string email)
        {
            try
            {
                var resetPassword = this.context.UserTable.FirstOrDefault(e => e.Email == email);
                if (resetPassword != null && model.NewPassword == model.ConfirmPassword)
                {
                    resetPassword.Password = EncryptedPassword(model.NewPassword);
                    this.context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Redises the user.
        /// </summary>
        /// <returns></returns>
        public List<FundooUser> RedisUser()
        {
            try
            {
                return this.context.UserTable.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
