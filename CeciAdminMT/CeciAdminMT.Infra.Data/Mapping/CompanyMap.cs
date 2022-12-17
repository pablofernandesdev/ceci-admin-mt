using CeciAdminMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CeciAdminMT.Infra.Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.ApiKey)
                .IsRequired()
                .HasColumnName("ApiKey")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.DocumentNumber)
                .IsRequired()
                .HasColumnName("DocumentNumber")
                .HasColumnType("varchar(14)");
        }
    }
}
