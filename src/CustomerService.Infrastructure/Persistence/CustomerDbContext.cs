using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence.Extensions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SharedLib.Domain.Mediator;
using SharedLib.Domain.Messages;

namespace CustomerService.Infrastructure.Persistence
{
    public sealed class CustomerDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(160)");

            foreach(var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) property.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
        }
        public async Task<bool> CommitAsync()
        {
            var success = await SaveChangesAsync() > 0;

            if (success) await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }
}
