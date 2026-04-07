using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class CommitRepository : IEntityRepository<Commit>
    {
        DataBaseContext _context;
        public CommitRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(Commit entity)
        {
            await _context.Commits.AddAsync(entity);
        }

        public async Task<Commit> Get(Guid id)
        {
            return await _context.Commits.FindAsync(id);
        }

        public async Task<Commit> GetFullEntity(Guid id)
        {
            return await _context.Commits.FindAsync(id);
        }

        public async Task<Commit> Get(String name)
        {
            return await _context.Commits.FirstOrDefaultAsync(c => c.Description.StartsWith(name));
        }   

        public async Task<List<Commit>> GetAll()
        {
            return await _context.Commits.ToListAsync();
        }

        public async Task<List<Commit>> GetMany(Expression<Func<Commit, bool>> filter = null)
        {
            IQueryable<Commit> query = _context.Commits;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        public void Remove(Commit entity)
        {
            this._context.Commits.Remove(entity);
        }

        public async Task Update(Commit entity)
        {
            this._context.Commits.Update(entity);
        }

        public void Detach(Commit entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }
    }
}
