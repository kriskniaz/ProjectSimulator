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
using NET.Kniaz.ProperArchitecture.Persistence.Repositories;
using NET.Kniaz.ProperArchitecture.Application.Utils;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class TeamMemberCommandHandler :GenericCommandHandler, ICommandHandler<TeamMemberCommand>
    {
        private IEntityRepository<TeamMember> _teamMemberRepository;

        public TeamMemberCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)  
        {
            this._teamMemberRepository = _unitOfWork.TeamMemberRepository;
        }

        public async Task AddEntity(ICommand<TeamMemberCommand> command)
        {
            await _teamMemberRepository.Add(EntitiesCommandsMapper.MapToTeamMember(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<TeamMemberCommand> command)
        {
            _teamMemberRepository.Remove(EntitiesCommandsMapper.MapToTeamMember(command));
            Commit();
        }

        public async Task UpdateEntity(ICommand<TeamMemberCommand> command)
        {
            await _teamMemberRepository.Update(EntitiesCommandsMapper.MapToTeamMember(command));
            await CommitAsync();
        }
        public async Task<TeamMemberCommand> GetEntityAsync(Guid id)
        {
            TeamMemberCommand command = new TeamMemberCommand();
            TeamMember teamMember = await _teamMemberRepository.Get(id);
            if (teamMember != null)
            {
                command = EntitiesCommandsMapper.MapToTeamMemberCommand(teamMember);
            }
            return command;
        }

        public async Task<TeamMemberCommand> GetFullEntityAsync(Guid id)
        {
            TeamMemberCommand command = new TeamMemberCommand();
            TeamMember teamMember = await _teamMemberRepository.GetFullEntity(id);
            if (teamMember != null)
            {
                command = EntitiesCommandsMapper.MapToTeamMemberCommand(teamMember);
            }
            return command;
        }

        public async Task<TeamMemberCommand> GetEntityAsync(String name)
        {
            TeamMemberCommand command = new TeamMemberCommand();
            TeamMember teamMember = await _teamMemberRepository.Get(name);
            if (teamMember != null)
            {
                command = EntitiesCommandsMapper.MapToTeamMemberCommand(teamMember);
            }
            return command;
        }
        public async Task<List<TeamMemberCommand>> GetEntitiesAsync()
        {
            List<TeamMemberCommand> result = new List<TeamMemberCommand>();

            var teamMembers = await _teamMemberRepository.GetAll();

            if (teamMembers != null)
            {
                result = teamMembers.Select(tm=>EntitiesCommandsMapper.MapToTeamMemberCommand(tm)).ToList();
            }

            return result;
        }

        public Task<List<TeamMemberCommand>> GetManyEntitiesAsync(int selector)
        {
            throw new NotImplementedException();
        }
    }
}
