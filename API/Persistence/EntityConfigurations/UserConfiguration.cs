using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasMany(u => u.Projects)
                    .WithOne(p => p.User)
                    .OnDelete(DeleteBehavior.Cascade);
            
            // builder.HasMany(u => u.Problems)
            //         .WithOne(p => p.User)
            //         .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.ProblemsBeta)
                    .WithOne(p => p.User)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.Property(u => u.AgreeToTermsAndCondition)
                .HasDefaultValue(true);
        }
    }
}