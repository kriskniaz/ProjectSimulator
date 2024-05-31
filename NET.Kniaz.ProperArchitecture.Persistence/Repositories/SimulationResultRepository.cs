using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class SimulationResultRepository : IEntityRepository<SimulationResult>
    {
        DataBaseContext _context;

        public SimulationResultRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(SimulationResult entity)
        {
            this._context.SimulationResults.Add(entity);
        }

        public async Task<SimulationResult> Get(Guid id)
        {
            return await this._context.SimulationResults.FindAsync(id);
        }

        public async Task<SimulationResult> Get(string name)
        {
            return await this._context.SimulationResults.FirstOrDefaultAsync(s => s.RunDate.ToShortDateString() == name);
        }

        public async Task<List<SimulationResult>> GetAll()
        {
            return await this._context.SimulationResults.ToListAsync();
        }

        public async Task<List<SimulationResult>> GetMany(Expression<Func<SimulationResult, bool>> filter = null)
        {
            IQueryable<SimulationResult> query = this._context.SimulationResults;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<SimulationResult> GetFullEntity(Guid id)
        {
            return await this._context.SimulationResults.FindAsync(id);
        }

        public void Remove(SimulationResult entity)
        {
            this._context.SimulationResults.Remove(entity);
        }

        public async Task Update(SimulationResult entity)
        {
            this._context.SimulationResults.Update(entity);
        }
    }
}
