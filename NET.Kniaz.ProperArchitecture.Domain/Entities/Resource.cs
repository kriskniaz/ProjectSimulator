using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public sealed class Resource
    {
        public Resource(Guid id, string firstName, string lastName,
            string title)
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
