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
    public class SubjectRepository : RepositoryBase<SubjectEntity, SubjectDocument>, ISubjectRepository
    {
        private readonly IDynamoDBContext _dbContext;

        public SubjectRepository(IDynamoDBContext dbContext, ILogger<SubjectRepository> logger, IConfiguration configuration) : base(dbContext, logger, configuration)
        {
            _dbContext = dbContext;
        }

        #region Keys

        protected override string CreatePrimaryKey(SubjectDocument document)
        {
            return document.Type;
        }

        protected override string CreateSortKey(SubjectDocument document)
        {
            return document.Id;
        }

        #endregion

        #region Mappings

        protected override SubjectDocument MapToDocument(SubjectEntity entity)
        {
            if (entity == null) return null;
            
            return new SubjectDocument
            {
                Id = entity.Id,
                Name = entity.Name, 
                CreatedTime = entity.CreatedTime, 
                UpdatedTime = entity.UpdatedTime
            };
        }

        protected override SubjectEntity MapFromDocument(SubjectDocument document)
        {
            if (document == null) return null;
            
            return new SubjectEntity
            {
                Id = document.Id,
                Name = document.Name,
                CreatedTime = document.CreatedTime,
                UpdatedTime = document.UpdatedTime
            };
        }

        #endregion

        public override async Task<SubjectEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await GetItemByPrimaryKeyAndSortKeyAsync(SubjectDocument.GetDocumentType(), id, cancellationToken);
        }

        public override async Task DeleteItemAsync(string id, CancellationToken cancellationToken)
        {
            await DeleteItemByPrimaryKeyAndSortKeyAsync(SubjectDocument.GetDocumentType(), id, cancellationToken);
        }

        public async Task<IEnumerable<SubjectEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await GetAllByPrimaryKeyAsync(SubjectDocument.GetDocumentType(), cancellationToken);
        }
    }
}