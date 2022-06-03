using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;
using TripleT.User.Infrastructure.Persistence.Models;
using TripleT.User.Infrastructure.Persistence.Models.Interfaces;

namespace TripleT.User.Infrastructure.Persistence
{
    public class SummaryProductRepository : RepositoryBase<SummaryProductEntity, SummaryProductDocument>, ISummaryProductRepository
    {
        private readonly IDynamoDBContext _dbContext;

        public SummaryProductRepository(IDynamoDBContext dbContext, ILogger<SummaryProductRepository> logger, IConfiguration configuration)
            : base(dbContext, logger, configuration)
        {
            _dbContext = dbContext;
        }

        #region Keys

        private const string PK_TEMPLATE = "{0}#{1}";

        protected override string CreatePrimaryKey(SummaryProductDocument document)
        {
            return string.Format(PK_TEMPLATE, SummaryProductDocument.GetDocumentType(), document.Id);
        }

        protected override string CreateSortKey(SummaryProductDocument document)
        {
            return SummaryProductDocument.GetDocumentType();
        }

        #endregion

        #region Mappings

        protected override SummaryProductDocument MapToDocument(SummaryProductEntity entity)
        {
            if (entity == null) return null;

            return new SummaryProductDocument
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                CreatedTime = entity.CreatedTime,
                UpdatedTime = entity.UpdatedTime,
                FileLocation = entity.FileLocation,
                GradeId = entity.GradeId,
                IconUri = entity.IconUri,
                SubjectId = entity.SubjectId,
                FileSizeInMb = entity.FileSizeInMb,
                GSI1PK = entity.SubjectId,
                GSI1SK = entity.GradeId,
                GSI2PK = entity.GradeId,
                GSI2SK = entity.SubjectId,
            };
        }

        protected override SummaryProductEntity MapFromDocument(SummaryProductDocument document)
        {
            if (document == null) return null;

            return new SummaryProductEntity
            {
                Id = document.Id,
                Title = document.Title,
                Description = document.Description,
                Price = document.Price,
                CreatedTime = document.CreatedTime,
                FileLocation = document.FileLocation,
                GradeId = document.GradeId,
                IconUri = document.IconUri,
                SubjectId = document.SubjectId,
                UpdatedTime = document.UpdatedTime,
                FileSizeInMb = document.FileSizeInMb
            };
        }

        #endregion

        public override async Task DeleteItemAsync(string id, CancellationToken cancellationToken)
        {
            await DeleteItemByPrimaryKeyAndSortKeyAsync(
                string.Format(PK_TEMPLATE, SummaryProductDocument.GetDocumentType(), id),
                SummaryProductDocument.GetDocumentType(), cancellationToken);
        }

        public override async Task<SummaryProductEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await GetItemByPrimaryKeyAndSortKeyAsync(
                string.Format(PK_TEMPLATE, SummaryProductDocument.GetDocumentType(), id),
                SummaryProductDocument.GetDocumentType(),
                cancellationToken);
        }

        public async Task<IEnumerable<SummaryProductEntity>> GetBySubjectId(string subjectId, CancellationToken cancellationToken)
        {
            return await QueryGlobalSecondaryIndex(IGlobalSecondaryIndex1.GSI1IndexName, subjectId, cancellationToken);
        }

        public async Task<IEnumerable<SummaryProductEntity>> GetByGradeId(string gradeId, CancellationToken cancellationToken)
        {
            return await QueryGlobalSecondaryIndex(IGlobalSecondaryIndex2.GSI2IndexName, gradeId, cancellationToken);
        }

        public async Task<IEnumerable<SummaryProductEntity>> GetBySubjectAndGradeId(string subjectId, string gradeId, CancellationToken cancellationToken)
        {
            return await QueryGlobalSecondaryIndex(IGlobalSecondaryIndex1.GSI1IndexName, subjectId, QueryOperator.Equal, new[] {gradeId}, cancellationToken);
        }
    }
}