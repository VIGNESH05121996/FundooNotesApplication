using Business.Interfaces;
using Common.CollaboratorModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        readonly ICollaborateBL collaborateBL;
        public CollaboratorController(ICollaborateBL collaborateBL)
        {
            this.collaborateBL = collaborateBL;
        }

        [HttpPost]
        public IActionResult AddCollaborate(CollaborateModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Success = false, message = "Email Missing For Collaboration" });
                }
                collaborateBL.AddCollaborate(model);
                return Ok(new { Success = true, message = "Collaboration Successfull " });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, StackTraceException = ex.StackTrace });
            }
        }

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
