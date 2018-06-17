using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Group;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class GroupAdminService : BaseService, IGroupAdminService
    {
        private readonly IStudentAdminService _studentAdminService;
        public  GroupAdminService(
            IUnitOfWork unitOfWork, 
            IStudentAdminService studentAdminService) : base(unitOfWork)
        {
            _studentAdminService = studentAdminService;
        }

        public async Task<PageInfo<GroupListDto>> GetAllGroupsAsync(int page = 1, int pageSize = 10)
        {
            var groups = UnitOfWork.GroupRepository.GetAll()
                .OrderBy(x => x.Number);

            return await PagedHelper.CreatePagedResultsAsync(groups, page, pageSize, x => x.ToGroupListDto());
        }

        //TODO: Exception
        public async Task<GroupDto> GetGroupByIdAsync(Guid groupId)
        {
            var group = await UnitOfWork.GroupRepository.Query(x => x.Id == groupId)
                .FirstOrDefaultAsync();
            if (group == null)
            {
                throw new ArgumentNullException();
            }
            return group.ToGroupDto();
        }


        public async Task AddGroupAsync(AddGroupRequest request)
        {
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Number = request.Number,
                Title = request.Title,
                Students = new List<UserProfile>(),
                Created = DateTime.UtcNow
            };
            UnitOfWork.GroupRepository.Add(group);
            await UnitOfWork.SaveChangesAsync();
        }

        //TODO: Exception
        public async Task UpdateGroupAsync(UpdateGroupRequest request)
        {
            var group = await UnitOfWork.GroupRepository.Query(x => x.Id == request.Id)
                .FirstOrDefaultAsync();
            if (group == null)
            {
                throw new ArgumentNullException();
            }
            group.Title = request.Title;
            group.Number = request.Number;
            UnitOfWork.GroupRepository.Update(group);
            await UnitOfWork.SaveChangesAsync();
        }

        //TODO: Exception
        public async Task DeleteGroupAsync(Guid groupId)
        {
            var group = await UnitOfWork.GroupRepository.Query(x => x.Id == groupId)
                .FirstOrDefaultAsync();
            if (group == null)
            {
                throw new ArgumentNullException();
            }

            var students = await _studentAdminService.GetStudentsByGroupIdAsync(groupId);

            foreach (var student in students)
            {
                student.GroupId = null;
                UnitOfWork.UserProfileRepository.Update(student);
            }

            UnitOfWork.GroupRepository.Delete(group);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupListDto>> GetAllGroupsAsync()
        {
            var groups = await UnitOfWork.GroupRepository.GetAll()
                .ToListAsync();
            return groups.Select(x => x.ToGroupListDto());
        }
    }
}
