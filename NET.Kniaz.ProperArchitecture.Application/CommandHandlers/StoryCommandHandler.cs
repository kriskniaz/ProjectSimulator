using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Utils;

namespace NET.Kniaz.ProperArchitecture.Application.CommandHandlers
{
    public class StoryCommandHandler : GenericCommandHandler, ICommandHandler<StoryCommand>
    {
        IEntityRepository<Story> _storyRepository;

        public StoryCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _storyRepository = unitOfWork.StoryRepository;
        }

        public async Task AddEntity(ICommand<StoryCommand> command)
        {
            await _storyRepository.Add(EntitiesCommandsMapper.MapToStory(command));
            await CommitAsync();
        }

        public async Task UpdateEntity(ICommand<StoryCommand> command)
        {
            await _storyRepository.Update(EntitiesCommandsMapper.MapToStory(command));
            await CommitAsync();
        }

        public void RemoveEntity(ICommand<StoryCommand> command)
        {
            _storyRepository.Remove(EntitiesCommandsMapper.MapToStory(command));
            Commit();
        }

        public async Task<StoryCommand> GetEntityAsync(Guid id)
        {
            StoryCommand storyCommand = new StoryCommand();

            Story story = await _storyRepository.Get(id);

            if (story != null)
            {
                storyCommand = EntitiesCommandsMapper.MapToStoryCommand(story);
            }

            return storyCommand;
        }

        public async Task<StoryCommand> GetFullEntityAsync(Guid id)
        {
            StoryCommand storyCommand = new StoryCommand();

            Story story = await _storyRepository.GetFullEntity(id);

            if (story != null)
            {
                storyCommand = EntitiesCommandsMapper.MapToStoryCommand(story);
            }

            return storyCommand;
        }

        public async Task<StoryCommand> GetEntityAsync(String name)
        {
            StoryCommand storyCommand = new StoryCommand();

            Story story = await _storyRepository.Get(name);

            if (story != null)
            {
                storyCommand = EntitiesCommandsMapper.MapToStoryCommand(story);
            }

            return storyCommand;
        }

        public async Task<List<StoryCommand>> GetEntitiesAsync()
        {
            var stories = await _storyRepository.GetAll();
            List<StoryCommand> result = new List<StoryCommand>();

            if (stories != null)
            {
                result = stories.Select(story => EntitiesCommandsMapper.MapToStoryCommand(story)).ToList();
            }

            return result;
        }

        public Task<List<StoryCommand>> GetManyEntitiesAsync(int selector)
        {
            throw new NotImplementedException();
        }
    }
}
