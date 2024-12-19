using CustomerService.Domain.Entities;
using CustomerService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.Persistence.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR(150)");
            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(x => x.Number).IsRequired().HasMaxLength(11).HasColumnName("Cpf").HasColumnType("VARCHAR");
            });

            builder.OwnsOne(x => x.Email, tf =>
            {
                tf.Property(x => x.Address)
                .IsRequired()
                .HasColumnType($"VARCHAR({Email.ADDRESS_MAX_LENGTH})")
                .HasColumnName("Email");
            });

            // 1:1
            builder.HasOne(x => x.Address).WithOne(x => x.Customer);
        }
    }
}
