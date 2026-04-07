using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using System.Net.Http.Headers;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class ResourceCommandHandler :GenericCommandHandler, ICommandHandler<ResourceCommand>
    {

        private IEntityRepository<Resource> _resourceRepository;

        public ResourceCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._resourceRepository = _unitOfWork.ResourceRepository;
        }

        public async Task AddEntity(ICommand<ResourceCommand> command)
        {
            await _resourceRepository.Add(EntitiesCommandsMapper.MapToResource(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<ResourceCommand> command)
        {
             _resourceRepository.Remove(EntitiesCommandsMapper.MapToResource(command));
            Commit();
        }

        public async Task UpdateEntity(ICommand<ResourceCommand> command)
        {
            await _resourceRepository.Update(EntitiesCommandsMapper.MapToResource(command));
            await CommitAsync();
        }        

        public async Task<ResourceCommand> GetEntityAsync(Guid id)
        {
            ResourceCommand command = new ResourceCommand();
            Resource resource = await _resourceRepository.Get(id);
            
            if (resource != null)
            { 
                command = new ResourceCommand(resource.Id, resource.FirstName, resource.LastName, resource.Title);
            
            }
            return command;
        }

        public async Task<ResourceCommand> GetFullEntityAsync(Guid id)
        {
            ResourceCommand command = new ResourceCommand();
            Resource resource = await _resourceRepository.GetFullEntity(id);
            
            if (resource != null)
            {
                command = new ResourceCommand(resource.Id, resource.FirstName, resource.LastName, resource.Title);
            }
            return command;
        }
        
        public async Task<ResourceCommand> GetEntityAsync(String name)
        {
            ResourceCommand command = new ResourceCommand();
            Resource resource = await _resourceRepository.Get(name);
            
            if (resource != null)
            {
                command = new ResourceCommand(resource.Id, resource.FirstName, resource.LastName, resource.Title);
            }
            return command;
        }
        
        public async Task<List<ResourceCommand>> GetEntitiesAsync()
        {
            var resources = await _resourceRepository.GetAll();
            
            List<ResourceCommand> result = new List<ResourceCommand>();

            if (resources != null)
            {
                result = resources.Select(resource=>EntitiesCommandsMapper.MapToResourceCommand(resource)).ToList();
            }
            return result;
        }       
        
        public async Task<List<ResourceCommand>> GetManyEntitiesAsync(int selector)
        {
            Expression<Func<Resource, bool>> filter = null;
            List<ResourceCommand> result = new List<ResourceCommand>();
            var resources = await _resourceRepository.GetMany(filter);

            if (resources != null)
            {
                result = resources.Select(resource => EntitiesCommandsMapper.MapToResourceCommand(resource)).ToList();
            }
            return result;

        }
    }
}
