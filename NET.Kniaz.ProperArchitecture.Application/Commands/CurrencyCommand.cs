using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class CurrencyCommand : ICommand<CurrencyCommand> 
    {
        public CurrencyCommand() { }
        public CurrencyCommand(Guid Id, string Name, string ShortName, decimal ValueInUSD)
        {
            this.Id = Id;
            this.Name = Name;
            this.ShortName = ShortName;
            this.ValueinUSD = ValueInUSD;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public decimal ValueinUSD { get; set; }

    }
}
