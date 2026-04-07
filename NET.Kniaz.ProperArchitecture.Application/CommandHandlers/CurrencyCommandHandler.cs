using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;
using NET.Kniaz.ProperArchitecture.Persistence.Repositories;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class CurrencyCommandHandler : GenericCommandHandler, ICommandHandler<CurrencyCommand>
    {
        private IEntityRepository<Currency> _currencyRepository;

        public CurrencyCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _currencyRepository = unitOfWork.CurrencyRepository;
        }

        public async Task AddEntity(ICommand<CurrencyCommand> command)
        {
            await _currencyRepository.Add(EntitiesCommandsMapper.MapToCurrency(command));
            await CommitAsync();
        }

        public async Task UpdateEntity(ICommand<CurrencyCommand> command)
        {
            await _currencyRepository.Update(EntitiesCommandsMapper.MapToCurrency(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<CurrencyCommand> command)
        {
            _currencyRepository.Remove(EntitiesCommandsMapper.MapToCurrency(command));
            Commit();
        }
        public async Task<CurrencyCommand> GetEntityAsync(Guid id)
        {
            CurrencyCommand currencyCommand = new CurrencyCommand();

            Currency currency = await _currencyRepository.Get(id);
            
            if (currency!=null)
            {
                currencyCommand = EntitiesCommandsMapper.MapToCurrencyCommand(currency);
            }

            return currencyCommand;
        }

        public async Task<CurrencyCommand> GetFullEntityAsync(Guid id)
        {
            CurrencyCommand currencyCommand = new CurrencyCommand();

            Currency currency = await _currencyRepository.GetFullEntity(id);
            
            if (currency!=null)
            {
                currencyCommand = EntitiesCommandsMapper.MapToCurrencyCommand(currency);
            }

            return currencyCommand;
        }

        public async Task<CurrencyCommand> GetEntityAsync(String name)
        {
            CurrencyCommand currencyCommand = new CurrencyCommand();

            Currency currency = await _currencyRepository.Get(name);
            
            if (currency!=null)
            {
                currencyCommand = EntitiesCommandsMapper.MapToCurrencyCommand(currency);
            }

            return currencyCommand;
        }

        public async Task<List<CurrencyCommand>> GetEntitiesAsync()
        {
            var currencies = await _currencyRepository.GetAll();
            List<CurrencyCommand> result = new List<CurrencyCommand>();

            if (currencies!=null)
            {
                result = currencies.Select(currency => EntitiesCommandsMapper.MapToCurrencyCommand(currency) ).ToList();
            }

            return result;
        }

        public async Task<List<CurrencyCommand>> GetManyEntitiesAsync(int selector)
        {
            Expression<Func<Currency, bool>> filter = null;
            var currencies = await _currencyRepository.GetMany(filter);
            List<CurrencyCommand> result = new List<CurrencyCommand>();

            if (currencies != null)
            {
                result = currencies.Select(currency => EntitiesCommandsMapper.MapToCurrencyCommand(currency)).ToList();
            }

            return result;
        }

    }
}
