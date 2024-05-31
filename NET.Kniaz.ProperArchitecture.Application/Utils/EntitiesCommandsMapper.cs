using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NET.Kniaz.ProperArchitecture.Application.Utils
{
    public static class EntitiesCommandsMapper
    {
        public static CurrencyCommand MapToCurrencyCommand(Currency aCurrency)
        {
            return new CurrencyCommand(aCurrency.Id,
                aCurrency.Name,
                aCurrency.ShortName,
                aCurrency.ValueinUSD);
        }

        public static Currency MapToCurrency(ICommand<CurrencyCommand> command)
        {
            CurrencyCommand aCurrency = command as CurrencyCommand;
            return new Currency(aCurrency.Id,
                aCurrency.Name,
                aCurrency.ShortName,
                aCurrency.ValueinUSD);
        }

        public static ResourceCommand MapToResourceCommand(Resource aResource) 
        {
            return new ResourceCommand(aResource.Id,
                aResource.FirstName, aResource.LastName, aResource.Title);
        }

        public static Resource MapToResource(ICommand<ResourceCommand> command)
        {
            ResourceCommand resourceCommand = command as ResourceCommand;
            return new Resource(resourceCommand.Id, 
                resourceCommand.FirstName,
                resourceCommand.LastName,
                resourceCommand.Title);
        }

        public static TeamMemberCommand MapToTeamMemberCommand(TeamMember aTeamMember) 
        {
            return new TeamMemberCommand(aTeamMember.Id,
                aTeamMember.ResourceId,
                aTeamMember.TeamId,
                aTeamMember.CurrencyId,
                aTeamMember.HourlyRate,
                aTeamMember.StartWeek,
                aTeamMember.EndWeek,
                aTeamMember.StartYear,
                aTeamMember.EndYear,
                MapToResourceCommand(aTeamMember.Resource),
                MapToCurrencyCommand(aTeamMember.Currency)
                );
        }

        public static TeamMember MapToTeamMember(ICommand<TeamMemberCommand> command)
        {
            TeamMemberCommand teamMemberCommand = command as TeamMemberCommand;
            return new TeamMember(
                teamMemberCommand.Id,
                teamMemberCommand.ResourceId,
                teamMemberCommand.TeamId,
                teamMemberCommand.CurrencyId,
                teamMemberCommand.HourlyRate,
                teamMemberCommand.StartWeek,
                teamMemberCommand.EndWeek,
                teamMemberCommand.StartYear,
                teamMemberCommand.EndYear);
        }

        public static TeamCommand MapToTeamCommand(Team team)
        {
            if (team.TeamMembers == null)
            {
                team.TeamMembers = new List<TeamMember>();
            }
            return new TeamCommand(
                team.Id,
                team.Name,
                team.Description,
                team.ProjectId,
                team.TeamMembers.Select(t => MapToTeamMemberCommand(t)).ToList()) ;
        }

        public static Team MapToTeam(ICommand<TeamCommand> command)
        {
            TeamCommand aTeamCommand = command as TeamCommand;
            return new Team(
                aTeamCommand.Id,
                aTeamCommand.Name,
                aTeamCommand.Description,
                aTeamCommand.ProjectId);
        }

        public static ProjectCommand MapToProjectCommand(Project project) 
        {
            if (project.Teams == null)
            {
                project.Teams = new List<Team>();
            }

            if (project.Sprints == null)
            {
                project.Sprints = new List<Sprint>();
            }

            if (project.Stories == null)
            {
                project.Stories = new List<Story>();
            }
            return new ProjectCommand(
                project.Id,
                project.Name,
                project.Description,
                project.StartWeek,
                project.EndWeek,
                project.StartYear,
                project.EndYear,
                project.Teams.Select(t => MapToTeamCommand(t)).ToList(),
                project.Sprints.Select(t1=>MapToSprintCommand(t1)).ToList(),
                project.Stories.Select(t2=>MapToStoryCommand(t2)).ToList()
                );
        }

        public static Project MaptoProject(ICommand<ProjectCommand> command)
        {
            ProjectCommand project = command as ProjectCommand;
            return new Project(
                project.Id,
                project.Name,
                project.Description,
                project.StartWeek,
                project.EndWeek,
                project.StartYear,
                project.EndYear
                );
        }

        public static SprintCommand MapToSprintCommand(Sprint sprint)
        {
            if (sprint.Stories == null)
            {
                sprint.Stories = new List<Story>();
            }
            return new SprintCommand(
                               sprint.Id,
                               sprint.StartWeek,
                               sprint.EndWeek,
                               sprint.ProjectId,
                               sprint.TeamId,
                               sprint.CommittedPoints,
                               sprint.DeliveredPoints,
                               sprint.Stories.Select(s => MapToStoryCommand(s)).ToList());
        }

        public static Sprint MapToSprint(ICommand<SprintCommand> command)
        {
            SprintCommand sprintCommand = command as SprintCommand;
            return new Sprint(
                               sprintCommand.Id,
                               sprintCommand.StartWeek,
                               sprintCommand.EndWeek,
                               sprintCommand.ProjectId,
                               sprintCommand.TeamId,
                               sprintCommand.CommitedPoints,
                               sprintCommand.DeliveredPoints);
        }

        public static StoryCommand MapToStoryCommand(Story story)
        {
            if (story.Commits == null)
            {
                story.Commits = new List<Commit>();
            }
            var sCommand = new StoryCommand(
                               story.Id,
                               story.PointValue,
                               story.ProjectId,
                               story.Description,
                               story.IsDone,
                               story.Commits.Select(s=>MapToCommitCommand(s)).ToList());
            if (story.SprintId.HasValue)
            {
                sCommand.SprintId = story.SprintId.Value;
            }

            return sCommand;
        }

        public static Story MapToStory(ICommand<StoryCommand> command)
        {
            StoryCommand storyCommand = command as StoryCommand;
            var story =  new Story(storyCommand.Id,
                             storyCommand.PointValue,
                             storyCommand.ProjectId,
                             storyCommand.Description,
                             storyCommand.IsDone);

            if (storyCommand.SprintId.HasValue)
            {
                story.SprintId = storyCommand.SprintId.Value;
            }

            return story;
        }

        public static CommitCommand MapToCommitCommand(Commit commit)
        {
            return new CommitCommand(
                               commit.Id,
                               commit.StoryId,
                               commit.Description);
        }

        public static Commit MapToCommit(ICommand<CommitCommand> command)
        {
            CommitCommand commitCommand = command as CommitCommand;

            return new Commit(commitCommand.Id,
                              commitCommand.StoryId,
                              commitCommand.Description);
        }

        public static SimulationResult MapToSimulationResult(ICommand<SimulationResultCommand> simulation)
        {
            SimulationResultCommand simulationResultCommand = simulation as SimulationResultCommand;  

            return new SimulationResult(
                simulationResultCommand.Id,
                simulationResultCommand.RunDate,
                simulationResultCommand.DeliveredPoints,
                simulationResultCommand.CommitedPoints,
                simulationResultCommand.NumberOfPlannedSprints,
                simulationResultCommand.NumberOfExecutedSprints,
                simulationResultCommand.NumberOfDeveloperEvents,
                simulationResultCommand.DeveloperEventsImpact,
                simulationResultCommand.NumberOfScopeEvents,
                simulationResultCommand.ScopeEventsImpact,
                simulationResultCommand.SimulatorType);
        }

        public static SimulationResultCommand MapToSimulationResultCommand(SimulationResult simulationResult)
        {
            return new SimulationResultCommand(
                               simulationResult.Id,
                               simulationResult.RunDate,
                               simulationResult.DeliveredPoints,
                               simulationResult.CommitedPoints,
                               simulationResult.NumberOfPlannedSprints,
                               simulationResult.NumberOfExecutedSprints,
                               simulationResult.NumberOfDeveloperEvents,
                               simulationResult.DeveloperEventsImpact,
                               simulationResult.NumberOfScopeEvents,
                               simulationResult.ScopeEventsImpact,
                               simulationResult.SimulatorType);
        }


    }

}
