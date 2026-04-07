using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class SprintCommandHandler : GenericCommandHandler, ICommandHandler<SprintCommand>
    {
        IEntityRepository<Sprint> _sprintRepository;

        public SprintCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _sprintRepository = unitOfWork.SprintRepository;
        }

        public async Task AddEntity(ICommand<SprintCommand> command)
        {
            await _sprintRepository.Add(EntitiesCommandsMapper.MapToSprint(command));
            await CommitAsync();
        }

        public async Task UpdateEntity(ICommand<SprintCommand> command)
        {
            await _sprintRepository.Update(EntitiesCommandsMapper.MapToSprint(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<SprintCommand> command)
        {
            _sprintRepository.Remove(EntitiesCommandsMapper.MapToSprint(command));
            Commit();
        }

        public async Task<SprintCommand> GetEntityAsync(Guid id)
        {
            SprintCommand sprintCommand = new SprintCommand();

            Sprint sprint = await _sprintRepository.Get(id);

            if (sprint != null)
            {
                sprintCommand = EntitiesCommandsMapper.MapToSprintCommand(sprint);
            }

            return sprintCommand;
        }

        public async Task<SprintCommand> GetFullEntityAsync(Guid id)
        {
            SprintCommand sprintCommand = new SprintCommand();

            Sprint sprint = await _sprintRepository.GetFullEntity(id);

            if (sprint != null)
            {
                sprintCommand = EntitiesCommandsMapper.MapToSprintCommand(sprint);
            }

            return sprintCommand;
        }

        public async Task<SprintCommand> GetEntityAsync(String name)
        {
            SprintCommand sprintCommand = new SprintCommand();

            Sprint sprint = await _sprintRepository.Get(name);

            if (sprint != null)
            {
                sprintCommand = EntitiesCommandsMapper.MapToSprintCommand(sprint);
            }

            return sprintCommand;
        }

        public async Task<List<SprintCommand>> GetEntitiesAsync()
        {
            var sprints = await _sprintRepository.GetAll();
            List<SprintCommand> result = new List<SprintCommand>();

            if (sprints != null)
            {
                result = sprints.Select(sprint => EntitiesCommandsMapper.MapToSprintCommand(sprint)).ToList();
            }

            return result;
        }

        public Task<List<SprintCommand>> GetManyEntitiesAsync(int selector)
        {
            throw new NotImplementedException();
        }
    }
}
