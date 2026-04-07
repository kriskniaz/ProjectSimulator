using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Entities
{
    public class SimulationResult
    {
        public SimulationResult() { }

        public SimulationResult(Guid id, DateTime runDate, 
            int deliveredPoints, int commitedPoints, 
            int numberOfPlannedSprints, int numberOfExecutedSprints,
            int numberOfDeveloperEvents, int developerEventsImpact,
            int numberOfScopeEvents, int scopeEventsImpact,
            string simulatorType)
        {
            Id = id;
            RunDate = runDate;
            DeliveredPoints = deliveredPoints;
            CommitedPoints = commitedPoints;
            NumberOfPlannedSprints = numberOfPlannedSprints;
            NumberOfExecutedSprints = numberOfExecutedSprints;
            NumberOfDeveloperEvents = numberOfDeveloperEvents;
            DeveloperEventsImpact = developerEventsImpact;
            NumberOfScopeEvents = numberOfScopeEvents;
            ScopeEventsImpact = scopeEventsImpact;
            SimulatorType = simulatorType;
        }   

        public Guid Id { get; set; }

        public DateTime RunDate { get; set; }

        public int DeliveredPoints { get; set; }
        
        public int CommitedPoints { get; set; }

        public int NumberOfPlannedSprints { get; set; }

        public int NumberOfExecutedSprints { get; set; }

        public string SimulatorType { get; set; }

        public int NumberOfDeveloperEvents { get; set; }

        public int DeveloperEventsImpact { get; set; }

        public int NumberOfScopeEvents { get; set; }

        public int ScopeEventsImpact { get; set; }  
    }
}
