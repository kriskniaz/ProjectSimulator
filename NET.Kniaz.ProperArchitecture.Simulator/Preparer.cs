using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Commands;

namespace NET.Kniaz.ProperArchitecture.Simulator
{
    public class Preparer
    {
        private readonly ICommandHandler<ProjectCommand> _projectCommandHandler;
        private readonly ICommandHandler<TeamCommand> _teamCommandHandler;
        private readonly ICommandHandler<TeamMemberCommand> _teamMemberCommandHandler;
        private readonly ICommandHandler<CommitCommand> _commitCommandHandler;
        private readonly ICommandHandler<StoryCommand> _storyCommandHandler;
        private readonly ICommandHandler<SprintCommand> _sprintCommandHandler;
        private readonly ICommandHandler<ResourceCommand> _resourceCommandHandler;
        private readonly ICommandHandler<CurrencyCommand> _currencyCommandHandler;
        
        private ProjectCommand _projectCommand;

        public Preparer(ICommandHandler<ProjectCommand> projectCommandHandler,
            ICommandHandler<TeamCommand> teamCommandHandler,
            ICommandHandler<TeamMemberCommand> teamMemberCommandHandler,
            ICommandHandler<CommitCommand> commitCommandHandler,
            ICommandHandler<StoryCommand> storyCommandHandler,
            ICommandHandler<SprintCommand> sprintCommandHandler,
            ICommandHandler<ResourceCommand> resourceCommandHandler,
            ICommandHandler<CurrencyCommand> currencyCommandHandler)
        {
            _projectCommandHandler = projectCommandHandler;
            _teamCommandHandler = teamCommandHandler;
            _teamMemberCommandHandler = teamMemberCommandHandler;
            _commitCommandHandler = commitCommandHandler;
            _storyCommandHandler = storyCommandHandler;
            _sprintCommandHandler = sprintCommandHandler;
            _resourceCommandHandler = resourceCommandHandler;
            _currencyCommandHandler = currencyCommandHandler;
            _projectCommand = new ProjectCommand();
            
        }

        public ProjectCommand ProjectCommand 
            { get => _projectCommand;  }




        public async Task OrchestrateData()
        {
            await PrepareProject();
            await PrepareTeam();
            await PrepareSprints();
            await PrepareStories();
        }
        public async Task PrepareProject()
        {

            _projectCommand.Id = Guid.NewGuid();
            _projectCommand.Name = Guid.NewGuid().ToString();
            _projectCommand.Description = Guid.NewGuid().ToString();
            _projectCommand.StartYear = DateTime.Now.Year;
            _projectCommand.EndYear = DateTime.Now.Year;
            _projectCommand.StartWeek = 1;
            _projectCommand.EndWeek =  GetRandomEvenNumber(12,52);
            _projectCommand.Teams = new List<TeamCommand>();
            _projectCommand.Stories = new List<StoryCommand>();
            _projectCommand.Sprints = new List<SprintCommand>();

            await _projectCommandHandler.AddEntity(_projectCommand);

        }

        public async Task PrepareTeam() 
        {
            var teamCommand = new TeamCommand();
            
            var teamMemberCommand = new TeamMemberCommand();


            teamCommand.Name = "Team_" + Guid.NewGuid().ToString();
            teamCommand.Id = Guid.NewGuid();
            teamCommand.Description = Guid.NewGuid().ToString();
            teamCommand.ProjectId = _projectCommand.Id;
            teamCommand.TeamMembers = new List<TeamMemberCommand>();

            await _teamCommandHandler.AddEntity(teamCommand);

            CurrencyCommand currency = await _currencyCommandHandler.GetEntityAsync("USD");
            int teamMembersCount = GetRandomEvenNumber(4, 6);

            for (int i = 0; i < teamMembersCount; i++)
            {
                var resource = new ResourceCommand();
                resource.Title = "Developer";
                resource.FirstName = "John_" + Guid.NewGuid().ToString();
                resource.LastName = "Doe_" + Guid.NewGuid().ToString();
                resource.Id = Guid.NewGuid();
                await _resourceCommandHandler.AddEntity(resource);

                teamMemberCommand = new TeamMemberCommand();

                teamMemberCommand.Id = Guid.NewGuid();
                teamMemberCommand.ResourceId = resource.Id;
                teamMemberCommand.TeamId = teamCommand.Id;
                teamMemberCommand.HourlyRate = 90;
                //usd
                  
                teamMemberCommand.CurrencyId = currency.Id;
                teamMemberCommand.StartYear = _projectCommand.StartYear;
                teamMemberCommand.EndYear = _projectCommand.EndYear;
                teamMemberCommand.StartWeek = _projectCommand.StartWeek;
                teamMemberCommand.EndWeek = _projectCommand.EndWeek;
                teamMemberCommand.Resource = resource;
                teamMemberCommand.Currency = currency;

                await _teamMemberCommandHandler.AddEntity(teamMemberCommand);
                teamCommand.TeamMembers.Add(teamMemberCommand);
            }

            _projectCommand.Teams.Add(teamCommand);

        }
        //Sprints are prepared based on the project length,
        //obviously it does not wokr liek this in real life, where 
        //stories - roadmap are created first and then sprints are planned
        // but for the purpose of the simulation we will create sprints first
        //and stories based on team size and assumed velocity later
        public async Task PrepareSprints()
        {
            
            for (int i = _projectCommand.StartWeek; i < _projectCommand.EndWeek; i+=2)
            {
                var sprintCommand = new SprintCommand();
                sprintCommand.Id = Guid.NewGuid();
                sprintCommand.StartWeek = i;
                sprintCommand.EndWeek = i+1;
                sprintCommand.ProjectId = _projectCommand.Id;
                sprintCommand.TeamId = _projectCommand.Teams.FirstOrDefault().Id;
                sprintCommand.CommitedPoints = 0;
                sprintCommand.DeliveredPoints = 0;
                sprintCommand.StoryCommands = new List<StoryCommand>();

                await _sprintCommandHandler.AddEntity(sprintCommand);
                _projectCommand.Sprints.Add(sprintCommand);
            }
        }

        public async Task PrepareStories()
        {
            int numberOfPointsLimit = _projectCommand.Sprints.Count * _projectCommand.Teams.FirstOrDefault().TeamMembers.Count*10;
            int numberfPoints = 0;
            


            while (numberfPoints < numberOfPointsLimit)
            {
                var storyCommand = new StoryCommand();
                storyCommand.Id = Guid.NewGuid();
                storyCommand.Description = "Story_" + Guid.NewGuid().ToString();
                storyCommand.PointValue = GetRandomOddNumber(1, 7);
                storyCommand.ProjectId = _projectCommand.Id;
                storyCommand.CommitCommands = new List<CommitCommand>();

                await _storyCommandHandler.AddEntity(storyCommand);
                _projectCommand.Stories.Add(storyCommand);
                numberfPoints += storyCommand.PointValue;
            }
        }

        private int GetRandomOddNumber(int min, int max)
        {
            var random = new Random();
            var number = random.Next(min, max);

            while (number % 2 == 0)
            {
                number = random.Next(min, max);
            }

            return number;
        }
        private int GetRandomEvenNumber(int min, int max)
        {
            var random = new Random();
            var number = random.Next(min, max);

            while (number % 2 != 0)
            {
                number = random.Next(min, max);
            }

            return number;
        }

    }
}
