using StudentTestingSystem.Domain.Models.Group;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;

namespace StudentTestingSystem.Services.Extensions
{
    public static class GroupMapper
    {
        public static GroupListDto ToGroupListDto(this Group group)
        {
            var groupListDto = new GroupListDto();
            if (group != null)
            {
                groupListDto.Id = group.Id;
                groupListDto.Title = group.Title;
                groupListDto.Created = group.Created;
                groupListDto.Number = group.Number;
            }
            return groupListDto;
        }

        public static GroupDto ToGroupDto(this Group group)
        {
            var groupDto = new GroupDto();
            if (group != null)
            {
                groupDto.Id = group.Id;
                groupDto.Title = group.Title;
                groupDto.Created = group.Created;
                groupDto.Number = group.Number;
            }
            return groupDto;
        }
    }
}
