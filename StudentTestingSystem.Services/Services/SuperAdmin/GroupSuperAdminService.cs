using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Services.SuperAdmin
{
    public class GroupSuperAdminService : BaseService, IGroupSuperAdminService
    {
        public GroupSuperAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task DeleteAllGroupResultsAsync(Guid groupId)
        {
            var results = await UnitOfWork.ResultRepository.Query(x => x.Student.GroupId == groupId)
                .ToListAsync();

            foreach (var result in results)
            {
                UnitOfWork.ResultRepository.Delete(result);
            }
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(Guid groupId)
        {
            var group = await UnitOfWork.GroupRepository.Query(x => x.Id == groupId)
                .Include(x => x.Students)
                .FirstOrDefaultAsync();

            UnitOfWork.GroupRepository.Delete(group);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<PageInfo<GroupListDto>> GetAllGroupsAsync(int page = 1, int pageSize = 10)
        {
            var groups = UnitOfWork.GroupRepository.GetAll()
                .OrderBy(x => x.Number);

            return await PagedHelper.CreatePagedResultsAsync(groups, page, pageSize, x => x.ToGroupListDto());
        }

        public async Task<GroupListDto> GetGroupByIdAsync(Guid groupId)
        {
            var group = await UnitOfWork.GroupRepository.Query(x => x.Id == groupId)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                throw new ArgumentNullException();
            }

            return group.ToGroupListDto();
        }

        public async Task<bool> CheckIfGroupHasResults(Guid groupId)
        {
            var result = await UnitOfWork.ResultRepository.Query(x => x.Student.GroupId == groupId)
                .FirstOrDefaultAsync();

            return result != null;
        }
    }
}
