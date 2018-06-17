using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Theme;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Response;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class ThemeAdminService : BaseService, IThemeAdminService
    {
        private readonly IQuestionAdminService _questionAdminService;
        private readonly IResultAdminService _resultAdminService;

        public ThemeAdminService(
            IUnitOfWork unitOfWork, 
            IQuestionAdminService questionAdminService, 
            IResultAdminService resultAdminService) : base(unitOfWork)
        {
            _questionAdminService = questionAdminService;
            _resultAdminService = resultAdminService;
        }

        public async Task CreateThemeAsync(CreateThemeRequest model)
        {
            Theme theme = new Theme()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Created = DateTime.UtcNow,
                SubjectId = model.SubjectId,
                TimeLine = model.TimeLine
            };

            UnitOfWork.ThemeRepository.Add(theme);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteThemeAsync(Guid themeId)
        {
            var theme = await UnitOfWork.ThemeRepository.Query(x => x.Id == themeId)
                .FirstOrDefaultAsync();

            if (theme == null)
            {
                throw new ArgumentNullException();
            }

            await _resultAdminService.DeleteResultsByThemeIdAsync(themeId);
            await _questionAdminService.DeleteQuestionsByThemeIdAsync(themeId);

            UnitOfWork.ThemeRepository.Delete(theme);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task EditThemeAsync(EditThemeRequest model)
        {
            var theme = await UnitOfWork.ThemeRepository.Query(x => x.Id == model.Id)
                .FirstOrDefaultAsync();

            if (theme == null)
            {
                throw new ArgumentNullException();
            }

            if (theme.Title != model.Title || theme.TimeLine != model.TimeLine)
            {
                theme.Title = model.Title;
                theme.TimeLine = model.TimeLine;

                UnitOfWork.ThemeRepository.Update(theme);
                await UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PageInfo<ThemeResponse>> GetAllThemesBySubjectId(Guid subjectId, int page, int pageSize)
        {
            var themes = UnitOfWork.ThemeRepository.Query(x => x.SubjectId == subjectId)
                .OrderBy(x => x.Title);

            return await PagedHelper.CreatePagedResultsAsync(themes, page, pageSize, x => x.ToThemeResponse());
        }

        public async Task<ThemeResponse> GetThemeByIdAsync(Guid id)
        {
            var theme = await UnitOfWork.ThemeRepository.Query(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (theme == null)
            {
                throw new ArgumentNullException();
            }

            return theme.ToThemeResponse();
        }
    }
}