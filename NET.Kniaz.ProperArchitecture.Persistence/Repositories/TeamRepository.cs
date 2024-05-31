using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class TeamRepository : IEntityRepository<Team>
    {
        DataBaseContext _context;
        public TeamRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(Team team)
        {
            _context.Teams.Add(team);
        }

        public async Task Update(Team team)
        {
            _context.Teams.Update(team);
        }

        public async Task<List<Team>> GetAll()
        {
            return await _context.Teams.Include(t=>t.TeamMembers).
                ThenInclude(tm=>tm.Currency).
                Include(t=>t.TeamMembers).Include(p=>p.TeamMembers).
                ThenInclude(q=>q.Resource).ToListAsync();
        }

        public async Task<List<Team>> GetMany(Expression<Func<Team, bool>> filter = null)
        {
            IQueryable<Team> query = _context.Teams;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Team> Get(Guid id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> GetFullEntity(Guid id)
        {
            return await _context.Teams.Include(t=>t.TeamMembers).
                ThenInclude(tm => tm.Currency).
                Include(t => t.TeamMembers).Include(p => p.TeamMembers).
                ThenInclude(q => q.Resource).FirstOrDefaultAsync(tm1 => tm1.Id == id);
        }

        public async Task<Team> Get(String name)
        {
            return await _context.Teams.Include(t => t.TeamMembers).
                ThenInclude(tm => tm.Currency).
                Include(t => t.TeamMembers).Include(p => p.TeamMembers).
                ThenInclude(q => q.Resource).FirstOrDefaultAsync(tm1 => tm1.Name == name);
        }

        public void Remove(Team team)
        {
            _context.Teams.Remove(team);
        }

        public void Detach(Team team)
        {
            _context.Entry(team).State = EntityState.Detached;
        }
    }
}
