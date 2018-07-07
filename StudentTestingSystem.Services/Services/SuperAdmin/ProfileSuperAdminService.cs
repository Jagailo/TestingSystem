using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Request;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Response;

namespace StudentTestingSystem.Services.Services.SuperAdmin
{
    public class ProfileSuperAdminService : BaseService, IProfileSuperAdminService
    {
        public  ProfileSuperAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<SuperAdminProfileResponse> CreateProfileAsync(CreateProfileRequest model)
        {
            var profile = await UnitOfWork.UserProfileRepository.Query(x => x.User.Email == model.Email)
                .FirstOrDefaultAsync();

            if (profile != null)
            {
                return profile.ToSuperAdminProfileResponse();
            }
            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                SearchFullName = model.FirstName?.ToLower() + model.LastName?.ToLower(),
                GroupId = null,
                UserId = model.UserId
            };
            UnitOfWork.UserProfileRepository.Add(newProfile);
            await UnitOfWork.SaveChangesAsync();
            return newProfile.ToSuperAdminProfileResponse();
        }

        public async Task<PageInfo<SuperAdminProfileResponse>> GetProfilesByRoleIdAsync(string roleId, int page, int pageSize)
        {
            var userIds = await UnitOfWork.UserRepository.Query(x => x.Roles.Any(y => y.RoleId == roleId))
                .Select(x => x.Id)
                .ToListAsync();

            var profiles = UnitOfWork.UserProfileRepository.Query(x => userIds.Contains(x.UserId))
                .OrderBy(x => x.SearchFullName);

            return await PagedHelper.CreatePagedResultsAsync(profiles, page, pageSize, x => x.ToSuperAdminProfileResponse());
        }

        public async Task<SuperAdminProfileResponse> GetProfileByProfileIdAsync(Guid profileId)
        {
            var profile = await UnitOfWork.UserProfileRepository.Query(x => x.Id == profileId)
                .FirstOrDefaultAsync();
            if (profile == null)
            {
                throw new ArgumentNullException();
            }
            return profile.ToSuperAdminProfileResponse();
        }

        public async Task<SuperAdminProfileResponse> GetProfileByUserIdAsync(string userId)
        {
            var profile = await UnitOfWork.UserProfileRepository.Query(x => x.UserId == userId)
                .FirstOrDefaultAsync();
            if (profile == null)
            {
                throw new ArgumentNullException();
            }
            return profile.ToSuperAdminProfileResponse();
        }

        public async Task<string> GetUserIdByProfileIdAsync(Guid profileId)
        {
            var profile = await UnitOfWork.UserProfileRepository.Query(x => x.Id == profileId)
                .FirstOrDefaultAsync();

            if (profile == null)
            {
                throw new ArgumentNullException();
            }

            return profile.UserId;
        }

        public async Task DeleteProfileAsync(Guid profileId)
        {
            var profile = UnitOfWork.UserProfileRepository.Query(x => x.Id == profileId)
                .Include(x => x.Subjects)
                .FirstOrDefault();

            if (profile == null)
            {
                throw new ArgumentNullException();
            }
            UnitOfWork.UserProfileRepository.Delete(profile);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}