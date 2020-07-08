using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using s18782_Daniel_Janus.Services;

namespace s18782_Daniel_Janus.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskDbService _service;
        
        public TaskController(ITaskDbService service)
        {
            _service = service;
        }

        [HttpGet("{IdTeamMember}")]

        public IActionResult GetTeamMember(int IdTeamMember)
        {
            return Ok(_service.GetTeamMember(IdTeamMember));
        }

        [HttpDelete("{IdProject}")]
        public IActionResult DeleteProject(int IdProject)
        {
            return Ok(_service.DeleteProject(IdProject));
        }
    }
}