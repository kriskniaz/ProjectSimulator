using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Events;

namespace NET.Kniaz.ProperArchitecture.Simulator
{
    public class GenericSimulator
    {
        protected readonly ICommandHandler<ProjectCommand> _projectCommandHandler;
        protected readonly ICommandHandler<TeamCommand> _teamCommandHandler;
        protected readonly ICommandHandler<TeamMemberCommand> _teamMemberCommandHandler;
        protected readonly ICommandHandler<CommitCommand> _commitCommandHandler;
        protected readonly ICommandHandler<StoryCommand> _storyCommandHandler;
        protected readonly ICommandHandler<SprintCommand> _sprintCommandHandler;
        protected readonly ICommandHandler<ResourceCommand> _resourceCommandHandler;
        protected readonly ICommandHandler<CurrencyCommand> _currencyCommandHandler;
        protected readonly ICommandHandler<SimulationResultCommand> _simulationResultCommandHandler;
        protected readonly ProjectCommand _projectCommand;

        protected int _numberOfExecutedSprints;
        protected int _numberOfPlannedSprints;
        protected int _numberOfPlannedPoints;
        protected int _numberOfDeliveredPoints;
        protected List<ISprintEvent> _sprintEvents;

        public GenericSimulator(ProjectCommand projectCommand, ICommandHandler<ProjectCommand> projectCommandHandler,
                       ICommandHandler<TeamCommand> teamCommandHandler,
                       ICommandHandler<TeamMemberCommand> teamMemberCommandHandler,
                       ICommandHandler<CommitCommand> commitCommandHandler,
                       ICommandHandler<StoryCommand> storyCommandHandler,
                       ICommandHandler<SprintCommand> sprintCommandHandler,
                       ICommandHandler<ResourceCommand> resourceCommandHandler,
                       ICommandHandler<CurrencyCommand> currencyCommandHandler,
                       ICommandHandler<SimulationResultCommand> simulationResultCommandHandler)
        {
            {
                _projectCommandHandler = projectCommandHandler;
                _teamCommandHandler = teamCommandHandler;
                _teamMemberCommandHandler = teamMemberCommandHandler;
                _commitCommandHandler = commitCommandHandler;
                _storyCommandHandler = storyCommandHandler;
                _sprintCommandHandler = sprintCommandHandler;
                _resourceCommandHandler = resourceCommandHandler;
                _currencyCommandHandler = currencyCommandHandler;
                _simulationResultCommandHandler = simulationResultCommandHandler;
                _projectCommand = projectCommand;

                _numberOfExecutedSprints = 0;
                _numberOfPlannedSprints = projectCommand.Sprints.Count;
                _numberOfPlannedPoints = projectCommand.Stories.Sum(s => s.PointValue);
                _sprintEvents = new List<ISprintEvent>();
                
            }
        }

        protected async Task SaveStatistics(string simulatorName)
        {
            _numberOfDeliveredPoints = _projectCommand.Sprints.Sum(s => s.DeliveredPoints);
            int numberOfDeveloperEvents = 0;
            int developerEventsImpact = 0;
            int numberOfScopeEvents = 0;
            int scopeEventsImpact = 0;
            foreach(var sprintEvent in _sprintEvents)
            {
                string eventType = sprintEvent.GetType().Name;
                if (eventType == "TeamMemberNotAvailableEvent")
                {
                    numberOfDeveloperEvents++;
                    developerEventsImpact += sprintEvent.GetSprintImpact();
                }
                else if (eventType == "NewScopeEvent")
                {
                    numberOfScopeEvents++;
                    scopeEventsImpact += sprintEvent.GetSprintImpact();
                }
            }   

            var simulationResultCommand =
                new SimulationResultCommand(Guid.NewGuid(),
                DateTime.Now, _numberOfDeliveredPoints, _numberOfPlannedPoints, 
                _numberOfPlannedSprints,_numberOfExecutedSprints, 
                numberOfDeveloperEvents, developerEventsImpact,
                numberOfScopeEvents, scopeEventsImpact,
                simulatorName);

            await _simulationResultCommandHandler.AddEntity(simulationResultCommand);
        }

        protected async Task RunPlannedSprints(bool withEvents)
        {
            var enumeratorStories = _projectCommand.Stories?.GetEnumerator();
            var enumeratorSprints = _projectCommand.Sprints?.GetEnumerator();

            while (enumeratorSprints != null && enumeratorSprints.MoveNext())
            {
                var sprintCommand = enumeratorSprints.Current;
                if (sprintCommand == null)
                {
                    break;
                }

                await HandleSprint(sprintCommand, enumeratorStories, withEvents);
                _numberOfExecutedSprints++;
            }
        }

        protected virtual async Task HandleSprint(SprintCommand sprintCommand, IEnumerator<StoryCommand> sEnumerator, bool withEvents = true)
        {

        }

        protected async Task GenerateCommits(StoryCommand storyCommand)
        {
            for (int j = 0; j < storyCommand.PointValue; j++)
            {
                await _commitCommandHandler.AddEntity(new CommitCommand
                {
                    Id = Guid.NewGuid(),
                    StoryId = storyCommand.Id,
                    Description = "Description_" + Guid.NewGuid().ToString(),
                });
            }

        }
    }
}
