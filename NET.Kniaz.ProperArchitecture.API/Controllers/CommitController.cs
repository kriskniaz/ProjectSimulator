using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Commands;


namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitController : ControllerBase
    {
        private ICommandHandler<CommitCommand> _commitCommandHandler;

        public CommitController(ICommandHandler<CommitCommand> commitCommandHandler)
        {
            this._commitCommandHandler = commitCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommitCommand>> GetCommit(Guid id)
        {
            var commit = await _commitCommandHandler.GetFullEntityAsync(id);
            return Ok(commit);
        }

        [HttpGet]
        public async Task<ActionResult<List<CommitCommand>>> GetAllCommits()
        {
            var commits = await _commitCommandHandler.GetEntitiesAsync();
            return Ok(commits);
        }

        [HttpPut]
        public async Task<ActionResult<CommitCommand>> UpdateCommit(CommitCommand commit)
        {
            await _commitCommandHandler.UpdateEntity(commit);
            return CreatedAtAction(nameof(GetCommit), new { id = commit.Id }, commit);
        }

        [HttpDelete]
        public void DeleteCommit(CommitCommand commit)
        {
            _commitCommandHandler.RemoveEntity(commit);
        }
    }
}
