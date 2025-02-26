﻿// <copyright file="LableController.cs" company="Fundoo Notes Application">
//     LableController copyright tag.
// </copyright>

namespace FundooNotesApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.LableModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Entities;

    /// <summary>
    /// Lable Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        /// <summary>
        /// The lable bl
        /// </summary>
        private readonly ILableBL lableBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LableController"/> class.
        /// </summary>
        /// <param name="lableBL">The lable bl.</param>
        public LableController(ILableBL lableBL)
        {
            this.lableBL = lableBL;
        }

        /// <summary>
        /// Creates the lable.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Response Body After creating lable</returns>
        [HttpPost("{notesId}")]
        public IActionResult CreateLable(long notesId, LableModel model)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            if (jwtUserId == 0 && notesId == 0)
            {
                return NotFound(new { Success = false, message = "Name Missing For Lable" });
            }
            LableResponseModel lable = lableBL.CreateLable(notesId, jwtUserId, model);
            return Ok(new { Success = true, message = "Lable Created", lable });
        }

        /// <summary>
        /// Gets all lable.
        /// </summary>
        /// <returns>Response body of all lables</returns>
        [HttpGet]
        public IActionResult GetAllLable()
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            LableResponseModel lable = lableBL.GetAllLable(jwtUserId);
            if (lable == null)
            {
                return NotFound(new { Success = false, message = "No lable in database " });
            }
            return Ok(new { Success = true, message = "Retrived All lables ", lable });
        }

        /// <summary>
        /// Gets the lable with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <returns>Response Body of lable with particular lableId</returns>
        [HttpGet("{lableId}")]
        public IActionResult GetLableWithId(long lableId)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            LableResponseModel lable = lableBL.GetLableWithId(lableId, jwtUserId);
            if (lable == null)
            {
                return NotFound(new { Success = false, message = "No Lable With Particular LableId " });
            }
            return Ok(new { Success = true, message = "Retrived Lable ", lable });
        }

        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Respose Body after updating the lable</returns>
        [HttpPut("{lableId}")]
        public IActionResult UpdateLable(long lableId, UpdateLableModel model)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            FundooLable updateLable = lableBL.GetLablesWithId(lableId, jwtUserId);
            if (updateLable == null)
            {
                return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
            }
            LableResponseModel lable = lableBL.UpdateLable(updateLable, model, jwtUserId);
            return Ok(new { Success = true, message = "Lable Updated Sucessfully", lable });
        }

        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <returns>Delete Message</returns>
        [HttpDelete("{lableId}")]
        public IActionResult DeleteLable(long lableId)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            FundooLable lable = lableBL.GetLablesWithId(lableId, jwtUserId);
            if (lable == null)
            {
                return NotFound(new { Success = false, message = "No Lable Found" });
            }
            lableBL.DeleteLable(lable, jwtUserId);
            return Ok(new { Success = true, message = "Lable Removed" });
        }

        /// <summary>
        /// Adds the lable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Response Body after adding lable</returns>
        [HttpPost("AddLable")]
        public IActionResult AddLable(LableModel model)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            if (jwtUserId == 0)
            {
                return NotFound(new { Success = false, message = "No Notes Found With NotesId" });
            }
            LableResponseModel addLable = lableBL.AddLable(model, jwtUserId);
            return Ok(new { Success = true, message = "Lable Updated Sucessfully", addLable });
        }
    }
}
