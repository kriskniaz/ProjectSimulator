using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class SprintRepository : IEntityRepository<Sprint>
    {
        DataBaseContext _context;

        public SprintRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(Sprint entity)
        {
            await this._context.Sprints.AddAsync(entity);
        }

        public async Task<Sprint> Get(Guid id)
        {
            return await this._context.Sprints.FindAsync(id);
        }

        public async Task<Sprint> GetFullEntity(Guid id)
        {
            return await this._context.Sprints.Include(p => p.Id).FirstOrDefaultAsync(tm1 => tm1.Id == id);
        }

        public async Task<Sprint> Get(String name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sprint>> GetAll()
        {
            return await this._context.Sprints.ToListAsync();
        }

        public async Task<List<Sprint>> GetMany(Expression<Func<Sprint, bool>> filter = null)
        {
            IQueryable<Sprint> query = this._context.Sprints;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public void Remove(Sprint entity)
        {
            this._context.Sprints.Remove(entity);
        }

        public async Task Update(Sprint entity)
        {
            var trackedEntity = await this._context.Sprints.FindAsync(entity.Id);
            this._context.Entry(trackedEntity).CurrentValues.SetValues(entity);
        }

        public void Detach(Sprint entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }
    }
}
