using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public sealed class Sprint
    {
        public Sprint() { }
        public Sprint(Guid id, int startWeek, int endWeek, 
            Guid projectId, Guid teamId, int committedPoints, 
            int deliveredPoints)
        {
            Id = id;
            StartWeek = startWeek;
            EndWeek = endWeek;
            ProjectId = projectId;
            TeamId = teamId;
            CommittedPoints = committedPoints;
            DeliveredPoints = deliveredPoints;
        }
        public Guid Id { get; set; }
        public int StartWeek {  get; set; }
        public int EndWeek { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TeamId { get; set; }
        public int CommittedPoints { get; set; }
        public int DeliveredPoints { get; set; }
        public ICollection<Story> Stories { get; set; }

    }
}
