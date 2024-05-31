using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class StoryRepository : IEntityRepository<Story>
    {
        DataBaseContext _context;

        public StoryRepository(DataBaseContext context) 
        {
            _context = context;
        }
        public async Task Add(Story entity)
        {
            await this._context.Stories.AddAsync(entity);
        }

        public async Task<Story> Get(Guid id)
        {
            return await this._context.Stories.FindAsync(id);
        }

        public async Task<Story> GetFullEntity(Guid id)
        {
            return await this._context.Stories.FindAsync(id);
        }

        public async Task<Story> Get(String name)
        {
            return await this._context.Stories.FirstOrDefaultAsync(s => s.Description.StartsWith(name));
        }   
        public async Task<List<Story>> GetAll()
        {
            return await this._context.Stories.ToListAsync();
        }

        public async Task<List<Story>> GetMany(Expression<Func<Story, bool>> filter = null)
        {
            IQueryable<Story> query = _context.Stories;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public void Remove(Story entity)
        {
            this._context.Stories.Remove(entity);
        }

        public async Task Update(Story entity)
        {   
            var trackedEntity = await this._context.Stories.FindAsync(entity.Id);
            this._context.Entry(trackedEntity).CurrentValues.SetValues(entity);
        }


        public void Detach(EntityEntry<Story> entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }

    }
}
