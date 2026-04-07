using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;
using NET.Kniaz.ProperArchitecture.Persistence.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class SimulationResultCommandHandler : GenericCommandHandler, ICommandHandler<SimulationResultCommand>
    {
        private readonly IEntityRepository<SimulationResult> _simulationResultRepository;

        public SimulationResultCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _simulationResultRepository = unitOfWork.SimulationResultRepository;
        }

        public async Task AddEntity(ICommand<SimulationResultCommand> command)
        {
            await this._simulationResultRepository.Add(EntitiesCommandsMapper.MapToSimulationResult(command));
            await CommitAsync();
        }

        public async Task<List<SimulationResultCommand>> GetEntitiesAsync()
        {
            var simulations = await _simulationResultRepository.GetAll();
            List<SimulationResultCommand> result = new List<SimulationResultCommand>();

            if (simulations != null)
            {
                result = simulations.Select(simulation => EntitiesCommandsMapper.MapToSimulationResultCommand(simulation)).ToList();
            }

            return result;
        }

        public async Task<List<SimulationResultCommand>> GetManyEntitiesAsync(int selector)
        {
            throw new NotImplementedException();
        }

        public async Task<SimulationResultCommand> GetEntityAsync(Guid id)
        {
            SimulationResultCommand simulationResultCommand = new SimulationResultCommand();

            SimulationResult simulation = await _simulationResultRepository.Get(id);

            if (simulation != null)
            {
                simulationResultCommand = EntitiesCommandsMapper.MapToSimulationResultCommand(simulation);
            }

            return simulationResultCommand;

        }

        public async Task<SimulationResultCommand> GetEntityAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<SimulationResultCommand> GetFullEntityAsync(Guid id)
        {
            return await GetEntityAsync(id);
        }

        public void RemoveEntity(ICommand<SimulationResultCommand> command)
        {
            _simulationResultRepository.Remove(EntitiesCommandsMapper.MapToSimulationResult(command));
            Commit();
        }

        public async Task UpdateEntity(ICommand<SimulationResultCommand> command)
        {
            await _simulationResultRepository.Update(EntitiesCommandsMapper.MapToSimulationResult(command));
            await CommitAsync();
        }
    }   

}
