using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigFromDatabase.Configuration
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ConfigurationValue> Values { get; set; }

    }

}