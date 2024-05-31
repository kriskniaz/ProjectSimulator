using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private ICommandHandler<TeamMemberCommand> _commandHandler;

        public TeamMemberController(ICommandHandler<TeamMemberCommand> commandHandler) 
        { 
            this._commandHandler = commandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMemberCommand>> GetTeamMember(Guid id)
        {
            var teamMember = await _commandHandler.GetFullEntityAsync(id);
            return Ok(teamMember);
        }
        [HttpGet]
        public async Task<ActionResult<List<TeamMemberCommand>>> GetAllTeamMembers()
        {
            var teamMembers = await _commandHandler.GetEntitiesAsync();
            return Ok(teamMembers);
        }

        [HttpPost]
        public async Task<ActionResult<TeamMemberCommand>> AddTeamMember(TeamMemberCommand teamMember)
        {
            await _commandHandler.AddEntity(teamMember);
            return CreatedAtAction(nameof(GetTeamMember), new { id = teamMember.Id }, teamMember);
        }


        [HttpPut]
        public async Task<ActionResult<TeamMemberCommand>> UpdateTeamMember(TeamMemberCommand teamMember)
        {
            await _commandHandler.UpdateEntity(teamMember);
            return CreatedAtAction(nameof(GetTeamMember), new { id = teamMember.Id }, teamMember);
        }

        [HttpDelete]
        public void DeleteTeamMember(TeamMemberCommand teamMember)
        {
            _commandHandler.RemoveEntity(teamMember);
        }
    }
}
