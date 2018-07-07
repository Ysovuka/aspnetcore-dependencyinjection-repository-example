using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public static class RepositoryFactory
    {
        public static IRepository Create<TDbContext>(Func<TDbContext> factory, IRepositoryOptions options)
            where TDbContext : DbContext
        {
            return new Repository<TDbContext>(factory(), options);
        }
    }

}
