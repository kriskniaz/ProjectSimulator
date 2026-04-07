using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public  class TeamCommand : ICommand<TeamCommand>
    {
        public TeamCommand() { }    
        
        public TeamCommand(Guid id, string name, string description, Guid projectId) 
        { 
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProjectId = projectId;
        }

        public TeamCommand(Guid id, string name, string description, Guid projectId, ICollection<TeamMemberCommand> teamMembers)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProjectId = projectId;
            this.TeamMembers = teamMembers;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProjectId { get; set; }

        public ICollection<TeamMemberCommand> TeamMembers { get; set; }
    }
}
