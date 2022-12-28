using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;
using TripleT.User.Infrastructure.Persistence.Models;

namespace TripleT.User.Infrastructure.Persistence
{
    public class UserRepository : RepositoryBase<UserEntity, UserDocument>, IUserRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IDynamoDBContext dbContext, ILogger<UserRepository> logger, IConfiguration configuration)
            : base(dbContext, logger, configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private const string PK_TEMPLATE = "user#{0}";
        private const string SK_TEMPLATE = "user#{0}";

        protected override string CreatePrimaryKey(UserDocument document)
        {
            return string.Format(PK_TEMPLATE, document.Email);
        }

        protected override string CreateSortKey(UserDocument document)
        {
            return string.Format(SK_TEMPLATE, document.Email);
        }

        protected override UserDocument MapToDocument(UserEntity entity)
        {
            if (entity == null) return null;

            var userDocument = new UserDocument
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Cellphone = entity.Cellphone,
                Email = entity.Email,
                Role = entity.Role,
                DateOfBirth = entity.DateOfBirth,
                CreatedTime = entity.CreatedTime,
                UpdatedTime = entity.UpdatedTime,
                EmailVerification = new()
                {
                    Reference = entity.EmailVerification.Reference,
                    Verified = entity.EmailVerification.Verified,
                    VerifiedTime = entity.EmailVerification.VerifiedTime
                }
            };

            return userDocument;
        }

        protected override UserEntity MapFromDocument(UserDocument document)
        {
            if (document == null) return null;

            var userEntity = new UserEntity
            {
                Name = document.Name,
                Surname = document.Surname,
                Cellphone = document.Cellphone,
                Email = document.Email,
                Role = document.Role,
                DateOfBirth = document.DateOfBirth,
                CreatedTime = document.CreatedTime,
                UpdatedTime = document.UpdatedTime,
                EmailVerification = new()
                {
                    Reference = document.EmailVerification.Reference,
                    Verified = document.EmailVerification.Verified,
                    VerifiedTime = document.EmailVerification.VerifiedTime
                }
            };

            return userEntity;
        }

        public override async Task<UserEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await GetItemByPrimaryKeyAndSortKeyAsync(
                string.Format(PK_TEMPLATE, id),
                string.Format(SK_TEMPLATE, id),
                cancellationToken);
        }
    }
}