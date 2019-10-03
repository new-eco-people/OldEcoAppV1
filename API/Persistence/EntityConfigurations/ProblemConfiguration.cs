using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.EntityConfigurations
{
    public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> builder)
        {
//     builder.Property(prob => prob.Class)
        //             .IsRequired()
        //             .HasMaxLength(50)
        //             .HasColumnType("varchar");
            
        //     builder.Property(prob => prob.Subclass)
        //             .IsRequired()
        //             .HasMaxLength(50)
        //             .HasColumnType("varchar");
            
            builder.Property(prob => prob.Location)
                    .IsRequired()
                    .HasMaxLength(255);
                //     .HasColumnType("varchar");
            
            builder.Property(prob => prob.Topic)
                    .IsRequired()
                    .HasMaxLength(255);
            
            builder.Property(prob => prob.Description)
                    .IsRequired()
                    .HasMaxLength(3000);

        //     builder.HasMany(prob => prob.Projects)
        //             .WithOne(proj => proj.Problem)
        //             .OnDelete(DeleteBehavior.SetNull);

        //     builder.HasOne(prop => prop.Project)
        //                 .WithOne(proj => proj.Problem)
        //                 .HasForeignKey<Project>(proj => proj.ProblemId)
        //                 .OnDelete(DeleteBehavior.SetNull);
            
        //     builder.HasMany(prob => prob.Ideas)
        //            .WithOne(id => id.Problem)
        //            .HasForeignKey(id => id.ProblemId);
        }
    }
}