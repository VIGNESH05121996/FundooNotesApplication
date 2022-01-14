// <copyright file="FundooUserBL.cs" company="Fundoo Notes Application">
//     FundooUserBL copyright tag.
// </copyright>

namespace Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.Models;
    using Common.UserModels;
    using Repository.Entities;
    using Repository.Interfaces;
    using Repository.Services;

    /// <summary>
    /// Business Layer for Fundoo User
    /// </summary>
    /// <seealso cref="Business.Interfaces.IFundooUserBL&lt;Repository.Entities.FundooUser&gt;" />
    public class FundooUserBL : IFundooUserBL<FundooUser>
    {
        /// <summary>
        /// The fundoo user rl
        /// </summary>
        private Repository.Interfaces.IFundooUserRL<FundooUser> fundooUserRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundooUserBL"/> class.
        /// </summary>
        /// <param name="fundooUserRL">The fundoo user rl.</param>
        public FundooUserBL(IFundooUserRL<FundooUser> fundooUserRL)
        {
            this.fundooUserRL = fundooUserRL;
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public RegistrationResponse Register(RegistrationModel model)
        {
            try
            {
                return this.fundooUserRL.Register(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model"></param>
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
