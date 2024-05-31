using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;

namespace NET.Kniaz.ProperArchitecture.Simulator
{
    public class Simulation : IConsoleApplication
    {
        private readonly ICommandHandler<ProjectCommand> _projectCommandHandler;
        private readonly ICommandHandler<TeamCommand> _teamCommandHandler;
        private readonly ICommandHandler<TeamMemberCommand> _teamMemberCommandHandler;
        private readonly ICommandHandler<CommitCommand> _commitCommandHandler;
        private readonly ICommandHandler<StoryCommand> _storyCommandHandler;
        private readonly ICommandHandler<SprintCommand> _sprintCommandHandler;
        private readonly ICommandHandler<ResourceCommand> _resourceCommandHandler;
        private readonly ICommandHandler<CurrencyCommand> _currencyCommandHandler;
        private readonly ICommandHandler<SimulationResultCommand> _simulationResultCommandHandler;
        private readonly IUnitOfWork _unitOfWork;


        public Simulation(IUnitOfWork unitOfWork, ICommandHandler<ProjectCommand> projectCommandHandler,
            ICommandHandler<TeamCommand> teamCommandHandler,
            ICommandHandler<TeamMemberCommand> teamMemberCommandHandler,
            ICommandHandler<CommitCommand> commitCommandHandler,
            ICommandHandler<StoryCommand> storyCommandHandler,
            ICommandHandler<SprintCommand> sprintCommandHandler,
            ICommandHandler<ResourceCommand> resourceCommandHandler,
            ICommandHandler<CurrencyCommand> currencyCommand,
            ICommandHandler<SimulationResultCommand> simuationResultHandler)
        {
            _projectCommandHandler = projectCommandHandler;
            _teamCommandHandler = teamCommandHandler;
            _teamMemberCommandHandler = teamMemberCommandHandler;
            _commitCommandHandler = commitCommandHandler;
            _storyCommandHandler = storyCommandHandler;
            _sprintCommandHandler = sprintCommandHandler;
            _resourceCommandHandler = resourceCommandHandler;
            _currencyCommandHandler = currencyCommand;
            _simulationResultCommandHandler = simuationResultHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task Run()
        {
            Preparer preparer = new Preparer(this._projectCommandHandler,
                this._teamCommandHandler,
                this._teamMemberCommandHandler,
                this._commitCommandHandler,
                this._storyCommandHandler,
                this._sprintCommandHandler,
                this._resourceCommandHandler,
                this._currencyCommandHandler);
                
            await preparer.OrchestrateData(); 
            int rnd = DateTime.Now.Millisecond % 2;
            if(rnd>0)
            {
                IdealSimulator simulator = new IdealSimulator(preparer.ProjectCommand, 
                                                  this._projectCommandHandler,
                                                  this._teamCommandHandler,
                                                  this._teamMemberCommandHandler,
                                                  this._commitCommandHandler,
                                                  this._storyCommandHandler,
                                                  this._sprintCommandHandler,
                                                  this._resourceCommandHandler,
                                                  this._currencyCommandHandler,
                                                  this._simulationResultCommandHandler);

                await simulator.Run();
            }
            else
            {
                PoissonSimulator simulator = new PoissonSimulator(preparer.ProjectCommand, 
                                                  this._projectCommandHandler,
                                                  this._teamCommandHandler,
                                                  this._teamMemberCommandHandler,
                                                  this._commitCommandHandler,
                                                  this._storyCommandHandler,
                                                  this._sprintCommandHandler,
                                                  this._resourceCommandHandler,
                                                  this._currencyCommandHandler,
                                                  this._simulationResultCommandHandler);

                await simulator.Run();
            }   

        }
    }
}
