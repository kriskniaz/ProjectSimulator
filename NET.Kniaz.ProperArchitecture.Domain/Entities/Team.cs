using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public class Team
    {
        public Team() { }

        public Team(Guid id, string name, string description, 
            Guid projectId)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProjectId = projectId;
            this.TeamMembers = new HashSet<TeamMember>();
        }

        public Team(Guid id, string name, string description, Guid projectId, ICollection<TeamMember> tMembers)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProjectId = projectId;
            this.TeamMembers = tMembers;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProjectId { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set;}
    }
}
