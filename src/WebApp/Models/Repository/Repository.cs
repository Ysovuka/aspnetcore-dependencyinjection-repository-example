using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class Repository<TDbContext> : IRepository
      where TDbContext : DbContext
    {
        public readonly TDbContext _context;
        public Repository(TDbContext context, IRepositoryOptions options)
        {
            _context = context;

            if (options.Migrate)
            {
                Migrate();
            }
            else if (!options.Migrate
                && !options.UseInMemoryDatabase
                && (context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                Migrate();
            }
        }

        public virtual void Add<T>(T entity) where T : class => _context.Add(entity);

        public virtual void Attach<T>(T entity) where T : class => _context.Attach(entity);

        public ChangeTracker ChangeTracker { get { return _context.ChangeTracker; } }

        public bool Delete() => _context.Database.EnsureDeleted();

        public Task<bool> DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _context.Database.EnsureDeletedAsync(cancellationToken);

        public virtual void Dispose() { _context.Dispose(); }

        public virtual T FirstOrDefault<T>(object key) where T : class => _context.Find<T>(key);

        public virtual T FirstOrDefault<T>(Expression<Func<T, bool>> predicate)
            where T : class => _context.Set<T>().FirstOrDefault(predicate);

        public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        => _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual T FirstOrDefault<T> (Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> inclusion) where T : class
        => inclusion(_context.Set<T>()).FirstOrDefault(predicate);

        public virtual Task<T> FirstOrDefaultAsync<T> (Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> inclusion,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        => inclusion(_context.Set<T>()).FirstOrDefaultAsync(predicate, cancellationToken);

        public void Migrate() => _context.Database.Migrate();

        public Task MigrateAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _context.Database.MigrateAsync(cancellationToken);

        public virtual void Remove<T>(T entity) where T : class => _context.Remove(entity);

        public virtual void Save() => _context.SaveChanges();

        public virtual void Update<T>(T entity) where T : class => _context.Update(entity);

        public virtual IEnumerable<T> Where<T> (Expression<Func<T, bool>> predicate)
            where T : class => _context.Set<T>().Where(predicate).ToList();

        public virtual IEnumerable<T> Where<T> (Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> extendQuery) where T : class
        => extendQuery(_context.Set<T>().Where(predicate)).ToList();
    }

}
