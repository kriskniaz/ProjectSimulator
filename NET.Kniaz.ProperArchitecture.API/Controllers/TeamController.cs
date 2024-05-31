using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ICommandHandler<TeamCommand> _teamCommandHandler;

        public TeamController(ICommandHandler<TeamCommand> teamCommandHandler)
        {
            _teamCommandHandler = teamCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamCommand>> GetTeam(Guid id)
        {
            var team = await _teamCommandHandler.GetFullEntityAsync(id);
            return Ok(team);
        }
        [HttpGet]
        public async Task<ActionResult<List<TeamCommand>>> GetAllTeams()
        {
            var teams = await _teamCommandHandler.GetEntitiesAsync();
            return Ok(teams);
        }

        [HttpPost]
        public async Task<ActionResult<TeamCommand>> AddTeam(TeamCommand team)
        {
            await _teamCommandHandler.AddEntity(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }


        [HttpPut]
        public async Task<ActionResult<TeamCommand>> UpdateTeam(TeamCommand team)
        {
            await _teamCommandHandler.UpdateEntity(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        [HttpDelete]
        public void DeleteTeam(TeamCommand team)
        {
            _teamCommandHandler.RemoveEntity(team);
        }
    }
}
