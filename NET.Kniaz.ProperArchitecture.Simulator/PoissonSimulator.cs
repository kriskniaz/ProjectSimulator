using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Events;
using NET.Kniaz.ProperArchitecture.Application.Utils;

namespace NET.Kniaz.ProperArchitecture.Simulator
{
    public class PoissonSimulator : GenericSimulator
    {
        private Dictionary<int, int> _sickLeaves;
        private List<StoryCommand> _newScopeList;
        public PoissonSimulator(ProjectCommand projectCommand, 
            ICommandHandler<ProjectCommand> projectCommandHandler,
            ICommandHandler<TeamCommand> teamCommandHandler,
            ICommandHandler<TeamMemberCommand> teamMemberCommandHandler,
            ICommandHandler<CommitCommand> commitCommandHandler,
            ICommandHandler<StoryCommand> storyCommandHandler,
            ICommandHandler<SprintCommand> sprintCommandHandler,
            ICommandHandler<ResourceCommand> resourceCommandHandler,
            ICommandHandler<CurrencyCommand> currencyCommandHandler,
            ICommandHandler<SimulationResultCommand> simulationResultCommandHandler)
            : base(projectCommand,
                   projectCommandHandler,
                   teamCommandHandler,
                   teamMemberCommandHandler,
                   commitCommandHandler,
                   storyCommandHandler,
                   sprintCommandHandler,
                   resourceCommandHandler,
                   currencyCommandHandler,
                   simulationResultCommandHandler)
        { 
            _sickLeaves = new Dictionary<int, int>();
            _newScopeList = new List<StoryCommand>();
            _numberOfExecutedSprints = 0;
        }

        public async Task Run()
        {
            InitializeProjectSickness();
            await RunPlannedSprints(true);
            await RunUnPlannedSprints();
            await SaveStatistics("PoissonSimulator");
        }


        protected async Task RunUnPlannedSprints()
        {
            _newScopeList.AddRange(_projectCommand.Stories.Where(x => x.IsDone == 0).ToList());
            int startweek = _projectCommand.Sprints.LastOrDefault().EndWeek + 1;
            
            while (_newScopeList.Where(x=>x.IsDone==0).Count()>0)
            {
                var enumeratorStories = _newScopeList.Where(x => x.IsDone == 0).ToList().GetEnumerator();
                var sprintCommand = await AddSprint(startweek);
                await HandleSprint(sprintCommand, enumeratorStories, false);
                _numberOfExecutedSprints++;
                startweek += 2;
            }

        }

        private async Task<SprintCommand> AddSprint(int startWeek)
        {
            var sprintCommand = new SprintCommand();
            sprintCommand.Id = Guid.NewGuid();
            sprintCommand.StartWeek = startWeek;
            sprintCommand.EndWeek = startWeek + 1;
            sprintCommand.ProjectId = _projectCommand.Id;
            sprintCommand.TeamId = _projectCommand.Teams.FirstOrDefault().Id;
            sprintCommand.CommitedPoints = 0;
            sprintCommand.DeliveredPoints = 0;
            sprintCommand.StoryCommands = new List<StoryCommand>();

            await _sprintCommandHandler.AddEntity(sprintCommand);
            _projectCommand.Sprints.Add(sprintCommand);

            return sprintCommand;
        }

        protected override async Task HandleSprint(SprintCommand sprintCommand, IEnumerator<StoryCommand> sEnumerator, bool withEvents=true)
        {
            
            int teamMembers = _projectCommand.Teams.FirstOrDefault().TeamMembers.Count;
            int commitedPoints = 0;
            var enumeratorStories = sEnumerator;
            int teamVelocity = teamMembers * Constants.DefaultDeveloperVelocity;
            //making sprint velocity a bit random
            var rnd = new Random();
            int upperBound = (int)Math.Ceiling(teamVelocity * 1.15);
            int randomVelocity = rnd.Next(teamVelocity, upperBound);

            if (withEvents)
            {
                TeamMemberNotAvailableEvent teamMemberNotAvailableEvent = new TeamMemberNotAvailableEvent(_sickLeaves, teamMembers);
                randomVelocity = teamMemberNotAvailableEvent.GetSprintVelocity(sprintCommand.EndWeek / 2);
                _sprintEvents.Add(teamMemberNotAvailableEvent);

                //begining of the sprint we are handling random scope events, poisson distribution with lambda = 7
                NewScopeEvent newScopeEvent = new NewScopeEvent(Constants.MeanScopePoissonValue, sprintCommand);
                StoryCommand newScope = newScopeEvent.HandleNewScope();
                _sprintEvents.Add(newScopeEvent);
                //if the new scope is less or equal to 5 the sprint team will handle it within
                //sprint, otherwise it will be added to the backlog
                if (newScope != null)
                {
                    if (newScope.PointValue <= Constants.DefaultManageableScope)
                    {
                        newScope.IsDone = 1;
                        newScope.SprintId = sprintCommand.Id;
                        sprintCommand.DeliveredPoints = newScope.PointValue;
                        await _storyCommandHandler.AddEntity(newScope);
                        await GenerateCommits(newScope);
                    }
                    else
                    {
                        await _storyCommandHandler.AddEntity(newScope);
                        _newScopeList.Add(newScope);
                    }

                }
            }

            //story loop
            while (((Math.Abs(commitedPoints - randomVelocity) > randomVelocity * 0.1) && (commitedPoints < Constants.MaxSprintVelocity)))
            {
                
                
                enumeratorStories.MoveNext();
                var storyCommand = enumeratorStories.Current;

                if (storyCommand == null)
                {
                    break;
                }

                commitedPoints += storyCommand.PointValue;
                storyCommand.IsDone = 1;
                storyCommand.SprintId = sprintCommand.Id;
                await _storyCommandHandler.UpdateEntity(storyCommand);
                await GenerateCommits(storyCommand);
            }

            sprintCommand.CommitedPoints = commitedPoints;
            sprintCommand.DeliveredPoints += commitedPoints;
            await _sprintCommandHandler.UpdateEntity(sprintCommand);
        }

        /// <summary>
        /// initilize absences
        /// </summary>
        private void InitializeProjectSickness()
        {
            int numCoders = _projectCommand.Teams.FirstOrDefault().TeamMembers.Count;
            int numSprints = _projectCommand.Sprints.Count;
            System.Random random = SystemRandomSource.Default;

            // Assuming lambda (mean number of sick sprints) is about 2 per 26 sprints
            double lambda = Constants.MeanSicknessPoissonValue; // Expected value for Poisson distribution
            for (int i = 1; i <= numCoders; i++)
            {
                int numSickSprints = Poisson.Sample(lambda);
                for (int j = 0; j < numSickSprints; j++)
                {
                    int sickSprint = random.Next(1, numSprints + 1);
                    if (!_sickLeaves.ContainsKey(sickSprint))
                    {
                        _sickLeaves[sickSprint] = 0;
                    }
                    _sickLeaves[sickSprint]++;
                }
            }
        }
    }
}
