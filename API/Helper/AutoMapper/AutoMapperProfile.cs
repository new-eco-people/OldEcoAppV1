using AutoMapper;
using API.Controllers.Resources.Http.RequestResources.Projects;
using API.Controllers.Resources.Http.RequestResources.Users;
using API.Controllers.Resources.Http.ResponseResources.Projects;
using API.Controllers.Resources.Http.ResponseResources.Users;
using API.Core.Domain.Models;
using API.Controllers.Resources.Http.RequestResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Problems;
using API.Controllers.Resources.Http.ResponseResources.Photos;
using API.Core.Domain.Models.Application.Response;
using API.Core.Domain.Models.Application.Request.ProblemBeta;
using API.Controllers.Resources.Http.ResponseResources.Generic;
using API.Core.Domain.Application.Request.ProblemBeta;
using API.Controllers.Resources.Http.RequestResources.Comments;
using API.Controllers.Resources.Http.ResponseResources.Comments;
using API.Core.Domain.Application.Request.Comment;
using API.Controllers.Resources.Http.RequestResources.Hacks;
using API.Controllers.Resources.Http.RequestResources.Ideas;
using API.Core.Domain.Application.Request.Idea;
using API.Controllers.Resources.Http.ResponseResources.IdeaPost;

namespace API.Helper.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {


            // ------- From Model to Resource
            CreateMap<User, UserResponseResource>();
            CreateMap<Project, ProjectResponseResource>();
            CreateMap<IdeaPost, IdeaPostResponseResource>();
            CreateMap<ProblemBeta, ProblemBetaResponseResource>()
                .ForMember((prob => prob.Description), config => config.MapFrom((cre => cre.Description.Trim())));
            CreateMap<PersonalDetail, PersonalDetailResponseResource>();
            CreateMap<Photo, ProblemPhotosResponseResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResponseResource<>));
            CreateMap<Comment, CommentResponseResource>();
            CreateMap<Idea, CommentResponseResource>();
            



            //------ From Request to Domain

                //From Request to Model
                CreateMap<RegisterRequestResource, User>()
                    .ForMember((user => user.UserName), config => config.MapFrom(reg => reg.Email));
                CreateMap<NewProjectRequestResource, Project>();
                CreateMap<EditProjectRequestResource, Project>();

                //From Request to Application
                CreateMap<CreateProblemBeteRequestResource, CreateProblemBetaRequest>()
                    .ForMember((prob => prob.Description), config => config.MapFrom((cre => cre.Description.Trim())));
                    // .ForMember((prob => prob.EcoUn), config => config.MapFrom((cre => cre.EcoUnOther.ToLower())));
                CreateMap<SearchPostRequestResource, SearchPostFilter>();
                CreateMap<SearchPostRequestResource, CommentFilter>();

                CreateMap<CreateCommentRequestResource, Comment>();
                CreateMap<CreateCommentRequestResource, Idea>();
                CreateMap<CreateCommentWithIdRequestResource, Comment>();
                CreateMap<CreateCommentWithIdRequestResource, Idea>();

                CreateMap<CreateIdeaRequestResource, CreateIdeaPostRequest>()
                    .ForMember((prob => prob.Description), config => config.MapFrom((cre => cre.Description.Trim())));




            //------- From Response to Response
            CreateMap<ProblemCardRawData, ProblemCardBetaResponseResource>();
            CreateMap<IdeaPostCardRawData, IdeaPostCardResponseResource>();

                // Hacks
                CreateMap<ProblemCardRawData, ProblemHacksCardResponseResource>();
                CreateMap<ProblemBeta, ProblemHacksResponseResource>();
                CreateMap<User, UserHacksResponseResource>();
                CreateMap<Comment, CommentHacksResponseResource>();
                CreateMap<Idea, CommentHacksResponseResource>();

            //--------From Application to Model
            CreateMap<CreateProblemBetaRequest, ProblemBeta>();
            CreateMap<CreateIdeaPostRequest, IdeaPost>();

            //--------From Application to Resource

        }
    }
}