using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Domain.Models.Answer;
using StudentTestingSystem.Domain.Models.Group;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Domain.Models.Subject;
using StudentTestingSystem.Domain.Models.Theme;

namespace StudentTestingSystem.SQLRepository
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTheme> QuestionThemes { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UserProfile> Students { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            //Add configuration
            modelBuilder.Entity<QuestionTheme>().HasKey(q => new {q.QuestionId, q.ThemeId});
            modelBuilder.Entity<QuestionAnswer>().HasKey(q => new {q.AnswerId, q.QuestionId});
            modelBuilder.Entity<UserProfile>().HasMany(x => x.Results).WithRequired(x => x.Student).WillCascadeOnDelete();
            modelBuilder.Entity<Group>().HasMany(x => x.Students).WithOptional(x => x.Group).WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }

        private void AddTimestamps()
        {
            var entitiesAdded =
                ChangeTracker.Entries().Where(x => x.Entity is ICreated && x.State == EntityState.Added);
            foreach (var entity in entitiesAdded) ((ICreated) entity.Entity).Created = DateTime.UtcNow;

            var entitiesModified =
                ChangeTracker.Entries().Where(x => x.Entity is IUpdated && x.State == EntityState.Modified);
            foreach (var entity in entitiesModified) ((IUpdated) entity.Entity).Updated = DateTime.UtcNow;
        }
    }
}