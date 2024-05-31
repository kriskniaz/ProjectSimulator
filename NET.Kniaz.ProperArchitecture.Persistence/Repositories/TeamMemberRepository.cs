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
    public class TeamMemberRepository : IEntityRepository<TeamMember>
    {

        DataBaseContext _context;

        public TeamMemberRepository(DataBaseContext context) 
        {
            this._context = context;
        }
        public async Task Add(TeamMember entity)
        {
            await this._context.TeamMembers.AddAsync(entity);
        }

        public async Task<TeamMember> GetFullEntity(Guid id)
        {
            return await this._context.TeamMembers.Include(t => t.Resource).
                Include(tm => tm.Currency).FirstOrDefaultAsync(tm1 => tm1.Id == id);
        }

        public async Task<TeamMember> Get(Guid id)
        {
            return await this._context.TeamMembers.FindAsync(id);
        }

        public async Task<TeamMember> Get(String name)
        {
            return await this._context.TeamMembers.Include(t => t.Resource).
                Include(tm => tm.Currency).FirstOrDefaultAsync(tm1 => tm1.Resource.LastName == name);
        }

        public async Task<List<TeamMember>> GetAll()
        {
            return await _context.TeamMembers.Include(t => t.Resource).
                Include(tm => tm.Currency).ToListAsync();
        }

        public async Task<List<TeamMember>> GetMany(Expression<Func<TeamMember, bool>> filter = null)
        {
            IQueryable<TeamMember> query = this._context.TeamMembers;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public void Remove(TeamMember entity)
        {
            this._context.TeamMembers.Remove(entity);
        }

        public async Task Update(TeamMember entity)
        {
            this._context.TeamMembers.Update(entity);
        }

        public void Detach(TeamMember entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }
    }
}
