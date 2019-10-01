using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.EntityConfigurations
{
    public class ProblemBetaConfiguration : IEntityTypeConfiguration<ProblemBeta>
    {
        public void Configure(EntityTypeBuilder<ProblemBeta> builder)
        {
            builder.Property(prop => prop.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .IsFixedLength();

                // .HasColumnType("varchar");

            builder.Property(prop => prop.Eco)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(prop => prop.EcoUn)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(prop => prop.EcoUnOther)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(prop => prop.Ico)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(prop => prop.IcoOther)
                .IsRequired(false)
                .HasMaxLength(500);

            // builder.HasOne(prop => prop.Country)
            //     .WithMany(country => country.ProblemBetas)
            //     .OnDelete(DeleteBehavior.ClientSetNull);

            // builder.HasOne(prop => prop.State)
            //     .WithMany(state => state.ProblemBetas)
            //     .OnDelete(DeleteBehavior.ClientSetNull);

            // builder.HasOne(prop => prop.User)
            //     .WithMany(prop => prop.ProblemsBeta)
            //     .OnDelete(DeleteBehavior.SetNull);

            // builder.HasMany(prop => prop.Photos)
            //     .WithOne(prop => prop.ProblemBeta)
            //     .OnDelete(DeleteBehavior.Cascade);
        }

    }
}