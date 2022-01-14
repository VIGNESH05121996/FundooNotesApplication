// <copyright file="IFundooUserRL.cs" company="Fundoo Notes Application">
//     IFundooUserRL copyright tag.
// </copyright>

namespace Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Common.UserModels;
    using Repository.Entities;

    /// <summary>
    /// Reposirtory Layer Interface for FundooUser
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IFundooUserRL<TEntity>
    {
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        RegistrationResponse Register(RegistrationModel model);

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        string Login(LoginModel model);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        string ForgetPassword(ForgetPasswordModel model);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="email">The email.</param>
        bool ResetPassword(ResetPasswordModel model, string email);

        /// <summary>
        /// Redises the user.
        /// </summary>
        List<FundooUser> RedisUser();
    }
}
