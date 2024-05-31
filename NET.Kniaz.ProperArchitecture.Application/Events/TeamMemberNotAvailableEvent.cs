using MathNet.Numerics.Distributions;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Commands;

namespace NET.Kniaz.ProperArchitecture.Application.Events
{
    public class TeamMemberNotAvailableEvent : ISprintEvent
    {
        private int _averageVelocityPerCoder = 10;

        // Store sick leaves as a dictionary where key is the sprint number and value is the number of coders out sick
        private Dictionary<int, int> _sickLeaves;
        private int _numCoders;
        private int _codersAbsent;

        public TeamMemberNotAvailableEvent(Dictionary<int, int> sickLeaves, int numberofCoders)
        {
            _sickLeaves = sickLeaves;
            _numCoders = numberofCoders;
        }

        public int GetSprintImpact()
        {
            return _codersAbsent * _averageVelocityPerCoder;
        }


        // Calculate the velocity for a given sprint
        public int GetSprintVelocity(int sprintNumber)
        {
            _codersAbsent = _sickLeaves.ContainsKey(sprintNumber) ? _sickLeaves[sprintNumber] : 0;
            return (_numCoders - _codersAbsent) * _averageVelocityPerCoder;
        }
    }

}
