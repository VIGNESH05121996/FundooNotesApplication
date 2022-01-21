// <copyright file="CollaboratorController.cs" company="Fundoo Notes Application">
//     CollaboratorController copyright tag.
// </copyright>

namespace FundooNotesApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Common.CollaboratorModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Entities;

    /// <summary>
    /// Collaborator Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The collaborate bl
        /// </summary>
        private readonly ICollaborateBL collaborateBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="collaborateBL">The collaborate bl.</param>
        public CollaboratorController(ICollaborateBL collaborateBL)
        {
            this.collaborateBL = collaborateBL;
        }

        /// <summary>
        /// Adds the collaborate.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Respose Body After Adding Collaborator to Data base</returns>
        [HttpPost("{notesId}")]
        public IActionResult AddCollaborate(long notesId, CollaborateModel model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (jwtUserId == 0 && notesId == 0)
                {
                    return NotFound(new { Success = false, message = "Email Missing For Collaboration" });
                }
                CollabResponseModel collaborate = collaborateBL.AddCollaborate(notesId, jwtUserId, model);
                return Ok(new { Success = true, message = "Collaboration Successfull ", collaborate });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes the collaborate.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns>Delete Message</returns>
        [HttpDelete("{collabId}")]
        public IActionResult DeleteCollaborate(long collabId)
        {
            try
            {
                FundooCollaborate collab = collaborateBL.GetCollabWithId(collabId);
                if (collab == null)
                {
                    return NotFound(new { Success = false, message = "No Collaboration Found" });
                }
                collaborateBL.DeleteCollab(collab);
                return Ok(new { Success = true, message = "Collaborated Email Removed" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }
    }
}
