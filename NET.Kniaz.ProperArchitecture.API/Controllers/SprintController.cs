using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private ICommandHandler<SprintCommand> _sprintCommandHandler;

        public SprintController(ICommandHandler<SprintCommand> sprintCommandHandler)
        {
            this._sprintCommandHandler = sprintCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SprintCommand>> GetSprint(Guid id)
        {
            var sprint = await _sprintCommandHandler.GetFullEntityAsync(id);
            return Ok(sprint);
        }

        [HttpGet]
        public async Task<ActionResult<List<SprintCommand>>> GetAllSprints()
        {
            var sprints = await _sprintCommandHandler.GetEntitiesAsync();
            return Ok(sprints);
        }

        [HttpPost]
        public async Task<ActionResult<SprintCommand>> AddSprint(SprintCommand sprint)
        {
            await _sprintCommandHandler.AddEntity(sprint);
            return CreatedAtAction(nameof(GetSprint), new { id = sprint.Id }, sprint);
        }

        [HttpPut]
        public async Task<ActionResult<SprintCommand>> UpdateSprint(SprintCommand sprint)
        {
            await _sprintCommandHandler.UpdateEntity(sprint);
            return CreatedAtAction(nameof(GetSprint), new { id = sprint.Id }, sprint);
        }

        [HttpDelete]
        public void DeleteSprint(SprintCommand sprint)
        {
            _sprintCommandHandler.RemoveEntity(sprint);
        }   
    }
}
