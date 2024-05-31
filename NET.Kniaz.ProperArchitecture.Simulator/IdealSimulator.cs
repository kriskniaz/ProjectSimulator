using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Events;


namespace NET.Kniaz.ProperArchitecture.Simulator
{
    public class IdealSimulator : GenericSimulator
    {
        public IdealSimulator(ProjectCommand projectCommand, ICommandHandler<ProjectCommand> projectCommandHandler,
                       ICommandHandler<TeamCommand> teamCommandHandler,
                       ICommandHandler<TeamMemberCommand> teamMemberCommandHandler,
                       ICommandHandler<CommitCommand> commitCommandHandler,
                       ICommandHandler<StoryCommand> storyCommandHandler,
                       ICommandHandler<SprintCommand> sprintCommandHandler,
                       ICommandHandler<ResourceCommand> resourceCommandHandler,
                       ICommandHandler<CurrencyCommand> currencyCommandHandler,
                       ICommandHandler<SimulationResultCommand> simulationResultCommandHandler)
            :base(projectCommand, 
                 projectCommandHandler, 
                 teamCommandHandler, 
                 teamMemberCommandHandler, 
                 commitCommandHandler, 
                 storyCommandHandler, 
                 sprintCommandHandler, 
                 resourceCommandHandler, 
                 currencyCommandHandler, 
                 simulationResultCommandHandler)
        {        }

        public async Task Run()
        {
            await RunPlannedSprints(false);
            await SaveStatistics("IdealSimulator");

        }

        protected override async Task HandleSprint(SprintCommand sprintCommand, IEnumerator<StoryCommand> sEnumerator, bool withEvents = false)
        {
            int teamMembers = _projectCommand.Teams.FirstOrDefault().TeamMembers.Count;
            int commitedPoints = 0;          
            var enumeratorStories = sEnumerator;
            int startingVelocity = teamMembers * 10;
            var randomVelocity = new Random().Next(startingVelocity, startingVelocity+teamMembers*2);

            //story loop
            while (((Math.Abs(commitedPoints - randomVelocity) > randomVelocity * 0.1) && (commitedPoints < 100)))
            {
                enumeratorStories.MoveNext();
                var storyCommand = enumeratorStories.Current;

                if (storyCommand == null)
                {
                    break;
                }

                commitedPoints += storyCommand.PointValue;
                storyCommand.SprintId = sprintCommand.Id;
                storyCommand.IsDone = 1;
                await _storyCommandHandler.UpdateEntity(storyCommand);
                await GenerateCommits(storyCommand);
            }

            sprintCommand.CommitedPoints = commitedPoints;
            sprintCommand.DeliveredPoints = commitedPoints;
            await _sprintCommandHandler.UpdateEntity(sprintCommand);
        }

    }
}
