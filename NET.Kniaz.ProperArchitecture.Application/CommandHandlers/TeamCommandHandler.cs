using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;



namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class TeamCommandHandler : GenericCommandHandler, ICommandHandler<TeamCommand>
    {
        private IEntityRepository<Team> _teamRepository;

        public TeamCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._teamRepository = unitOfWork.TeamRepository;
        }

        public async Task AddEntity(ICommand<TeamCommand> command)
        {
            await _teamRepository.Add(EntitiesCommandsMapper.MapToTeam(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<TeamCommand> command)
        {
             _teamRepository.Remove(EntitiesCommandsMapper.MapToTeam(command));
            Commit();
        }

        public async Task UpdateEntity(ICommand<TeamCommand> command)
        {
            await _teamRepository.Update(EntitiesCommandsMapper.MapToTeam(command));
            await CommitAsync();
        }

        public async Task<List<TeamCommand>> GetEntitiesAsync()
        {
            var teams = await _teamRepository.GetAll();
            
            List<TeamCommand> result = new List<TeamCommand>();

            if (teams != null)
            {
                result = teams.Select(team => EntitiesCommandsMapper.MapToTeamCommand(team)).ToList();
            }

            return result;

        }

        public async Task<TeamCommand> GetEntityAsync(Guid id)
        {
            TeamCommand command = new TeamCommand();
            Team team = await _teamRepository.Get(id);
            if (team != null)
            {
                command = EntitiesCommandsMapper.MapToTeamCommand(team);

            }
            return command;
        }

        public async Task<TeamCommand> GetFullEntityAsync(Guid id)
        {
            TeamCommand command = new TeamCommand();
            Team team = await _teamRepository.GetFullEntity(id);
            if (team != null)
            {
                command = EntitiesCommandsMapper.MapToTeamCommand(team);
            }
            return command;
        }

        public async Task<TeamCommand> GetEntityAsync(String name)
        {
            TeamCommand command = new TeamCommand();
            Team team = await _teamRepository.Get(name);
            if (team != null)
            {
                command = EntitiesCommandsMapper.MapToTeamCommand(team);
            }
            return command;
        }

        public Task<List<TeamCommand>> GetManyEntitiesAsync(int selector)
        {
            throw new NotImplementedException();
        }
    }
}
