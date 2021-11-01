using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence
{
    public static class TripleTDbContextSeed
    {
        public static async Task SeedSampleDataAsync(TripleTDbContext context)
        {
            RoleEntity userRole = new RoleEntity { Name = "client", Description = "Normal role for all clients" };
            RoleEntity adminRole = new RoleEntity { Name = "admin", Description = "Admin role for managing clients" };

            //UserEntity userNormal1 = GetUser("Raymond", "van Tonder", "rayvtonder8@gmail.com", userRole);
            UserEntity userNormal2 = GetUser("Test", "Client", "testclient1@gmail.com", userRole);
            UserEntity userAdmin = GetUser("Admin", "I Am", "admin1@gmail.com", adminRole);

            if (!context.Users.Any())
            {
                //await context.Users.AddAsync(userNormal1);
                await context.Users.AddAsync(userNormal2);
                await context.Users.AddAsync(userAdmin);

                await context.SaveChangesAsync(CancellationToken.None);
            }

            LanguageEntity languageEntity = new LanguageEntity { Value = "Afrikaans" };
            GradeEntity gradeEntity = new GradeEntity { Value = "9" };
            ModuleTypeEntity moduleTypeEntity = new ModuleTypeEntity { TypeName = "Theory Summary" };
            SubjectEntity subjectEntity = new SubjectEntity { Name = "Maths" };

            await context.Languages.AddAsync(languageEntity);
            await context.Grades.AddAsync(gradeEntity);
            await context.ModuleTypes.AddAsync(moduleTypeEntity);
            await context.Subjects.AddAsync(subjectEntity);

            ModuleEntity moduleEntity1 = new ModuleEntity
            {
                Name = "Module one",
                Description = "First One",
                FileLocation = "SomeLocation",
                Price = 200,
                Subject = subjectEntity,
                ModuleType = moduleTypeEntity,
                Language = languageEntity,
                Grade = gradeEntity
            };

            ModuleEntity moduleEntity2 = new ModuleEntity
            {
                Name = "Module Two",
                Description = "First Two",
                FileLocation = "SomeLocation Two",
                Price = 20,
                Subject = subjectEntity,
                ModuleType = moduleTypeEntity,
                Language = languageEntity,
                Grade = gradeEntity
            };

            await context.Modules.AddAsync(moduleEntity1);
            await context.Modules.AddAsync(moduleEntity2);

            PackageEntity packageEntity = new PackageEntity
            {
                Name = "Maths one and two",
                Description = "All maths",
                Price = 40,
                Subject = subjectEntity,
                Grade = gradeEntity,
                Modules = new List<ModuleEntity> { moduleEntity1, moduleEntity2 }
            };

            await context.Packages.AddAsync(packageEntity);

            await context.SaveChangesAsync(CancellationToken.None);

            foreach (var entry in context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }


        private static UserEntity GetUser(string name, string surname, string email, RoleEntity role)
        {
            return new UserEntity
            {
                Firstname = name,
                Surname = surname,
                CreatedTime = DateTime.Now,
                DateOfBirth = new DateTime(1995, 10, 10),
                Cellphone = "0718123447",
                Email = email,
                Password = "PALvxy9S3VuBY31PftWoMEdbgcdoRoAPY6AtASdimVc=",
                Role = role,
                EmailVerification = new EmailVerificationEntity { Reference = Guid.NewGuid().ToString(), Verified = false },
            };
        }
    }
}
