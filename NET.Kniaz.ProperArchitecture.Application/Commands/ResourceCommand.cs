using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;


namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class ResourceCommand : ICommand<ResourceCommand>
    {
        public ResourceCommand() { }
        public ResourceCommand(Guid id, string firstName, string lastName, string title) 
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Title = title;
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

    }
}
