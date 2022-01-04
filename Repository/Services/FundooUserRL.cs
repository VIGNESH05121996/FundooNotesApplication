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

        public static string EncryptedPassword(string password)
        {
            try
            {
                byte[] encptPass = new byte[password.Length];
                encptPass = Encoding.UTF8.GetBytes(password);
                string encrypted = Convert.ToBase64String(encptPass);
                return encrypted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string JwtTokenGenerate(string email, long userId)
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
            catch (Exception)
            {
                throw;
            }
        }

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
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var emailValidation = this.context.UserTable.FirstOrDefault(e => e.Email == model.Email);
                if (emailValidation != null)
                {
                    var token = JwtTokenGenerate(emailValidation.Email, emailValidation.UserId);
                    new MsmqModel().MsmqSender(token);
                    return "Email Sent";
                }
                else
                {
                    return "Email not sent";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(ResetPasswordModel model,string email)
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
    }
}
