using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.EntityConfigurations
{
    public class IdeaConfiguration : IEntityTypeConfiguration<Idea>
    {
         public void Configure(EntityTypeBuilder<Idea> builder)
        {
            builder.Property(prop => prop.Message)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}