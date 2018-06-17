using System.Threading.Tasks;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Request;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;

namespace StudentTestingSystem.Services.Abstract.Student
{
    public interface IProfileService
    {
        Task<ProfileResponse> CreateProfileAsync(CreateProfileRequest model);
        Task<ProfileResponse> GetProfileByUserIdAsync(string userId);
    }
}