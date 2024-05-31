using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public class Project
    {
        public Project(Guid id, string name, string description,
            int startWeek, int endWeek, int startYear, int endYear)
        {
            Id = id;
            Name = name;
            Description = description;
            this.StartWeek = startWeek;
            this.EndWeek = endWeek;
            this.StartYear = startYear;
            this.EndYear = endYear;
            this.Teams = new HashSet<Team>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }


        public ICollection<Team> Teams { get; set; }

        public ICollection<Sprint> Sprints { get; set; }

        public ICollection<Story> Stories { get; set; }
    }
}
