// <copyright file="FundooUserRL.cs" company="Fundoo Notes Application">
//     FundooUserRL copyright tag.
// </copyright>

namespace Repository.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Common.UserModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Repository.Context;
    using Repository.Entities;
    using Repository.ExceptionHandling;
    using Repository.Interfaces;

    /// <summary>
    /// Repository Layer Fundoo User
    /// </summary>
    /// <seealso cref="Repository.Interfaces.IFundooUserRL&lt;Repository.Entities.FundooUser&gt;" />
    public class FundooUserRL : IFundooUserRL<FundooUser>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly FundooUserContext context;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundooUserRL"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="config">The configuration.</param>
        public FundooUserRL(FundooUserContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        /// <summary>
        /// Encrypteds the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Encypted Password</returns>
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
                throw new CustomException(HttpStatusCode.BadRequest, "Password missing for encryption");
            }
        }

        /// <summary>
        /// JWTs the token generate.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Json Web Token</returns>
        public string JwtTokenGenerate(string email, long userId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, email),
                        new Claim("UserId", userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Cannot generate json web token since claims not added");
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Response Body of Registration</returns>
        public RegistrationResponse Register(RegistrationModel model)
        {
            try
            {
                FundooUser user = new ()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = EncryptedPassword(model.Password),
                    CreatedAt = model.CreatedAt
                };
                this.context.Add(user);
                this.context.SaveChanges();
                RegistrationResponse response = new ()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Details already in database");
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json Web Token</returns>
        public string Login(LoginModel model)
        {
            try
            {
                var loginValidation = this.context.UserTable.FirstOrDefault(e => e.Email == model.Email && e.Password == EncryptedPassword(model.Password));
                if (loginValidation != null)
                {
                    var token = this.JwtTokenGenerate(model.Email, loginValidation.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "User Credentiatials wrong");
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json Web Token To Mail</returns>
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var emailValidation = this.context.UserTable.FirstOrDefault(e => e.Email == model.Email);
                if (emailValidation != null)
                {
                    var token = this.JwtTokenGenerate(emailValidation.Email, emailValidation.UserId);
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
                throw new CustomException(HttpStatusCode.NotFound, "No email found in database");
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="email">The email.</param>
        /// <returns>True or False</returns>
        public bool ResetPassword(ResetPasswordModel model, string email)
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
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "Not authorized with token");
            }
        }

        /// <summary>
        /// Redises the user.
        /// </summary>
        /// <returns>All User in User Table</returns>
        public List<FundooUser> RedisUser()
        {
            try
            {
                return this.context.UserTable.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Cannot fetch details");
            }
        }
    }
}
