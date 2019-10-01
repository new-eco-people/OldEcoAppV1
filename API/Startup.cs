using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using API.Helper.Extension;
using API.Helper.Validations.Database;
using API.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using API.Core.Interfaces.IServices;
using API.Helper.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var prod = Environment.GetEnvironmentVariable("DefaultConnection");
            var dev = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var connectionString = prod;

            services.AddDbContext<DataContext>(x => x.UseNpgsql(connectionString));
// 
            IdentityBuilder builder = services.AddIdentityCore<User>(opts => {
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.MaxFailedAccessAttempts = 10;
                opts.User.RequireUniqueEmail = true;
            });
            // .AddUserValidator<UniqueEmail<User>>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();

            services.AddAutoMapper();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProblemBetaRepository, ProblemBetaRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IIdeaRepository, IdeaRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();

            //Email Sender
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors();
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.BuildServiceProvider().GetService<DataContext>().Database.Migrate();

            // Add Cloudinary
            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
                app.UseExceptionHandler(IappBuilder => {
                    IappBuilder.Run(async context => {
                        
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {   
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });

                
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.SeedDatabase();

            app.UseDefaultFiles();
                app.UseStaticFiles();
                app.UseMvc(
                    route => {
                    route.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "FallBack", Action = "Index" }
                    );
                }
                );
            
        }
    }
}
