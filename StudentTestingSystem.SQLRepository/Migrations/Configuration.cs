using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Domain.Models.Group;
using StudentTestingSystem.Domain.Models.Profile;
using System;
using System.Linq;

namespace StudentTestingSystem.SQLRepository.Migrations
{
    using System.Data.Entity.Migrations;

    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var testUserGroupId = Guid.NewGuid();

            // Roles

            if (!context.Roles.Any(x => x.Name == "User"))
            {
                roleManager.Create(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User"
                });
            }

            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin"
                });
            }

            if (!context.Roles.Any(x => x.Name == "SuperAdmin"))
            {
                roleManager.Create(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SuperAdmin"
                });
            }

            // Groups

            if (!context.Groups.Any(x => x.Number == 10702114))
            {
                context.Groups.AddOrUpdate(new Group
                {
                    Id = testUserGroupId,
                    Number = 10702114,
                    Title = "�������������� ������� � ���������� � ��������� � ������������� ����������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10702214))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10702214,
                    Title = "�������������� ������� � ���������� � ��������� � ������������� ����������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10702314))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10702314,
                    Title = "�������������� ������� � ���������� � �������������� � ������������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10701114))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10701114,
                    Title = "����������� ����������� �������������� ����������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10701214))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10701214,
                    Title = "����������� ����������� �������������� ����������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10705114))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10705114,
                    Title = "������������������ ��������������",
                    Created = DateTime.UtcNow
                });
            }

            if (!context.Groups.Any(x => x.Number == 10705214))
            {
                var groupId = Guid.NewGuid();
                context.Groups.AddOrUpdate(new Group
                {
                    Id = groupId,
                    Number = 10705214,
                    Title = "������������������ ��������������",
                    Created = DateTime.UtcNow
                });
            }

            // Users

            if (!context.Users.Any(u => u.Email == "user@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "user@gmail.com", Email = "user@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "User");

                if (!context.Students.Any(x => x.Email == "user@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "user@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "�������",
                        LastName = "�����",
                        GroupId = testUserGroupId,
                        SearchFullName = "������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }

            if (!context.Users.Any(u => u.Email == "user2@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "user2@gmail.com", Email = "user2@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "User");

                if (!context.Students.Any(x => x.Email == "user2@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "user2@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "�������",
                        LastName = "�������",
                        GroupId = testUserGroupId,
                        SearchFullName = "��������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }

            if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "Admin");

                if (!context.Students.Any(x => x.Email == "admin@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "admin@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "�����",
                        LastName = "��������",
                        GroupId = null,
                        SearchFullName = "�������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }

            if (!context.Users.Any(u => u.Email == "admin2@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "admin2@gmail.com", Email = "admin2@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "Admin");

                if (!context.Students.Any(x => x.Email == "admin2@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "admin2@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "�������",
                        LastName = "���������",
                        GroupId = null,
                        SearchFullName = "����������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }

            if (!context.Users.Any(u => u.Email == "admin3@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "admin3@gmail.com", Email = "admin3@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "Admin");

                if (!context.Students.Any(x => x.Email == "admin3@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "admin3@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "��������",
                        LastName = "�������",
                        GroupId = null,
                        SearchFullName = "���������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }

            if (!context.Users.Any(u => u.Email == "superadmin@gmail.com"))
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new User { Id = userId, UserName = "superadmin@gmail.com", Email = "superadmin@gmail.com" };
                userManager.Create(userToInsert, "123456789");
                userManager.AddToRole(userId, "SuperAdmin");

                if (!context.Students.Any(x => x.Email == "superadmin@gmail.com"))
                {
                    var profileId = Guid.NewGuid();
                    var profileToInsert = new UserProfile
                    {
                        Id = profileId,
                        Email = "superadmin@gmail.com",
                        Created = DateTime.UtcNow,
                        FirstName = "������",
                        LastName = "�������",
                        GroupId = null,
                        SearchFullName = "�������������",
                        UserId = userId
                    };
                    context.Students.AddOrUpdate(profileToInsert);
                }
            }
        }
    }
}