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
    public class ResourceRepository : IEntityRepository<Resource>
    {
        DataBaseContext _context;

        public ResourceRepository(DataBaseContext context)
        {
            this._context = context;
        }

        public async Task Add(Resource entity)
        {
            this._context.Resources.Add(entity);
        }

        public async Task<Resource> Get(Guid id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<Resource> Get(String name)
        {
            return await _context.Resources.FirstOrDefaultAsync(r => r.LastName == name);
        }

        public async Task<Resource> GetFullEntity(Guid id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<List<Resource>> GetAll()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<List<Resource>> GetMany(Expression<Func<Resource, bool>> filter = null)
        {
            IQueryable<Resource> query = _context.Resources;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public void Remove(Resource entity)
        {
            this._context.Resources.Remove(entity);
        }

        public async Task Update(Resource entity)
        {
            this._context.Resources.Update(entity);
        }

        public void Detach(Resource entity)
        {
            this._context.Entry(entity).State = EntityState.Detached;
        }
    }
}
