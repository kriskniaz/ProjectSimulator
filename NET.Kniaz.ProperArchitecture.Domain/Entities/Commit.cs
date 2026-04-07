using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public sealed class Commit
    {
        public Commit() { }
        public Commit(Guid id, Guid storyId, string description)
        {
            this.Id = id;
            this.StoryId = storyId;
            this.Description = description;
        }
        public Guid Id { get; set; }
        public Guid StoryId { get; set; }
        public string Description { get; set; }
    }
}
