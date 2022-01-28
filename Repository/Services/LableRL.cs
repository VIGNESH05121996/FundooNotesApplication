// <copyright file="LableRL.cs" company="Fundoo Notes Application">
//     LableRL copyright tag.
// </copyright>

namespace Repository.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Common.LableModel;
    using Common.NotesModels;
    using Repository.Context;
    using Repository.Entities;
    using Repository.ExceptionHandling;
    using Repository.Interfaces;

    /// <summary>
    /// Repository Layer Lable Table
    /// </summary>
    /// <seealso cref="Repository.Interfaces.ILableRL" />
    public class LableRL : ILableRL
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly FundooUserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LableRL"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
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
        /// <returns>Response body for created lable</returns>
        public LableResponseModel CreateLable(long notesId, long jwtUserId, LableModel model)
        {
            try
            {
                var validNotesAndUser = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                FundooLable lable = new ()
                {
                    NotesId = notesId,
                    UserId = jwtUserId,
                    Lable_Name = model.Lable_Name
                };
                this.context.Add(lable);
                this.context.SaveChanges();

                LableResponseModel responseModel = new ()
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
                throw new CustomException(HttpStatusCode.BadRequest, "Details missing to create lable");
            }
        }

        /// <summary>
        /// Gets all lable.
        /// </summary>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response body of all lables</returns>
        public LableResponseModel GetAllLable(long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var response = this.context.LableTable.FirstOrDefault(e => e.UserId == jwtUserId);
                    LableResponseModel model = new ()
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
                throw new CustomException(HttpStatusCode.NotFound, "No lables found");
            }
        }

        /// <summary>
        /// Gets the lables with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Lables with particular lableId</returns>
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
                throw new CustomException(HttpStatusCode.NotFound, "No lables found");
            }
        }

        /// <summary>
        /// Gets the lable with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response body of lable with particular lableId</returns>
        public LableResponseModel GetLableWithId(long lableId, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    var response = this.context.LableTable.FirstOrDefault(e => e.LableId == lableId && e.UserId == jwtUserId);
                    LableResponseModel model = new ()
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
                throw new CustomException(HttpStatusCode.NotFound, "No lable found");
            }
        }

        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="updateLable">The update lable.</param>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response Body of update lable</returns>
        public LableResponseModel UpdateLable(FundooLable updateLable, UpdateLableModel model, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    updateLable.Lable_Name = model.Lable_Name;
                    updateLable.NotesId = model.NotesId;
                    this.context.SaveChanges();

                    var response = this.context.LableTable.FirstOrDefault(e => e.UserId == jwtUserId);
                    LableResponseModel models = new ()
                    {
                        LableId = response.LableId,
                        NotesId = response.NotesId,
                        UserId = response.UserId,
                        Lable_Name = response.Lable_Name
                    };
                    return models;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No lable found to update");
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
                throw new CustomException(HttpStatusCode.NotFound, "No lable found to delete");
            }
        }

        /// <summary>
        /// Adds the lable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns>Response Body for Added Lable</returns>
        public LableResponseModel AddLable(LableModel model, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    FundooLable lable = new ()
                    {
                        Lable_Name = model.Lable_Name,
                        UserId = jwtUserId
                    };
                    this.context.Add(lable);
                    this.context.SaveChanges();

                    LableResponseModel models = new ()
                    {
                        LableId = lable.LableId,
                        NotesId = lable.NotesId,
                        UserId = lable.UserId,
                        Lable_Name = lable.Lable_Name
                    };
                    return models;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Cannot add lable");
            }
        }
    }
}
