using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;

namespace NET.Kniaz.ProperArchitecture.Application.Commands
{
    public class TeamMemberCommand : ICommand<TeamMemberCommand>
    {
        public TeamMemberCommand() { }
        
        public TeamMemberCommand(Guid id , Guid resourceId, Guid teamid, Guid currencyId, decimal rate, 
            int sweek, int eweek, int syear, int eyear) 
        { 
            this.Id = id;
            this.ResourceId = resourceId;
            this.TeamId = teamid;
            this.CurrencyId = currencyId;
            this.HourlyRate = rate;
            this.StartWeek = sweek;
            this.EndWeek = eweek;
            this.StartYear = syear;
            this.EndYear = eyear;
        }
        

        public TeamMemberCommand(Guid id, Guid resourceId, Guid teamid, Guid currencyId, decimal rate,
    int sweek, int eweek, int syear, int eyear, ResourceCommand resource, CurrencyCommand currency)
        {
            this.Id = id;
            this.ResourceId = resourceId;
            this.TeamId = teamid;
            this.CurrencyId = currencyId;
            this.HourlyRate = rate;
            this.StartWeek = sweek;
            this.EndWeek = eweek;
            this.StartYear = syear;
            this.EndYear = eyear;
            this.Resource = resource;
            this.Currency = currency;
        }

        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }

        public Guid TeamId { get; set; }

        public decimal HourlyRate { get; set; }

        public Guid CurrencyId { get; set; }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public CurrencyCommand Currency { get; set; }

        public ResourceCommand Resource { get; set; }
    }
}
