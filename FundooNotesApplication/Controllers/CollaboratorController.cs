using Business.Interfaces;
using Common.CollaboratorModels;
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
    public class CollaboratorController : ControllerBase
    {
        readonly ICollaborateBL collaborateBL;
        public CollaboratorController(ICollaborateBL collaborateBL)
        {
            this.collaborateBL = collaborateBL;
        }

        /// <summary>
        /// Adds the collaborate.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("{notesId}")]
        public IActionResult AddCollaborate(long notesId, CollaborateModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId == 0 && notesId == 0)
                {
                    return BadRequest(new { Success = false, message = "Email Missing For Collaboration" });
                }
                CollabResponseModel collaborate = collaborateBL.AddCollaborate(notesId,jwtUserId,model);
                return Ok(new { Success = true, message = "Collaboration Successfull ",collaborate });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = "No Notes With Particular NotesId", Exception_Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes the collaborate.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns></returns>
        [HttpDelete("{collabId}")]
        public IActionResult DeleteCollaborate(long collabId)
        {
            try
            {
                FundooCollaborate collab = collaborateBL.GetCollabWithId(collabId);
                if (collab == null)
                {
                    return BadRequest(new { Success = false, message = "No Collaboration Found" });
                }
                collaborateBL.DeleteCollab(collab);
                return Ok(new { Success = true, message = "Collaborated Email Removed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
