using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Resources.Http.RequestResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Projects;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using API.Helper.Functions;
using API.Helper.Services;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using API.Controllers.Resources.Http.ResponseResources.Photos;
using Microsoft.AspNetCore.Mvc.Rendering;
using API.Core.Domain.Models.Application.Response;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Core.Domain.Application.Request.ProblemBeta;
using Microsoft.EntityFrameworkCore.Query;
using API.Helper.Extension;

namespace API.Persistence.Repository
{
    public class ProblemBetaRepository : Repository<ProblemBeta>, IProblemBetaRepository
    {
        UserManager<User> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IHostingEnvironment _env;
        private readonly IUserRepository _user;
        private readonly Cloudinary _cloudinary;

        private readonly string _path;

        public ProblemBetaRepository(
            DataContext context,  
            UserManager<User> userManager, 
            IOptions<CloudinarySettings> cloudinaryConfig,
            IHostingEnvironment env,
            IUserRepository User
            )
            : base(context)
        {
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;
            _env = env;
            _user = User;
            var Account = new Account(
                _cloudinaryConfig.Value.Name,
                _cloudinaryConfig.Value.Key,
                _cloudinaryConfig.Value.Secret
            );

            _cloudinary = new Cloudinary(Account);

            _path = env.IsDevelopment() ? "dev" : "prod";
        }

        public async Task<User> saveProblemBeta(ProblemBeta problemBeta, CreateProblemBetaRequest createProblemBetaRequest) {

            var user = await _user.SaveUser(createProblemBetaRequest);

            if(user != null && createProblemBetaRequest.AddProblemToUser)
                problemBeta.User = user;

             if(user != null && createProblemBetaRequest.addProjectToProblem) {
                var Project = new Project() {
                    User = user,
                    WebLink = createProblemBetaRequest.ProjectLink
                };

                problemBeta.Projects.Add(Project);
            }

            if (createProblemBetaRequest.RealImages.Count > 0)
                SaveImages(problemBeta, createProblemBetaRequest);
            

            await _context.AddAsync(problemBeta);

            return user;
        }

        private void SaveImages(ProblemBeta problemBeta, CreateProblemBetaRequest createProblemBetaRequest) {

                
            var ImageUploadResult = new ImageUploadResult();

            var listOfResult = new List<ImageUploadResult>();

            // using(var stream = new MemoryStream()) {
                // var file = System.IO.Path.Combine(webRoot, "image.png");

            createProblemBetaRequest.RealImages.ForEach(byteImage => {

                using(var stream = new MemoryStream(byteImage)){

                        var UploadParams = new ImageUploadParams() {
                        File = new FileDescription(Guid.NewGuid().ToString(),stream),
                        Transformation = new Transformation().Width(750).Height(750).Crop("fill").Quality(50),
                        Format = "jpg",
                        Folder = _path
                    };

                    listOfResult.Add(_cloudinary.Upload(UploadParams));
                }

            });
            
            listOfResult.ForEach(result=> {
                if (result.PublicId != null) {
                    problemBeta.Photos.Add( new Photo() {
                        PublicId = result.PublicId,
                        Url = result.Uri.AbsoluteUri
                    });
                }
            });

        }

        


        public async Task<IEnumerable<ProblemCardRawData>> GetProblemBetaPublic() {

            var query = getProblemBetDefaultQuery();
            // return query;
            return await query.OrderBy(r => r.Comments.Count)
                            .Take(10)
                            .Select(s => new ProblemCardRawData() {
                                Problem = s,
                                Likes = s.Likes.Count,
                                Comments = s.Comments.Count,
                                Ideas = s.Ideas.Count
                            })
                            .AsQueryable()
                            .ToListAsync();
        }

        public async Task<QueryResult<ProblemCardRawData>> ProblemBasedOn(ProblemBetaFilter filter ) {

            var result = new QueryResult<ProblemCardRawData>();
            var query = getProblemBetDefaultQuery()
                            .OrderByDescending(prop => prop.Likes.Count).AsQueryable();


            // Filter query -------------- Use this method for the time being
            query = FilterProblemBeta(query, filter);

            // Get Total items
            result.TotalItems = await query.CountAsync();

            // Pagination
            query = query.AddPagination(filter);


            result.Items = await query.Select(s => new ProblemCardRawData() {
                Problem = s,
                Likes = s.Likes.Count,
                Comments = s.Comments.Count,
                Ideas = s.Ideas.Count
            }).ToListAsync();

            return result;
        }

        private IQueryable<ProblemBeta> FilterProblemBeta (IQueryable<ProblemBeta> query, ProblemBetaFilter filter) {

            if(filter.CountryId.HasValue)
                query = query.Where(prop => prop.CountryId == filter.CountryId.Value);

            if(filter.CountryId.HasValue && filter.StateId.HasValue)
                query = query.Where(prop => prop.CountryId == filter.CountryId.Value && prop.StateId == filter.StateId.Value);

            if(!String.IsNullOrEmpty(filter.Name))
                query = query.Where(
                        prop => prop.User.PersonalDetail.FirstName.ToUpper().Contains(filter.Name.ToUpper()) || 
                        prop.User.PersonalDetail.LastName.ToUpper().Contains(filter.Name.ToUpper()));

            if(!String.IsNullOrEmpty(filter.Eco))
                query = query.Where(prop => prop.Eco.ToUpper() == filter.Eco.ToUpper());

            if(!String.IsNullOrEmpty(filter.Ico))
                query = query.Where(prop => prop.Ico.ToUpper() == filter.Ico.ToUpper());
            
            if(!String.IsNullOrEmpty(filter.EcoUn))
                query = query.Where(prop => prop.EcoUn.ToLower() == filter.EcoUn.ToLower());

            return query;
        }

        private IIncludableQueryable<ProblemBeta, PersonalDetail> getProblemBetDefaultQuery() {
            return _context.ProblemBeta
                    .Include(prop => prop.Country)
                    .Include(prop => prop.State)
                    .Include(prop => prop.Photos)
                    .Include(prop => prop.Projects)
                    // .Include(prop => prop.Comments)
                        // .ThenInclude(prop => prop.User)
                        // .ThenInclude(user => user.PersonalDetail)
                    // .Include(prop => prop.Ideas)
                    .Include(prop => prop.User)
                        .ThenInclude(user => user.PersonalDetail);
                        // .ThenInclude(prop => prop.User)
                        // .ThenInclude(user => user.PersonalDetail);
                            
                            
        }

        public object CountIco() {
            // var result = await _context.ProblemBeta.CountAsync()
                            // .Select(s => new {
                            //     i = s.Ico.Where(i => i == 'i').Count(),
                            //     o = s.Ico.Where(i => i == 'o').Count(),
                            //     c = s.Ico.Where(i => i == 'c').Count(),
                            // })
                            // .ToListAsync();
                // var result = from i in _context.ProblemBeta
                //              from o in _context.ProblemBeta
                //              from c in _context.ProblemBeta
                //              where i.Ico == "i" 
                //              where o.Ico == "o"
                //              where c.Ico == "c"
                //              select new {
                //                  i = i,
                //                  o = o,
                //                  c = c
                //              };


                return 0;
        }
    }
}