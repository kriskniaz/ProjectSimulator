using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Application.Abstractions
{
    public interface ICommandHandler<TCommand>
    {
        Task<TCommand> GetEntityAsync(Guid id);

        Task<TCommand> GetFullEntityAsync(Guid id);

        Task<TCommand> GetEntityAsync(string name);

        Task<List<TCommand>> GetEntitiesAsync();

        Task<List<TCommand>> GetManyEntitiesAsync(int selector);

        Task AddEntity(ICommand<TCommand> command);

        void RemoveEntity(ICommand<TCommand> command);

        Task UpdateEntity(ICommand<TCommand> command);

    }
}
