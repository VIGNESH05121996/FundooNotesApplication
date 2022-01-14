using Business.Interfaces;
using Common.LableModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        readonly ILableBL lableBL;
        public LableController(ILableBL lableBL)
        {
            this.lableBL = lableBL;
        }

        /// <summary>
        /// Creates the lable.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("{notesId}")]
        public IActionResult CreateLable(long notesId, LableModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId == 0 && notesId == 0)
                {
                    return BadRequest(new { Success = false, message = "Name Missing For Lable" });
                }
                LableResponseModel lable=lableBL.CreateLable(notesId, jwtUserId, model);
                return Ok(new { Success = true, message = "Lable Created",lable });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = "No Notes With Particular NotesId", Exception_Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Gets all lable.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllLable()
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                LableResponseModel lable = lableBL.GetAllLable(jwtUserId);
                if (lable == null)
                {
                    return BadRequest(new { Success = false, message = "No lable in database " });
                }
                return Ok(new { Success = true, message = "Retrived All lables ", lable });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Gets the lable with identifier.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <returns></returns>
        [HttpGet("{lableId}")]
        public IActionResult GetLableWithId(long lableId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                LableResponseModel lable = lableBL.GetLableWithId(lableId, jwtUserId);
                if (lable == null)
                {
                    return BadRequest(new { Success = false, message = "No Lable With Particular LableId " });
                }
                return Ok(new { Success = true, message = "Retrived Lable ", lable });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("{lableId}")]
        public IActionResult UpdateLable(long lableId, UpdateLableModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooLable updateLable = lableBL.GetLablesWithId(lableId, jwtUserId);
                if (updateLable == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                LableResponseModel lable=lableBL.UpdateLable(updateLable, model, jwtUserId);
                return Ok(new { Success = true, message = "Lable Updated Sucessfully",lable });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="lableId">The lable identifier.</param>
        /// <returns></returns>
        [HttpDelete("{lableId}")]
        public IActionResult DeleteLable(long lableId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                FundooLable lable = lableBL.GetLablesWithId(lableId,jwtUserId);
                if (lable == null)
                {
                    return BadRequest(new { Success = false, message = "No Lable Found" });
                }
                lableBL.DeleteLable(lable,jwtUserId);
                return Ok(new { Success = true, message = "Lable Removed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Adds the lable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("AddLable")]
        public IActionResult AddLable(LableModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId == 0)
                {
                    return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });
                }
                LableResponseModel addLable = lableBL.AddLable(model, jwtUserId);
                return Ok(new { Success = true, message = "Lable Updated Sucessfully", addLable });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
