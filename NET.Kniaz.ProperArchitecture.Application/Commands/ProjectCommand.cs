
using NET.Kniaz.ProperArchitecture.Application.Abstractions;


namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class ProjectCommand : ICommand<ProjectCommand>
    {
        public ProjectCommand() { }
        public ProjectCommand(Guid id, string name, string description,
            int sweek, int eweek, int syear, int eyear)
        { 
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.StartWeek = sweek;
            this.EndWeek = eweek;
            this.StartYear = syear;
            this.EndYear = eyear;
        }

        public ProjectCommand(Guid id, string name, 
            string description,
            int sweek, int eweek, int syear, int eyear,
            ICollection<TeamCommand> teams,
            ICollection<SprintCommand> sprints,
            ICollection<StoryCommand> stories
            )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.StartWeek = sweek;
            this.EndWeek = eweek;
            this.StartYear = syear;
            this.EndYear = eyear;
            this.Teams = teams;
            this.Sprints = sprints;
            this.Stories = stories;
        }


        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public ICollection<TeamCommand> Teams { get; set; }

        public ICollection<SprintCommand> Sprints { get; set; }

        public ICollection<StoryCommand> Stories { get; set; }
    }
}
