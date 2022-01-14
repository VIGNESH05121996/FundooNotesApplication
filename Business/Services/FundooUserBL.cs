using Business.Interfaces;
using Common.Models;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class FundooUserBL : IFundooUserBL<FundooUser>
    {
        public Repository.Interfaces.IFundooUserRL<FundooUser> fundooUserRL;

        public FundooUserBL(IFundooUserRL<FundooUser> fundooUserRL)
        {
            this.fundooUserRL = fundooUserRL;
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Register(RegistrationModel model)
        {
            try
            {
                this.fundooUserRL.Register(model);
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
                return this.fundooUserRL.Login(model);
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
                return this.fundooUserRL.ForgetPassword(model);
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
        public bool ResetPassword(ResetPasswordModel model, string email)
        {
            try
            {
                return this.fundooUserRL.ResetPassword(model, email);
            }
            catch (Exception ex)
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
                return this.fundooUserRL.RedisUser();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
