using System.Collections.Generic;
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
    public class GradeRepository : RepositoryBase<GradeEntity, GradeDocument>, IGradeRepository
    {
        private readonly IDynamoDBContext _dbContext;

        public GradeRepository(IDynamoDBContext dbContext, ILogger<GradeRepository> logger, IConfiguration configuration) : base(dbContext, logger, configuration)
        {
            _dbContext = dbContext;
        }

        #region Keys

        protected override string CreatePrimaryKey(GradeDocument document)
        {
            return document.Type;
        }

        protected override string CreateSortKey(GradeDocument document)
        {
            return document.Id;
        }

        #endregion

        #region Mappings

        protected override GradeDocument MapToDocument(GradeEntity entity)
        {
            if (entity == null) return null;

            return new GradeDocument
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedTime = entity.CreatedTime,
                UpdatedTime = entity.UpdatedTime
            };
        }

        protected override GradeEntity MapFromDocument(GradeDocument document)
        {
            if (document == null) return null;

            return new GradeEntity
            {
                Id = document.Id,
                Name = document.Name,
                CreatedTime = document.CreatedTime,
                UpdatedTime = document.UpdatedTime
            };
        }

        #endregion

        public override async Task<GradeEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await GetItemByPrimaryKeyAndSortKeyAsync(GradeDocument.GetDocumentType(), id, cancellationToken);
        }

        public override async Task DeleteItemAsync(string id, CancellationToken cancellationToken)
        {
            await DeleteItemByPrimaryKeyAndSortKeyAsync(GradeDocument.GetDocumentType(), id, cancellationToken);
        }

        public async Task<IEnumerable<GradeEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await GetAllByPrimaryKeyAsync(GradeDocument.GetDocumentType(), cancellationToken);
        }
    }
}