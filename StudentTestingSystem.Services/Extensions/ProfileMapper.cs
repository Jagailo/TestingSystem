using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Response;

namespace StudentTestingSystem.Services.Extensions
{
    public static class ProfileMapper
    {
        public static ProfileResponse ToProfileResponse(this UserProfile profile)
        {
            var profileResponse = new ProfileResponse();
            if (profile == null)
            {
                return profileResponse;
            }
            profileResponse.Id = profile.Id;
            profileResponse.UserId = profile.UserId;
            profileResponse.Email = profile.Email;
            profileResponse.FirstName = profile.FirstName;
            profileResponse.LastName = profile.LastName;
            profileResponse.Created = profile.Created;
            profileResponse.GroupId = profile.GroupId;
            profileResponse.Group = profile.Group;
            return profileResponse;
        }

        public static SuperAdminProfileResponse ToSuperAdminProfileResponse(this UserProfile profile)
        {
            var superAdminProfileResponse = new SuperAdminProfileResponse();
            if (profile == null)
            {
                return superAdminProfileResponse;
            }
            superAdminProfileResponse.Id = profile.Id;
            superAdminProfileResponse.UserId = profile.UserId;
            superAdminProfileResponse.Email = profile.Email;
            superAdminProfileResponse.FirstName = profile.FirstName;
            superAdminProfileResponse.LastName = profile.LastName;
            superAdminProfileResponse.Created = profile.Created;
            return superAdminProfileResponse;
        }
    }
}