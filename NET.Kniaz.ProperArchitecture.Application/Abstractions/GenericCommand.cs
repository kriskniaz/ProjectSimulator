using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Application.Abstractions
{
    public class GenericCommand : ICommand<GenericCommand>
    {
        public Guid Id { get; set; }
        public GenericCommand(Guid id) 
        {
            this.Id = id;
        }

    }
}
