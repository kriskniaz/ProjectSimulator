using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Commands;


namespace NET.Kniaz.ProperArchitecture.Application.Events
{
    public class NewScopeEvent : ISprintEvent
    {
        private List<Tuple<int, double>> _distribution;
        private SprintCommand _sprintCommand;
        private StoryCommand _storyCommand;

        public NewScopeEvent(int scope, SprintCommand sprintCommand)
        {
            Scope = scope;
            _sprintCommand = sprintCommand;
            _distribution = new List<Tuple<int, double>>();
            _distribution = Enumerable.Range(0,13).Select(x => new Tuple<int, double>(x, Poisson.CDF(scope,x))).ToList();
            _storyCommand = new StoryCommand();
            
        }

        public int Scope { get; set; }

        public List<Tuple<int, double>> Distribution
        { get { return this._distribution; } }

        public int GetSprintImpact()
        {
            return _storyCommand.PointValue;
        }

        public  StoryCommand HandleNewScope()
        {
            var scope = GetRandomScopeValue();
            
            _storyCommand = new StoryCommand();
            _storyCommand.Id = Guid.NewGuid();
            _storyCommand.IsDone = 0;
            _storyCommand.Description = "Story_" + Guid.NewGuid().ToString();
            _storyCommand.PointValue = scope;
            _storyCommand.ProjectId = _sprintCommand.ProjectId;
            _storyCommand.CommitCommands = new List<CommitCommand>();

            return _storyCommand;
        }

        private int GetRandomScopeValue()
        {
            //using .NET Random
            //var rand = new Random();
            //var randomValue = rand.NextDouble();
            
            //using MathNet.Numerics.Random;
            System.Random rng = SystemRandomSource.Default;
            var randomValue = rng.NextDouble();

            var value = _distribution.FirstOrDefault(x => x.Item2 >= randomValue);
            if (value == null)
            {
                return _distribution.Last().Item1; // Return the maximum if no higher value is found
            }
            return value.Item1;
        }
    }
}
