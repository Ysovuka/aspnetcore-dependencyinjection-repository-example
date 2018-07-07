using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public interface IRepository : IDisposable
    {
        void Add<T>(T entity) where T : class;
        void Attach<T>(T entity) where T : class;
        ChangeTracker ChangeTracker { get; }
        bool Delete();
        Task<bool> DeleteAsync(CancellationToken cancellationToken = default(CancellationToken));
        T FirstOrDefault<T>(object key) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> inclusion) where T : class;
        Task<T> FirstOrDefaultAsync<T> (Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> inclusion,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;
        void Migrate();
        Task MigrateAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Remove<T>(T entity) where T : class;
        void Save();
        void Update<T>(T entity) where T : class;
        IEnumerable<T> Where<T> (Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> Where<T> (Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> extendQuery) where T : class;
    }

}
