using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;


namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ICommandHandler<ProjectCommand> _projectCommandHandler;

        public ProjectController(ICommandHandler<ProjectCommand> projectCommandHandler)
        {
            _projectCommandHandler = projectCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectCommand>> GetProject(Guid id)
        {
            var project = await _projectCommandHandler.GetFullEntityAsync(id);
            return Ok(project);
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectCommand>>> GetAllProjects()
        {
            var projects = await _projectCommandHandler.GetEntitiesAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectCommand>> AddProject(ProjectCommand project)
        {
            await _projectCommandHandler.AddEntity(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }


        [HttpPut]
        public async Task<ActionResult<ProjectCommand>> UpdateProject(ProjectCommand project)
        {
            await _projectCommandHandler.UpdateEntity(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpDelete]
        public void DeleteProject(ProjectCommand project)
        {
            _projectCommandHandler.RemoveEntity(project);
        }
    }
}
