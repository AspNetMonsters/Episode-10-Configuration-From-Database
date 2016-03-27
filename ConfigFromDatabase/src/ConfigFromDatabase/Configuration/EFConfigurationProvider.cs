
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace ConfigFromDatabase.Configuration
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEntityFramework(
            this IConfigurationBuilder builder, 
            Action<DbContextOptionsBuilder> setup)
        {
            return builder.Add(new EFConfigurationProvider(setup));
        }
    }

    public class EFConfigurationProvider 
        : ConfigurationProvider
    {
        public EFConfigurationProvider(
            Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);

            using (var context = new ConfigurationContext(builder.Options))
            {
                context.Database.EnsureCreated();
                Data = !context.Values.Any()
                    ? CreateAndSaveDefaultValues(context)
                    : context.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        private IDictionary<string, string> CreateAndSaveDefaultValues(ConfigurationContext context)
        {
            var configValues = new Dictionary<string, string>
            {
                { "SiteTitle", "It's the Monsters!" },
                { "CurrentPromo", "50% off Monsters Stickers" }
            };

            context.Values.AddRange(configValues
                .Select(entry => new ConfigurationValue() {
                    Id = entry.Key, Value = entry.Value })
                .ToArray());
            context.SaveChanges();

            return configValues;
        }
    }

}