using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(prop => prop.Title)
                .IsRequired(false)
                .HasMaxLength(255);
                // .HasColumnType("varchar");
            
            builder.Property(prop => prop.Description)
                .IsRequired(false)
                .HasMaxLength(3000);
                // .HasColumnType("varchar");
        }
    }
}