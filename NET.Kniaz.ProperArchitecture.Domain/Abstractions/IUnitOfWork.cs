using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
        void DetachAllEntities();

        IEntityRepository<Currency> CurrencyRepository { get; }
        IEntityRepository<Resource> ResourceRepository { get; }
        IEntityRepository<Project> ProjectRepository { get; }
        IEntityRepository<Team> TeamRepository { get; }
        IEntityRepository<TeamMember> TeamMemberRepository { get; }
        IEntityRepository<Sprint> SprintRepository { get; }
        IEntityRepository<Story> StoryRepository { get; }
        IEntityRepository<Commit> CommitRepository { get; }
        IEntityRepository<SimulationResult> SimulationResultRepository { get; }
    }
}
