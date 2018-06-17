using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.Abstract.Student;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Request;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;

namespace StudentTestingSystem.Services.Services.Student
{
    public class ProfileService : BaseService, IProfileService
    {
        public ProfileService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ProfileResponse> CreateProfileAsync(CreateProfileRequest model)
        {
            var profile = UnitOfWork.UserProfileRepository.Query(x => x.UserId == model.UserId)
                .FirstOrDefault();
            if (profile != null)
            {
                return profile.ToProfileResponse();
            }
            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                SearchFullName = model.FirstName?.ToLower() + model.LastName?.ToLower(),
                GroupId = model.GroupId,
                UserId = model.UserId
            };
            UnitOfWork.UserProfileRepository.Add(newProfile);
            await UnitOfWork.SaveChangesAsync();
            return newProfile.ToProfileResponse();         
        }

        public async Task<ProfileResponse> GetProfileByUserIdAsync(string userId)
        {
            var profile = await UnitOfWork.UserProfileRepository.Query(x => x.UserId == userId)
                .Include(x => x.Group)
                .FirstOrDefaultAsync();

            if (profile == null)
            {
                throw new ArgumentNullException();
            }
            return profile.ToProfileResponse();
        }
    }
}