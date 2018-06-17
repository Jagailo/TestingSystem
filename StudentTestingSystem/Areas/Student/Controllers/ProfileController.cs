using StudentTestingSystem.Areas.Admin.Controllers;
using StudentTestingSystem.Areas.Student.Models.Profile;
using StudentTestingSystem.Services.Abstract.Student;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Student.Controllers
{
    [Route("Student/[controller]/[action]")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: Student/Profile/Details
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Details()
        {
            var userId = GetCurrentUserId();
            var profile = await _profileService.GetProfileByUserIdAsync(userId);

            var model = new ProfileDetailsViewModel
            {
                Email = profile.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Group = profile.Group?.Title,
                GroupNumber = profile.Group.Number
            };

            return View(model);
        }
    }
}