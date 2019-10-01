using System.Threading.Tasks;
using API.Core.Interfaces;
using API.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using API.Core.Domain.Application.Request.ProblemBeta;
using Microsoft.AspNetCore.Identity;
using System;
using API.Helper.Functions;

namespace API.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, UserManager<User> userManager)
            :base(context)
        {
            _userManager = userManager;
        }

        public async Task<User> SaveUser(string userEmail, PersonalDetail personalDetail = null) {
            var user = await _context.Users.Include(u => u.PersonalDetail).FirstOrDefaultAsync(u => u.Email.ToUpper() == userEmail.ToUpper());

            if (user != null) {
                if (personalDetail != null && user.PersonalDetail == null) {
                    user.PersonalDetail = personalDetail;
                }
                
                return user;
            } else {

                user = new User() {
                    Email = userEmail,
                    UserName = userEmail
                };

                if (personalDetail != null) {
                    user.PersonalDetail = personalDetail;
                }

                var result = await _userManager.CreateAsync(user,UtilityFunctions.GenerateRandomPassword());

                if(!result.Succeeded)
                    throw new Exception("It seems we are having some issue creating your account");

                return user;

            }
            
        }

        public async Task<User> SaveUser(CreateProblemBetaRequest createProblemBetaRequest) {
            if (string.IsNullOrWhiteSpace(createProblemBetaRequest.Email))
                return null;

            return await SaveUser(createProblemBetaRequest.Email, new PersonalDetail() {
                FirstName = createProblemBetaRequest.FirstName,
                LastName = createProblemBetaRequest.LastName
            });
        }

        public async Task<User> GetWithPersonalDetails(string userId) {
            return await _context.Users.Include(u => u.PersonalDetail)
                            .SingleOrDefaultAsync(u => u.Id.ToString() == userId);
        }
    }
}