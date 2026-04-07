using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public class TeamMember
    {
        public TeamMember() { }
        public TeamMember(Guid id, Guid resourceId, Guid teamId, 
            Guid currencyId, decimal hourlyRate,
            int startWeek, int endWeek, int startYear, int endYear)
        {
            Id = id;
            ResourceId = resourceId;
            TeamId = teamId;
            HourlyRate = hourlyRate;
            CurrencyId = currencyId;
            this.StartWeek = startWeek;
            this.EndWeek = endWeek;
            this.StartYear = startYear;
            this.EndYear = endYear;
        }

        public Guid Id { get; set; }

        public Guid ResourceId {  get; set; }

        public Guid TeamId { get; set; }

        public decimal HourlyRate { get; set; }

        public Guid CurrencyId { get; set; }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public Currency Currency { get; set; }

        public Resource Resource { get; set; }

    }
}
