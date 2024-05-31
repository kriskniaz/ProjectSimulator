using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Persistence.Repositories
{
    public class CurrencyRepository : IEntityRepository<Currency>
    {
        DataBaseContext _context;
        public CurrencyRepository(DataBaseContext context) 
        { 
            _context = context;
        }
        public async Task Add(Currency currency)
        {
             _context.Currencies.AddAsync(currency);           
        }

        public async Task Update(Currency currency)
        {
             _context.Currencies.Update(currency);
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<List<Currency>> GetMany(System.Linq.Expressions.Expression<Func<Currency, bool>> filter = null)
        {
            IQueryable<Currency> query = _context.Currencies;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Currency> Get(Guid id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency> GetFullEntity(Guid id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency> Get(String name)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.ShortName == name);
        }

        public void Remove(Currency currency)
        {
             _context.Currencies.Remove(currency);
        }

        public void Detach(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Detached;
        }
    }
}
