using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class RepositoryOptions : IRepositoryOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public bool Migrate { get; set; }

        public bool UseInMemoryDatabase { get; set; }
    }
}
