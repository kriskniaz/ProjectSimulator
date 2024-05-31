
using NET.Kniaz.ProperArchitecture.Application.Abstractions;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class CommitCommand : ICommand<CommitCommand>
    {
        public CommitCommand() { }

        public CommitCommand(Guid id, Guid storyId, string description)
        { 
            Id = id;
            StoryId = storyId;
            Description = description;
        }
        
        public Guid Id { get; set; }
        public Guid StoryId { get; set; }
        public string Description { get; set; }
    }
}
