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
    public class ProjectCommandHandler : GenericCommandHandler, ICommandHandler<ProjectCommand>
    {
        private IEntityRepository<Project> _projectRepository;

        public ProjectCommandHandler(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            this._projectRepository = _unitOfWork.ProjectRepository;
        }

        public async Task AddEntity(ICommand<ProjectCommand> command)
        {
            await _projectRepository.Add(EntitiesCommandsMapper.MaptoProject(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<ProjectCommand> command)
        {
            _projectRepository.Remove(EntitiesCommandsMapper.MaptoProject(command));
            Commit();
        }

        public async Task UpdateEntity(ICommand<ProjectCommand> command)
        {
            await _projectRepository.Update(EntitiesCommandsMapper.MaptoProject(command));
            await CommitAsync();
        }
        public async Task<ProjectCommand> GetEntityAsync(Guid id)
        {
            ProjectCommand command = new ProjectCommand();

            Project project = await _projectRepository.Get(id);
            if (project != null)
            {
                command = EntitiesCommandsMapper.MapToProjectCommand(project);

            }
            return command;
        }

        public async Task<ProjectCommand> GetFullEntityAsync(Guid id)
        {
            ProjectCommand command = new ProjectCommand();

            Project project = await _projectRepository.GetFullEntity(id);
            if (project != null)
            {
                command = EntitiesCommandsMapper.MapToProjectCommand(project);
            }
            return command;
        }

        public async Task<ProjectCommand> GetEntityAsync(String name)
        {
            ProjectCommand command = new ProjectCommand();

            Project project = await _projectRepository.Get(name);
            if (project != null)
            {
                command = EntitiesCommandsMapper.MapToProjectCommand(project);
            }
            return command;
        }   

        public async Task<List<ProjectCommand>> GetEntitiesAsync()
        {
            List<ProjectCommand> result = new List<ProjectCommand>();
            var projects = await _projectRepository.GetAll();

            if(projects != null) 
            {
                result = projects.Select(project => EntitiesCommandsMapper.MapToProjectCommand(project)).ToList();
            }
            return result;
        }

        public async Task<List<ProjectCommand>> GetManyEntitiesAsync(int selector)
        {
            Expression<Func<Project, bool>> filter = null;
            List<ProjectCommand> result = new List<ProjectCommand>();
            var projects = await _projectRepository.GetMany(filter);

            if (projects != null)
            {
                result = projects.Select(project => EntitiesCommandsMapper.MapToProjectCommand(project)).ToList();
            }
            return result;
        }

    }
}
