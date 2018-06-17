using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Student.Request;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class StudentAdminService : BaseService, IStudentAdminService
    {
        public StudentAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PageInfo<ProfileResponse>> GetStudentsByGroupIdAsync(Guid groupId, int page, int pageSize)
        {
            var students = UnitOfWork.UserProfileRepository.Query(x => x.GroupId == groupId)
                .OrderBy(x => x.LastName);
            return await PagedHelper.CreatePagedResultsAsync(students, page, pageSize, x => x.ToProfileResponse());
        }

        public async Task<IEnumerable<UserProfile>> GetStudentsByGroupIdAsync(Guid groupId)
        {
            var students = await UnitOfWork.UserProfileRepository.Query(x => x.GroupId == groupId)
                .ToListAsync();

            return students;
        }

        // TODO: Throw exception
        public async Task<ProfileResponse> GetStudentByIdAsync(Guid profileId)
        {
            var student = await UnitOfWork.UserProfileRepository.Query(x => x.Id == profileId)
                .Include(x => x.Group)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                throw new ArgumentNullException();
            }
            return student.ToProfileResponse();
        }

        // TODO: Throw exception
        public async Task UpdateProfileAsync(UpdateStudentProfileRequest request)
        {
            var student = await UnitOfWork.UserProfileRepository.Query(x => x.Id == request.Id)
                .Include(x => x.User)
                .Include(x => x.Group)
                .Include(x => x.Subjects)
                .Include(x => x.Results)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                throw new ArgumentNullException();
            }
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.GroupId = request.GroupId;
            student.Email = request.Email;
            student.User.Email = request.Email;
            student.SearchFullName = request.FirstName?.ToLower() + request.LastName?.ToLower();
            UnitOfWork.UserProfileRepository.Update(student);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
