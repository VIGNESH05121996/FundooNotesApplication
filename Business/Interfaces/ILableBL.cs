// <copyright file="ILableBL.cs" company="Fundoo Notes Application">
//     ILableBL copyright tag.
// </copyright>

namespace Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.LableModel;
    using Repository.Entities;

    /// <summary>
    /// Business Layer Interface for Lable
    /// </summary>
    public interface ILableBL
    {
        /// <summary>
        /// Creates the lable.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <param name="model">The model.</param>
        LableResponseModel CreateLable(long notesId, long jwtUserId, LableModel model);

        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="lable">The lable.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        void DeleteLable(FundooLable lable, long jwtUserId);

        /// <summary>
        /// Gets the lable with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        LableResponseModel GetLableWithId(long lableId, long jwtUserId);

        /// <summary>
        /// Gets all lable.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        LableResponseModel GetAllLable(long jwtUserId);

        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="updateLable">The update lable.</param>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        LableResponseModel UpdateLable(FundooLable updateLable, UpdateLableModel model, long jwtUserId);

        /// <summary>
        /// Gets the lables with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        FundooLable GetLablesWithId(long lableId, long jwtUserId);

        /// <summary>
        /// Adds the lable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        LableResponseModel AddLable(LableModel model, long jwtUserId);
    }
}
