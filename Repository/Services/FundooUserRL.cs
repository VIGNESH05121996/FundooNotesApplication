using Common.Models;
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

        public FundooUserRL(FundooUserContext context)
        {
            this.context = context;
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
                var loginValidation= this.context.UserTable.FirstOrDefault(e => e.Email == model.Email && e.Password == EncryptedPassword(model.Password));
                if(loginValidation != null)
                {
                    string key = "MyFundooSecretKey-VIGNESH05121996";
                    var loginTokenHandler = new JwtSecurityTokenHandler();
                    var loginTokenKey = Encoding.ASCII.GetBytes(key);
                    var loginTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("Email", model.Email)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(loginTokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                    return loginTokenHandler.WriteToken(token);
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

        public static string EncryptedPassword(string password)
        {
            try
            {
                byte[] encptPass = new byte[password.Length];
                encptPass = Encoding.UTF8.GetBytes(password);
                string encrypted = Convert.ToBase64String(encptPass);
                return encrypted;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
