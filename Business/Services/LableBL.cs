using Business.Interfaces;
using Common.LableModel;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LableBL : ILableBL
    {
        readonly ILableRL lableRL;
        public LableBL(ILableRL lableRL)
        {
            this.lableRL = lableRL;
        }

        /// <summary>
        /// Creates the lable.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LableResponseModel CreateLable(long notesId, long jwtUserId, LableModel model)
        {
            try
            {
                return this.lableRL.CreateLable(notesId, jwtUserId, model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="lable">The lable.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void DeleteLable(FundooLable lable,long jwtUserId)
        {
            try
            {
                this.lableRL.DeleteLable(lable,jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all lable.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public LableResponseModel GetAllLable(long jwtUserId)
        {
            try
            {
                return this.lableRL.GetAllLable(jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the lables with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public FundooLable GetLablesWithId(long lableId, long jwtUserId)
        {
            try
            {
                return this.lableRL.GetLablesWithId(lableId, jwtUserId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the lable with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public LableResponseModel GetLableWithId(long lableId, long jwtUserId)
        {
            try
            {
                return this.lableRL.GetLableWithId(lableId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="updateLable">The update lable.</param>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        public void UpdateLable(FundooLable updateLable, UpdateLableModel model, long jwtUserId)
        {
            try
            {
                this.lableRL.UpdateLable(updateLable, model, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the lable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public FundooLable AddLable(LableModel model, long jwtUserId)
        {
            try
            {
                return this.lableRL.AddLable(model, jwtUserId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
