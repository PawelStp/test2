using Games.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer
{
    public abstract class MsSqlServerBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> DbSet;
        private readonly GamesDbContext _dbConext;

        public MsSqlServerBaseRepository(GamesDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
            _dbConext = dbContext;
        }

        protected virtual IQueryable<T> Queryable()
        {
            return DbSet.AsQueryable().AsNoTracking();
        }

        public async Task<T> Get(long id, CancellationToken cancellationToken)
        {
            return await Queryable().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IList<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Queryable().ToListAsync(cancellationToken);
        }

        public async Task<bool> Exists(long id, CancellationToken cancellationToken)
        {
            return await Queryable().AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task Add(T item, CancellationToken cancellationToken)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await DbSet.AddAsync(item, cancellationToken);
            await _dbConext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(T item, CancellationToken cancellationToken)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            DbSet.Update(item);
            await _dbConext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(T item, CancellationToken cancellationToken)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            DbSet.Remove(item);
            await _dbConext.SaveChangesAsync(cancellationToken);
        }
    }
}
