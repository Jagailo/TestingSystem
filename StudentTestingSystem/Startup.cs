using System.Data.Entity;
using Microsoft.Owin;
using Owin;
using StudentTestingSystem.SQLRepository;
using StudentTestingSystem.SQLRepository.Migrations;

[assembly: OwinStartupAttribute(typeof(StudentTestingSystem.Startup))]
namespace StudentTestingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
