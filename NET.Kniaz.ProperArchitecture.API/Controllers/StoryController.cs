using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private ICommandHandler<StoryCommand> _storyCommandHandler;

        public StoryController(ICommandHandler<StoryCommand> storyCommandHandler)
        {
            this._storyCommandHandler = storyCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoryCommand>> GetStory(Guid id)
        {
            var story = await _storyCommandHandler.GetFullEntityAsync(id);
            return Ok(story);
        }

        [HttpGet]
        public async Task<ActionResult<List<StoryCommand>>> GetAllStories()
        {
            var stories = await _storyCommandHandler.GetEntitiesAsync();
            return Ok(stories);
        }

        [HttpPost]
        public async Task<ActionResult<StoryCommand>> AddStory(StoryCommand story)
        {
            await _storyCommandHandler.AddEntity(story);
            return CreatedAtAction(nameof(GetStory), new { id = story.Id }, story);
        }

        [HttpPut]
        public async Task<ActionResult<StoryCommand>> UpdateStory(StoryCommand story)
        {
            await _storyCommandHandler.UpdateEntity(story);
            return CreatedAtAction(nameof(GetStory), new { id = story.Id }, story);
        }

    }
}
