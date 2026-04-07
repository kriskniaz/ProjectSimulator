using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private ICommandHandler<ResourceCommand> _resourceCommandHandler;

        public ResourceController(ICommandHandler<ResourceCommand> resourceCommandHandler)
        {
            this._resourceCommandHandler = resourceCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceCommand>> GetResource(Guid id)
        {
            var resource = await _resourceCommandHandler.GetFullEntityAsync(id);
            return Ok(resource);
        }
        [HttpGet]
        public async Task<ActionResult<List<ResourceCommand>>> GetAllResources()
        {
            var resources = await _resourceCommandHandler.GetEntitiesAsync();
            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<ResourceCommand>> AddCurrency(ResourceCommand resource)
        {
            await _resourceCommandHandler.AddEntity(resource);
            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);
        }


        [HttpPut]
        public async Task<ActionResult<ResourceCommand>> UpdateCurrency(ResourceCommand resource)
        {
            await _resourceCommandHandler.UpdateEntity(resource);
            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);
        }

        [HttpDelete]
        public void DeleteCurrency(ResourceCommand resource)
        {
            _resourceCommandHandler.RemoveEntity(resource);
        }

    }
}
