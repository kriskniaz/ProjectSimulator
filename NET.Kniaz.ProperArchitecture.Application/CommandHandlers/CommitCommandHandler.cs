using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class CommitCommandHandler : GenericCommandHandler, ICommandHandler<CommitCommand>
    {
        IEntityRepository<Commit> _commitRepository;

        public CommitCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _commitRepository = unitOfWork.CommitRepository;
        }

        public async Task AddEntity(ICommand<CommitCommand> command)
        {
            await _commitRepository.Add(EntitiesCommandsMapper.MapToCommit(command));
            await CommitAsync();
        }

        public async Task UpdateEntity(ICommand<CommitCommand> command)
        {
            await _commitRepository.Update(EntitiesCommandsMapper.MapToCommit(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<CommitCommand> command)
        {
            _commitRepository.Remove(EntitiesCommandsMapper.MapToCommit(command));
            Commit();
        }

        public async Task<CommitCommand> GetEntityAsync(Guid id)
        {
            CommitCommand commitCommand = new CommitCommand();

            Commit commit = await _commitRepository.Get(id);

            if (commit != null)
            {
                commitCommand = EntitiesCommandsMapper.MapToCommitCommand(commit);
            }

            return commitCommand;
        }

        public async Task<CommitCommand> GetFullEntityAsync(Guid id)
        {
            CommitCommand commitCommand = new CommitCommand();

            Commit commit = await _commitRepository.GetFullEntity(id);

            if (commit != null)
            {
                commitCommand = EntitiesCommandsMapper.MapToCommitCommand(commit);
            }

            return commitCommand;
        }

        public async Task<CommitCommand> GetEntityAsync(String name)
        {
            throw new NotImplementedException();        
        }
           

        public async Task<List<CommitCommand>> GetEntitiesAsync()
        {
            var commits = await _commitRepository.GetAll();
            List<CommitCommand> result = new List<CommitCommand>();

            if (commits != null)
            {
                result = commits.Select(commit => EntitiesCommandsMapper.MapToCommitCommand(commit)).ToList();
            }

            return result;
        }

        public async Task<List<CommitCommand>> GetManyEntitiesAsync(int selector)
        {
            Expression<Func<Commit, bool>> filter=null; 
            var commits = await _commitRepository.GetMany(filter);
            List<CommitCommand> result = new List<CommitCommand>();

            if (commits != null)
            {
                result = commits.Select(commit => EntitiesCommandsMapper.MapToCommitCommand(commit)).ToList();
            }

            return result;
        }


 
    }
}
