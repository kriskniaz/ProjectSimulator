using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class StoryCommand : ICommand<StoryCommand>
    {
        public StoryCommand() { }

        public StoryCommand(Guid id, int pointValue, Guid projectId, 
            string description, int isDone, ICollection<CommitCommand> commitCommands)
        {
            Id = id;
            PointValue = pointValue;
            ProjectId = projectId;
            Description = description;
            IsDone = isDone;
        }
        public Guid Id { get; set; }

        public int PointValue { get; set; }
        
        public Guid? SprintId { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }

        public int IsDone { get; set; }
        public ICollection<CommitCommand> CommitCommands { get; set; }
    }
}
