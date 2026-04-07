using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Linq.Expressions;


namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class ProjectRepository : IEntityRepository<Project>
    {
        DataBaseContext _context;
        public ProjectRepository(DataBaseContext context)
        {
            this._context = context;
        }
        public async Task Add(Project  entity)
        {
            this._context.Projects.Add(entity);
        }

        public async Task<Project> Get(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> GetFullEntity(Guid id)
        {
            return await _context.Projects
        .Include(p => p.Teams)
            .ThenInclude(t => t.TeamMembers)
                .ThenInclude(tm => tm.Currency)
        .Include(p => p.Teams)
            .ThenInclude(t => t.TeamMembers)
                .ThenInclude(tm => tm.Resource)
        .Include(p => p.Sprints.OrderBy(k=>k.StartWeek))
            .OrderBy(s => s.StartWeek) // Order by StartWeek
        .Include(p => p.Stories)
        .FirstOrDefaultAsync(p => p.Id == id);
        }

            public async Task<Project> Get(String name)
        {
            return await _context.Projects.Include(p => p.Teams).ThenInclude(t => t.TeamMembers).
                ThenInclude(tm => tm.Currency).
                Include(p1 => p1.Teams).ThenInclude(p => p.TeamMembers).
                ThenInclude(q => q.Resource).FirstOrDefaultAsync(tm1 => tm1.Name == name);
        }

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects.Include(p=>p.Teams).ThenInclude(t => t.TeamMembers).
                ThenInclude(tm => tm.Currency).
                Include(p1 => p1.Teams).ThenInclude(p => p.TeamMembers).
                ThenInclude(q => q.Resource).ToListAsync();
        }

        public async Task<List<Project>> GetMany(Expression<Func<Project, bool>> filter = null)
        {
            IQueryable<Project> query = _context.Projects;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public void Remove(Project entity)
        {
            this._context.Projects.Remove(entity);
        }

        public async Task Update(Project entity)
        {
            this._context.Projects.Update(entity);
        }

        public void Detach(Project entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }
    }
}
