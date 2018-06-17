using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Subject;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Response;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class SubjectAdminService : BaseService, ISubjectAdminService
    {
        private readonly IProfileSuperAdminService _profileSuperAdminService;

        public SubjectAdminService(IUnitOfWork unitOfWork, IProfileSuperAdminService profileSuperAdminService) : base(unitOfWork)
        {
            _profileSuperAdminService = profileSuperAdminService;
        }

        public async Task CreateSubjectAsync(CreateSubjectRequest model)
        {
            var profile = await _profileSuperAdminService.GetProfileByUserIdAsync(model.UserId);
            Subject subject = new Subject()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Created = DateTime.UtcNow,
                UserProfileId = profile.Id
            };

            UnitOfWork.SubjectRepository.Add(subject);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            var subject = await UnitOfWork.SubjectRepository.Query(x => x.Id == subjectId)
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new ArgumentNullException();
            }

            UnitOfWork.SubjectRepository.Delete(subject);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task EditSubjectAsync(EditSubjectRequest model)
        {
            var subject = await UnitOfWork.SubjectRepository.Query(x => x.Id == model.Id)
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new ArgumentNullException();
            }

            if (subject.Title != model.Title || subject.UserProfileId != model.UserProfileId)
            {
                subject.Title = model.Title;
                subject.UserProfileId = model.UserProfileId;

                UnitOfWork.SubjectRepository.Update(subject);
                await UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PageInfo<SubjectResponse>> GetAllSubjects(int page, int pageSize)
        {
            var subjects = UnitOfWork.SubjectRepository.GetAll()
                .Include(x => x.Themes)
                .OrderBy(x => x.Title);

            return await PagedHelper.CreatePagedResultsAsync(subjects, page, pageSize, x => x.ToSubjectResponse());
        }

        public async Task<PageInfo<SubjectResponse>> GetAllSubjectsByUserProfileId(int page, int pageSize, Guid userProfileId)
        {
            var subjects = UnitOfWork.SubjectRepository.Query(x => x.UserProfileId == userProfileId)
                .Include(x => x.Themes)
                .OrderBy(x => x.Title);

            return await PagedHelper.CreatePagedResultsAsync(subjects, page, pageSize, x => x.ToSubjectResponse());
        }

        public async Task<SubjectResponse> GetSubjectByIdAsync(Guid id)
        {
            var subject = await UnitOfWork.SubjectRepository.Query(x => x.Id == id)
                .Include(x => x.Themes)
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new ArgumentNullException();
            }

            return subject.ToSubjectResponse();
        }
    }
}