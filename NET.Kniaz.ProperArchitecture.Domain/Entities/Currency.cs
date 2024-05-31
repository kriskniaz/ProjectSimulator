using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public sealed class Currency
    {
        public Currency(Guid id, string name, string shortName, decimal valueinUSD)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
            ValueinUSD = valueinUSD;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public decimal ValueinUSD { get; set; }

    }
}
