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


            var summaryEntity1 = new SummaryEntity() { Name = "Trig", Description = "Triangles", FileLocation = "test\\test", Price = 90.5m, };
            var summaryEntity2 = new SummaryEntity() { Name = "Problems", Description = "Triangles", FileLocation = "test\\test", Price = 90.5m };

            var categoryEntity = new SummaryCategoryEntity { Name = "Maths" };

            categoryEntity.Summaries.Add(summaryEntity1);
            categoryEntity.Summaries.Add(summaryEntity2);

            var summaryPackageEntity = new SummaryPackageEntity { Name = "All Maths", Description = "All of it", Price = 200m };
            categoryEntity.SummaryPackages.Add(summaryPackageEntity);

            var packageSummaryLink1 = new PackageSummaryLinkEntity() { SummaryPackage = summaryPackageEntity, Summary = summaryEntity1 };
            var packageSummaryLink2 = new PackageSummaryLinkEntity() { SummaryPackage = summaryPackageEntity, Summary = summaryEntity2 };

            await context.SummaryCategories.AddAsync(categoryEntity);

            await context.PackageSummariesLink.AddRangeAsync(packageSummaryLink1);
            await context.PackageSummariesLink.AddRangeAsync(packageSummaryLink2);

            await context.SaveChangesAsync(CancellationToken.None);

            //categoryEntity.SummaryPackages.Add()

            //context.SummaryCategories
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
