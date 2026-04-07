using NET.Kniaz.ProperArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NET.Kniaz.ProperArchitecture.Domain.Abstractions
{
    public interface IEntityRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> Get(Guid id);

        Task<TEntity>GetFullEntity(Guid id);

        Task<TEntity> Get(String name);

        Task Add(TEntity entity);

        Task Update(TEntity entity);

        void Remove(TEntity entity);

    }
}
