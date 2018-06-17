using Autofac;
using Autofac.Integration.Mvc;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Repositories;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.Student;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.Services.Admin;
using StudentTestingSystem.Services.Services.Student;
using StudentTestingSystem.Services.Services.SuperAdmin;
using StudentTestingSystem.SQLRepository;
using StudentTestingSystem.SQLRepository.Repositories;
using System.Web.Mvc;
using StudentTestingSystem.Storage;

namespace StudentTestingSystem
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();
            var container = GetContainer(containerBuilder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static IContainer GetContainer(ContainerBuilder containerBuilder)
        {
            // Dependency injection DB context
            containerBuilder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerRequest();

            // Dependency injection repositories
            containerBuilder.RegisterType<AnswerRepository>().As<IAnswerRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<GroupRepository>().As<IGroupRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<QuestionAnswerRepository>().As<IQuestionAnswerRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<QuestionRepository>().As<IQuestionRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<QuestionThemeRepository>().As<IQuestionThemeRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<ResultRepository>().As<IResultRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<SubjectRepository>().As<ISubjectRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<ThemeRepository>().As<IThemeRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<UserProfileRepositoty>().As<IUserProfileRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().AsImplementedInterfaces().InstancePerRequest();

            // Dependency injection application services
            // Super adminServices
            containerBuilder.RegisterType<GroupSuperAdminService>().As<IGroupSuperAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<ProfileSuperAdminService>().As<IProfileSuperAdminService>().AsImplementedInterfaces().InstancePerRequest();
            
            // Admin services
            containerBuilder.RegisterType<ThemeAdminService>().As<IThemeAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<QuestionAdminService>().As<IQuestionAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<ResultAdminService>().As<IResultAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<SubjectAdminService>().As<ISubjectAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<AnswerAdminService>().As<IAnswerAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<GroupAdminService>().As<IGroupAdminService>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<StudentAdminService>().As<IStudentAdminService>().AsImplementedInterfaces().InstancePerRequest();

            // Student services
            containerBuilder.RegisterType<ProfileService>().As<IProfileService>().AsImplementedInterfaces().InstancePerRequest();

            // Dependency injection application controllers
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Dependency injection integration services
            containerBuilder.RegisterType<LocalContentStorage>().As<IContentStorage>().AsImplementedInterfaces().InstancePerRequest();

            IContainer container = containerBuilder.Build();
            return container;
        }
    }
}