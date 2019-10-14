using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Domain.Application.Request.Idea;
using API.Core.Domain.Models;
using API.Core.Interfaces;
using API.Helper.Functions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using CloudinaryDotNet.Actions;
using System.IO;
using System;
using API.Controllers.Resources.Http.ResponseResources.IdeaPost;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using API.Core.Domain.Models.Application.Response;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Helper.Extension;

namespace API.Persistence.Repository
{
    public class IdeaPostRepository : Repository<IdeaPost>, IIdeaPostRepository
    {
        UserManager<User> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IUserRepository _user;
        private readonly Cloudinary _cloudinary;
        private readonly string _path;
        public IdeaPostRepository(
            DataContext context,
            UserManager<User> userManager,
            IHostingEnvironment env,
            IUserRepository User
        ) : base(context)
        {
            _userManager = userManager;
            _env = env;
            _user = User;
            var Account = new Account(
                AppEnvironmentVariables.CloudinaryDetails.Name,
                AppEnvironmentVariables.CloudinaryDetails.Key,
                AppEnvironmentVariables.CloudinaryDetails.Secret
            );

            _cloudinary = new Cloudinary(Account);

            _path = env.IsDevelopment() ? "dev" : "prod";
        }

        public async Task<User> SaveIdeaPost(IdeaPost ideaPost, CreateIdeaPostRequest createIdeaPostRequest) {

            var personalDetails = new PersonalDetail() {
                FirstName = createIdeaPostRequest.FirstName,
                LastName = createIdeaPostRequest.LastName
            };

            var user = await _user.SaveUser(createIdeaPostRequest.Email, personalDetails);

            if (!string.IsNullOrEmpty(createIdeaPostRequest.ProjectLink)) {
                ideaPost.Projects.Add(new Project() {
                    User = user,
                    WebLink = createIdeaPostRequest.ProjectLink
                });
            }

            if(createIdeaPostRequest.RealImages.Count > 0) {}
                // return;

            await _context.AddAsync(ideaPost);

            return user;
        }


        private void SaveImages(IdeaPost ideaPost, CreateIdeaPostRequest createProblemBetaRequest) {

            var listOfResult = new List<ImageUploadResult>();


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
                    ideaPost.Photos.Add( new Photo() {
                        PublicId = result.PublicId,
                        Url = result.Uri.AbsoluteUri
                    });
                }
            });

        }

        public async Task<IEnumerable<IdeaPostCardRawData>> GetIdeaPostPublic() {

            var query = getIdeaPostQuery();
            // return query;
            return await query.OrderBy(r => r.Comments.Count)
                            .Take(10)
                            .Select(s => new IdeaPostCardRawData() {
                                IdeaPost = s,
                                Likes = s.Likes.Count,
                                Comments = s.Comments.Count,
                                Ideas = s.Ideas.Count
                            })
                            .AsQueryable()
                            .ToListAsync();
        }

        private IIncludableQueryable<IdeaPost, PersonalDetail> getIdeaPostQuery() {
            return _context.IdeaPosts
                .Include(prop => prop.Country)
                .Include(prop => prop.State)
                .Include(prop => prop.Photos)
                .Include(prop => prop.Projects)
                .Include(prop => prop.User)
                    .ThenInclude(user => user.PersonalDetail);
        }

        public async Task<QueryResult<IdeaPostCardRawData>> IdeaPostBasedOn(SearchPostFilter filter ) {
            var result = new QueryResult<IdeaPostCardRawData>();

            var query = getIdeaPostQuery().OrderByDescending(prop => prop.Likes.Count).AsQueryable();

            query = FilterIdeaPost(query, filter);

            result.TotalItems = await query.CountAsync();

            query = query.AddPagination(filter);

            result.Items = await query.Select(s => new IdeaPostCardRawData() {
                IdeaPost = s,
                Likes = s.Likes.Count,
                Comments = s.Comments.Count,
                Ideas = s.Ideas.Count
            }).ToListAsync();

            return result;

        }

        private IQueryable<IdeaPost> FilterIdeaPost (IQueryable<IdeaPost> query, SearchPostFilter filter) {
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
    }
}