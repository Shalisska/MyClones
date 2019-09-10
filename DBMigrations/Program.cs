using System;
using DBMigrations.Migrations;

using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;

namespace DBMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    //.WithGlobalConnectionString("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\projects\\MyClonesData\\MyClones.mdf;Integrated Security=True;Connect Timeout=30")
                    .WithGlobalConnectionString("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyClones;Integrated Security=True;")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddFieldsTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
