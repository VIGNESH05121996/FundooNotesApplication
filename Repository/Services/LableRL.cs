using Common.LableModel;
using Common.NotesModels;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class LableRL : ILableRL
    {
        readonly FundooUserContext context;
        public LableRL(FundooUserContext context)
        {
            this.context = context;
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
                try
                {
                    var validNotesAndUser = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                    FundooLable lable = new()
                    {
                        NotesId = notesId,
                        UserId = jwtUserId,
                        Lable_Name = model.Lable_Name
                    };
                    this.context.Add(lable);
                    this.context.SaveChanges();

                    LableResponseModel responseModel = new()
                    {
                        LableId = lable.LableId,
                        NotesId = lable.NotesId,
                        UserId = lable.UserId,
                        Lable_Name = lable.Lable_Name
                    };
                    return responseModel;
                }
                catch (Exception ex)
                {
                    throw;
                }
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var response = this.context.LableTable.FirstOrDefault(e=> e.UserId == jwtUserId);
                    LableResponseModel model = new()
                    {
                        LableId = response.LableId,
                        NotesId = response.NotesId,
                        UserId = response.UserId,
                        Lable_Name = response.Lable_Name
                    };
                    return model;
                }
                return null;
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    return this.context.LableTable.FirstOrDefault(e => e.LableId == lableId);
                }
                return null;
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var response=this.context.LableTable.FirstOrDefault(e => e.LableId == lableId && e.UserId == jwtUserId);
                    LableResponseModel model = new()
                    {
                        LableId=response.LableId,
                        NotesId=response.NotesId,
                        UserId=response.UserId,
                        Lable_Name=response.Lable_Name
                    };
                    return model;
                }
                return null;
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    updateLable.Lable_Name = model.Lable_Name;
                    updateLable.NotesId = model.NotesId;
                    this.context.SaveChanges();
                }
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
        public void DeleteLable(FundooLable lable, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    this.context.LableTable.Remove(lable);
                    this.context.SaveChanges();
                }
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
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    FundooLable lable = new()
                    {
                        Lable_Name = model.Lable_Name,
                        UserId = jwtUserId
                    };
                    this.context.Add(lable);
                    this.context.SaveChanges();
                    return lable;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
