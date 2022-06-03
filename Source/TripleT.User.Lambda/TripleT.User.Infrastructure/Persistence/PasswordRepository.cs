using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;
using TripleT.User.Infrastructure.Persistence.Models;
using PasswordResetDetails = TripleT.User.Infrastructure.Persistence.Models.PasswordResetDetails;

namespace TripleT.User.Infrastructure.Persistence
{
    public class PasswordRepository : RepositoryBase<PasswordEntity, PasswordDocument>, IPasswordRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ILogger _logger;

        public PasswordRepository(IDynamoDBContext dbContext, ILogger<PasswordRepository> logger, IConfiguration configuration)
            : base(dbContext, logger, configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private const string PK_TEMPLATE = "password#{0}";
        private const string SK_TEMPLATE = "password#{0}";

        protected override string CreatePrimaryKey(PasswordDocument document)
        {
            return string.Format(PK_TEMPLATE, document.Email);
        }

        protected override string CreateSortKey(PasswordDocument document)
        {
            return string.Format(SK_TEMPLATE, document.Email);
        }

        protected override PasswordDocument MapToDocument(PasswordEntity entity)
        {
            if (entity == null) return null;

            return new PasswordDocument
            {
                Email = entity.Email,
                Salt = entity.Salt,
                Value = entity.Value,
                CreatedTime = entity.CreatedTime,
                UpdatedTime = entity.UpdatedTime,
                PasswordResetDetails = entity.PasswordResetDetails == null
                    ? null
                    : new PasswordResetDetails
                    {
                        Reference = entity.PasswordResetDetails.Reference,
                        ExpiryTime = entity.PasswordResetDetails.ExpiryTime
                    }
            };
        }

        protected override PasswordEntity MapFromDocument(PasswordDocument document)
        {
            if (document == null) return null;

            return new PasswordEntity()
            {
                Email = document.Email,
                Salt = document.Salt,
                Value = document.Value,
                CreatedTime = document.CreatedTime,
                UpdatedTime = document.UpdatedTime,
                PasswordResetDetails = document.PasswordResetDetails == null
                    ? null
                    : new Domain.Domain.PasswordResetDetails
                    {
                        Reference = document.PasswordResetDetails.Reference,
                        ExpiryTime = document.PasswordResetDetails.ExpiryTime
                    }
            };
        }

        public override async Task<PasswordEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await GetItemByPrimaryKeyAndSortKeyAsync(
                string.Format(PK_TEMPLATE, id), 
                string.Format(SK_TEMPLATE, id),
                cancellationToken);
        }
    }
}