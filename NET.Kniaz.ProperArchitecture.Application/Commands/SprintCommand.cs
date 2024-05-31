using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class SprintCommand : ICommand<SprintCommand>
    {
        public SprintCommand() { }

        public SprintCommand(Guid id, int startWeek, int endWeek, 
            Guid projectId, Guid teamId, int commitedPoints, 
            int deliveredPoints, ICollection<StoryCommand> storyCommands)
        {
            Id = id;
            StartWeek = startWeek;
            EndWeek = endWeek;
            ProjectId = projectId;
            TeamId = teamId;
            CommitedPoints = commitedPoints;
            DeliveredPoints = deliveredPoints;
            StoryCommands = storyCommands;
        }

 
        public Guid Id { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TeamId { get; set; }
        public int CommitedPoints { get; set; }
        public int DeliveredPoints { get; set; }
        public ICollection<StoryCommand> StoryCommands { get; set; }
    }
}
