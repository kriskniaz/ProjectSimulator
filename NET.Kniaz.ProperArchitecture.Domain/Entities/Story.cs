using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public sealed class Story
    {
        public Story() { }
        public Story(Guid id, int pointValue, Guid projectId, string description, int isDone)
        {
            Id = id;
            PointValue = pointValue;
            ProjectId = projectId;
            Description = description;
            IsDone = isDone;
        }
        public Guid Id { get; set; }

        public int PointValue {  get; set; }

        public Guid? SprintId { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }

        public int IsDone { get; set; }
        public ICollection<Commit> Commits { get; set; }
    }
}
