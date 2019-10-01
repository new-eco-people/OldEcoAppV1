using System;
using API.Core.Domain.Models;
using API.Core.Domain.Models.Defaults;
using API.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository
{
    public class DataContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        
        // public DbSet<Project> Projects { get; set; }
        // public DbSet<Problem> Problems { get; set; }
        // public DbSet<Idea> Ideas { get; set; }
        public DbSet<ProblemBeta> ProblemBeta { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Idea> Idea { get; set; }


        public DataContext(DbContextOptions<DataContext> options)
        : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            // builder.ApplyConfiguration(new ProblemConfiguration());
            builder.ApplyConfiguration(new ProblemBetaConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new IdeaConfiguration());

            // builder.Entity<User>(user => {
            //     // user.HasIndex(u => u.PhoneNumber).IsUnique();
            //     // user.HasAlternateKey(u => u.PhoneNumber);
            // });

            base.OnModelCreating(builder);
        }
    }
}